using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class BoundingBox
    {
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public double ZMin { get; set; }
        public double ZMax { get; set; }
    }

    public class GCodeLine
    {
        #region "Public Properties"

        /// <summary>
        /// GCode Line x, y, z, i, j, f values
        /// </summary>
        public string x = string.Empty;
        public string y = string.Empty;
        public string z = string.Empty;
        public string i = string.Empty;
        public string j = string.Empty;
        public string f = string.Empty;

        //If the gcode line is comment
        public bool IsComment { get; set; }

        public BoundingBox GetBoundingBox()
        {
            BoundingBox bbox = new BoundingBox() { XMax = -1000, XMin = 1000, YMax = -1000, YMin = 1000, ZMax = -1000, ZMin = 1000 };
            GCodeLine curLn = this;
            while (curLn != null)
            {
                CNCPoint toPt = curLn.ToPosition;
                if (toPt.x < bbox.XMin)
                    bbox.XMin = toPt.x;
                if (toPt.x > bbox.XMax)
                    bbox.XMax = toPt.x;

                if (toPt.y < bbox.YMin)
                    bbox.YMin = toPt.y;
                if (toPt.y > bbox.YMax)
                    bbox.YMax = toPt.y;

                if (toPt.z < bbox.ZMin)
                    bbox.ZMin = toPt.z;
                if (toPt.z > bbox.ZMax)
                    bbox.ZMax = toPt.z;

                curLn = curLn.next;

            }
            return bbox;
        }
        public int LineNum
        {
            get;
            set;
        }


        /// <summary>
        /// X Value formatter
        /// </summary>
        public double X
        {
            get
            {
                if (x.Length > 0)
                {
                    double d = 0;
                    if (double.TryParse(x, out d))
                    {
                        return d;
                    }
                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    ///-99999 is a indicating the value not being set
                    return -99999;
                }
            }
            set
            {
                //For the value to be displayed decimal points
                x = Utils.FormatFloatDisplay(value);
            }
        }
        public double Y
        {
            get
            {
                if (y.Length > 0)
                {
                    double d = 0;
                    if (double.TryParse(y, out d))
                    {
                        return d;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -99999;
                }
            }
            set
            {
                y = Utils.FormatFloatDisplay(value);
            }
        }
        public double Z
        {
            get
            {
                if (z.Length > 0)
                {
                    double d = 0;
                    if (double.TryParse(z, out d))
                    {
                        return d;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -99999;
                }
            }
            set
            {
                z = Utils.FormatFloatDisplay(value);
            }
        }
        public double F
        {
            get
            {
                if (f.Length > 0)
                {
                    double d = 0;
                    if (double.TryParse(f, out d))
                    {
                        return d;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    return 0.00f;
            }
            set
            {
                f = Utils.FormatFloatDisplay(value);
            }
        }

        //Is line a position
        public bool isPosition = false;
        //Is line an Arc
        public bool isArc = false;

        #endregion

        #region "Private Properties"
        private int xDir = 0;
        private int yDir = 0;
        private int zDir = 0;

        //Current X, Y, Z Direction
        private static int curXDir = 0;
        private static int curYDir = 0;
        private static int curZDir = 0;

        //Current active GCode command
        public static string currentG = string.Empty;

        //GCode original input line
        public string inputLine = string.Empty;

        //GCode local representative, with backlash and fill out necessary values
        private string localLine = string.Empty;

        //Comment to display 
        private string comment = string.Empty;

        private string gCommand = string.Empty;



        //M Command
        private string mCommand = string.Empty;

        //Previous GCode Line, for linked list
        public GCodeLine prev = null;

        //Next GCode Line, for linked list
        public GCodeLine next = null;

        /// <summary>
        /// Process command
        /// </summary>
        private void ProcessCommand()
        {
            if (localLine.StartsWith("T"))
            {
                IsSupported = false;
                return;
            }
            Regex r = new Regex(gPattern, RegexOptions.IgnoreCase);

            Match gm = gCommR.Match(localLine);
            Match mm = mCommR.Match(localLine);

            IsSupported = false;


            //If gcode found
            if (gm.Success)
            {
                gCommand = gm.Value.ToString();
                IsSupported = true;

                currentG = gCommand;
            }

            //If mcode found
            if (mm.Success)
            {
                mCommand = mm.Value.ToString();
                IsSupported = true;

            }


            //if there isn't a valid gcode, using current g command as active g command
            if (gCommand.Length == 0)
            {
                if (x.Length > 0 || y.Length > 0 || z.Length > 0)
                {
                    gCommand = currentG;
                    IsSupported = true;
                }
            }


            if (!IsSupported)
            {
                Console.WriteLine("Not Supported:" + localLine);
            }

            //Non Supported GCode Guard
            if (localLine.StartsWith("T1"))
            {
                IsSupported = false;
            }


        }
        private bool xPresent = false;
        private bool yPresent = false;
        private bool zPresent = false;
        private bool fPresent = false;

        #endregion

        #region "Constants"
        /// <summary>
        /// regular expression for finding g code patterns
        /// </summary>

        private string[] commentSignbols = { @"\(.*\)", @";.*" };

        private const string nPattern = @"^N\d+";
        private static Regex nCommR = new Regex(nPattern, RegexOptions.IgnoreCase);

        private const string gPattern = @"^G\d+\.?\d?";
        private static Regex gCommR = new Regex(gPattern, RegexOptions.IgnoreCase);

        private const string mPattern = @"^M";
        private static Regex mCommR = new Regex(mPattern, RegexOptions.IgnoreCase);

        private const string xPosPat = @"X[+-]?\d*\.?\d*";
        private static Regex xPosR = new Regex(xPosPat, RegexOptions.IgnoreCase);

        private const string yPosPat = @"Y[+-]?\d*\.?\d*";
        private static Regex yPosR = new Regex(yPosPat, RegexOptions.IgnoreCase);

        private const string zPosPat = @"Z[+-]?\d*\.?\d*";
        private static Regex zPosR = new Regex(zPosPat, RegexOptions.IgnoreCase);

        private const string fRatePat = @"F[+-]?\d*\.?\d*";
        private static Regex fRateR = new Regex(fRatePat, RegexOptions.IgnoreCase);

        private const string iPat = @"I[+-]?\d*\.?\d*";
        private static Regex iR = new Regex(fRatePat, RegexOptions.IgnoreCase);

        private const string jPat = @"F[+-]?\d*\.?\d*";
        private static Regex jR = new Regex(fRatePat, RegexOptions.IgnoreCase);


        #endregion

        /// <summary>
        /// String representation of gcode line
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            if (comment.Length > 0)
            {
                return "Comments:" + comment;
            }
            else
            {
                return "From:" + FromPosition.ToString() + " To:" + ToPosition.ToString() +
                    "; Directory:" + this.xDir.ToString() + "," + this.yDir.ToString() + "," + this.zDir.ToString();
            }
        }

        //Fillin missing parts and create a complete gcode line
        public string CompleteLine()
        {
            string ret = string.Empty;
            if (IsG())
            {
                if (!isArc)
                {
                    ret += gCommand;
                    ret += " X" + X;
                    ret += " Y" + Y;
                    ret += " Z" + Z;
                    ret += " F" + F;
                }
                else
                {
                    ret += inputLine;
                }
            }
            else
            {
                ret = inputLine;
            }

            return ret;
        }

        //The line index
        private int _index = -1;
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }


        /// <summary>
        /// Is there a breakpoint set for current line
        /// </summary>
        public bool isBreakPoint = false;



        //Accumilated travel distance
        public double traveledDist = 0.0;

        public string CodeToSend()
        {
            return this.inputLine;
        }

        //The position 
        public CNCPoint GCodePosition
        {
            get
            {
                return new CNCPoint(X, Y, Z);
            }
        }

        public CNCPoint ToPosition
        {
            get; set;
        }


        //Current X, Y, Z position and feedrate
        public static string curX = string.Empty;
        public static string curY = string.Empty;
        public static string curZ = string.Empty;
        public static string curF = string.Empty;


        public CNCPoint FromPosition { get; set; }



        public static List<GCodeLine> LoadFromList(List<string> strlist)
        {
            int idx = 0;
            List<GCodeLine> list = new List<GCodeLine>();

            //Create a fist line to setup initial positions, in case no information from grbl
            GCodeLine firstLn = new GCodeLine() { Index = idx };
            firstLn.SetInitialPosition();
            list.Add(firstLn);

            foreach (string ln in strlist)
            {
                try
                {
                    idx++;
                    //Create a GCode Line
                    GCodeLine gcodeLn = new GCodeLine(ln) { Index = idx };

                    if (gcodeLn.comment.Length > 0)
                    {
                        continue;
                    }
                    list.Add(gcodeLn);

                    //Create linked list
                    GCodeLine prevCode = list[list.Count - 2];
                    gcodeLn.prev = prevCode;
                    prevCode.next = gcodeLn;
                    if (gcodeLn.IsPositionCode)
                    {
                        //gcodeLn.CalculateTravelDist();
                        //gcodeLn.GetCurrentDir();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "<<" + ln + ">>");
                }

            }

            return list;
        }
        /// <summary>
        /// Load GCode from a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<GCodeLine> LoadFromFile(string fileName)
        {
            int idx = 0;
            List<GCodeLine> list = new List<GCodeLine>();

            //Create a fist line to setup initial positions, in case no information from grbl
            GCodeLine firstLn = new GCodeLine() { Index = idx };
            firstLn.SetInitialPosition();
            list.Add(firstLn);

            //Start reading file
            string ln = string.Empty;
            StreamReader sr = new StreamReader(fileName);
            while ((ln = sr.ReadLine()) != null)
            {
                try
                {
                    idx++;
                    //Create a GCode Line
                    GCodeLine gcodeLn = new GCodeLine(ln) { Index = idx };

                    if (gcodeLn.comment.Length > 0)
                    {
                        continue;
                    }
                    list.Add(gcodeLn);


                    //Create linked list
                    GCodeLine prevCode = list[list.Count - 2];
                    gcodeLn.prev = prevCode;
                    prevCode.next = gcodeLn;
                    if (gcodeLn.IsPositionCode)
                    {
                        //gcodeLn.CalculateTravelDist();
                        //gcodeLn.GetCurrentDir();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "<<" + ln + ">>");
                }

            }
            return list;
        }

        public GCodeLine AddGCodeLine(string l)
        {
            GCodeLine newLine = new GCodeLine(l, this);

            return newLine;
        }

        #region "Constructors"
        public GCodeLine()
        {

        }

        public int GetCurrentLineNumber()
        {
            int cnt = 1;
            GCodeLine tempL = this;
            while (tempL.prev != null)
            {
                tempL = tempL.prev;
                cnt++;
            }

            return cnt;
        }
        /// <summary>
        /// Main function to parse a gcode
        /// </summary>
        /// <param name="l"></param>
        public GCodeLine(string l)
        {
            inputLine = localLine = l;



            //Remove line number, if presents
            RemoveLineNumber();
            //Remove comments
            RemoveComments();

            //Parse the coordination
            ParseCoord();

            //Parse the command
            ProcessCommand();

            LineNum = GetCurrentLineNumber();

        }
        public GCodeLine(string l, GCodeLine prevLn)
        {
            inputLine = localLine = l;

            CNCPoint curPosition = prevLn.ToPosition;

            FromPosition = curPosition;

            if (inputLine.Trim().StartsWith("("))
            {

            }

            //Remove line number, if presents
            RemoveLineNumber();
            //Remove comments
            RemoveComments();

            curX = curPosition.x.ToString();
            curY = curPosition.y.ToString();
            curZ = curPosition.z.ToString();

            ParseCoord();

            ProcessCommand();

            this.prev = prevLn;

            prevLn.next = this;

            LineNum = GetCurrentLineNumber();
        }
        //public GCodeLine(string l, GCodeLine prv)
        //{
        //    inputLine = l;

        //    localLine = inputLine;

        //    this.prev = prv;

        //    if (prv != null)
        //        prv.next = this;

        //    Init();
        //}

        #endregion

        public GCodeLine AdjustZ(double z)
        {
            if (z == 0)
            {
                return this;
            }
            string gcode = this.gCommand;
            if (this.xPresent)
            {
                gcode += "X" + this.X.ToString("0.000");
            }
            if (this.yPresent)
            {
                gcode += "Y" + this.Y.ToString("0.000");
            }
            gcode += "Z" + (this.Z + z).ToString("0.000");
            if (this.fPresent)
            {
                gcode += "F" + this.F.ToString("0.000");
            }
            return new GCodeLine(gcode);
        }
        /// <summary>
        /// Is the line G Command
        /// </summary>
        /// <returns></returns>
        public bool IsG()
        {
            bool isG = false;
            if (gCommand.Length > 0)
            {
                isG = true;
            }
            return isG;
        }

        /// <summary>
        /// If the line M Command
        /// </summary>
        /// <returns></returns>
        public bool IsM()
        {
            bool isM = false;
            if (mCommand.Length > 0)
            {
                isM = true;
            }
            return isM;
        }

        /// <summary>
        /// Remove the line number if presents
        /// </summary>
        private void RemoveLineNumber()
        {
            Match m = nCommR.Match(localLine);
            localLine = nCommR.Replace(localLine, "").Trim();
        }

        /// <summary>
        /// Remove comment if presents
        /// </summary>
        private void RemoveComments()
        {
            string ln = inputLine.Trim();

            if (ln.Trim().StartsWith("("))
            {
                comment = ln;
                return;
            }

            foreach (string commentSignbol in commentSignbols)
            {
                int idx = ln.IndexOf(commentSignbol);

                if (idx > -1)
                {
                    comment = ln.Substring(idx + 1);
                    ln = ln.Substring(0, idx).Trim();

                    break;
                }
            }
            if (ln.Length == 0)
            {
                return;
            }
            else
            {
                localLine = ln;
            }
        }

        public string GetGCodeWithFeedRate(double feedrate)
        {
            Match fm = fRateR.Match(inputLine);
            string newFeedRateStr = " F" + Utils.FormatFloatDisplay(feedrate);
            if (fm.Success)
            {
                fRateR.Replace(inputLine, newFeedRateStr);
            }
            else
            {
                inputLine += newFeedRateStr;
            }
            return inputLine;
        }


        public bool IsSupported { get; set; }

        /// <summary>
        /// Parse X, Y, Z, F, I and J
        /// </summary>
        private void ParseCoord()
        {
            //Find if there is any x, y, z or f parameters
            Match xm = xPosR.Match(localLine);
            Match ym = yPosR.Match(localLine);
            Match zm = zPosR.Match(localLine);
            Match fm = fRateR.Match(localLine);
            Match im = iR.Match(localLine);
            Match jm = jR.Match(localLine);

            //If x parameter found
            if (xm.Success)
            {
                xPresent = true;
                //Set local x parameter
                x = xm.Value.Substring(1).Trim();
                //Set global current x
                curX = x;
                //Set IsPosition Flag
                isPosition = true;
            }
            else //set the x value to global current x
            {
                if (curX.Length > 0)
                {
                    x = curX;
                }

            }

            if (ym.Success)
            {
                yPresent = true;
                y = ym.Value.Substring(1).Trim();
                curY = y;
                isPosition = true;
            }
            else
            {
                if (curY.Length > 0)
                {
                    y = curY;
                }
            }

            if (zm.Success)
            {
                zPresent = true;
                z = zm.Value.Substring(1).Trim();
                curZ = z;
                isPosition = true;
            }
            else
            {
                if (curZ.Length > 0)
                {
                    z = curZ;
                }
            }

            if (x.Length == 0 || y.Length == 0 || z.Length == 0)
            {
                xDir = curXDir = -1;
                yDir = curYDir = -1;
                zDir = curZDir = -1;

                x = curX = "0";
                y = curY = "0";
                z = curZ = "0";

                FromPosition = new CNCPoint(0, 0, 0);
            }

            ToPosition = new CNCPoint(x, y, z);

            if (fm.Success)
            {
                fPresent = true;
                f = fm.Value.Substring(1).Trim();
                curF = f;
            }
            else
            {
                if (curF.Length > 0)
                    f = curF;
            }

            if (im.Success && jm.Success)
            {
                isArc = true;
            }
        }

        private void Init()
        {

            //           RemoveLineNumber();

            //            RemoveComments();

            ParseCoord();

            ProcessCommand();

            GetCurrentDir();

        }

        public string GatCurrentFeedRate()
        {
            return f;
        }

        public void SetInitialPosition()
        {
            xDir = curXDir = -1;
            yDir = curYDir = -1;
            zDir = curZDir = -1;

            x = curX = "0";
            y = curY = "0";
            z = curZ = "0";
        }

        #region " Backlash and Direction Calculation "

        /// <summary>
        /// X Direction backlash (mm)
        /// </summary>
        public static float xLash = 0.2f;

        /// <summary>
        /// Y Direction backlash (mm)
        /// </summary>
        public static float yLash = 0.2f;

        /// <summary>
        /// Z Direction backlash (mm) 
        /// </summary>
        public static float zLash = 0.0f;

        /// <summary>
        /// Backlash compensation feedrate
        /// </summary>
        private float lashRate = 100f;

        public bool _enableBacklash = false;

        public bool EnableBacklash
        {
            set
            {
                _enableBacklash = value;
            }
            get
            {
                return _enableBacklash;
            }
        }
        public void GetPrevDir()
        {

        }

        public void GetCurrentDir()
        {
            xDir = 0;
            yDir = 0;
            zDir = 0;

            if (prev == null)
            {
                return;
            }

            //Only if there is valid value in both current and prev x position
            if (x.Length > 0 && prev.x.Length > 0)
            {
                if (float.Parse(x) > float.Parse(prev.x))
                {
                    xDir = 1;
                }
                else if (float.Parse(x) < float.Parse(prev.x))
                {
                    xDir = -1;
                }
                if (xDir != 0)
                    curXDir = xDir;
                else
                    xDir = curXDir;
            }
            else
            {
                xDir = curXDir;
            }

            if (y.Length > 0 && prev.y.Length > 0)
            {
                if (float.Parse(y) > float.Parse(prev.y))
                {
                    yDir = 1;
                }
                else if (float.Parse(y) < float.Parse(prev.y))
                {
                    yDir = -1;
                }
                if (yDir != 0)
                    curYDir = yDir;
                else
                    yDir = curYDir;
            }
            else
            {
                yDir = curYDir;
            }

            if (z.Length > 0 && prev.z.Length > 0)
            {
                if (float.Parse(z) > float.Parse(prev.z))
                {
                    zDir = 1;
                }
                else if (float.Parse(z) < float.Parse(prev.z))
                {
                    zDir = -1;
                }
                if (zDir != 0)
                    curZDir = zDir;
                else
                    zDir = curZDir;
            }
            else
            {
                zDir = curZDir;
            }

        }

        /// <summary>
        /// Calculate X Direction from the previous position
        /// </summary>
        /// <returns></returns>
        public int CurrentXDir()
        {
            int dir = 0;
            GCodeLine p = this.prev;
            while (p != null)
            {
                if (p.xDir != 0)
                {
                    dir = p.xDir;
                    break;
                }
                p = p.prev;
            }

            return dir;
        }
        public int CurrentYDir()
        {
            int dir = 0;
            GCodeLine p = this.prev;
            while (p != null)
            {
                if (p.yDir != 0)
                {
                    dir = p.yDir;
                    break;
                }
                p = p.prev;
            }

            return dir;
        }
        public int CurrentZDir()
        {
            int dir = 0;
            GCodeLine p = this.prev;
            while (p != null)
            {
                if (p.zDir != 0)
                {
                    dir = p.zDir;
                    break;
                }
                p = p.prev;
            }

            return dir;
        }

        public void GetDirChange(ref int xChange, ref int yChange, ref int zChange)
        {
            xChange = 0;
            yChange = 0;
            zChange = 0;
            if (prev != null)
            {
                if (this.prev.xDir * xDir < 0)
                {
                    xChange = xDir;
                }
                if (this.prev.yDir * yDir < 0)
                {
                    yChange = yDir;
                }
                if (this.prev.zDir * zDir < 0)
                {
                    zChange = zDir;
                }
            }
        }

        public ArrayList AddBacklashGCode()
        {
            ArrayList codeList = new ArrayList();

            int xChg = 0;
            int yChg = 0;
            int zChg = 0;

            this.GetDirChange(ref xChg, ref yChg, ref zChg);

            if (xChg != 0 || yChg != 0 || zChg != 0)
            {
                codeList.Add("G91");

                string backlashCompCode = gCommand + " ";
                if (xChg != 0 && xLash != 0)
                {
                    string tempX = xLash.ToString();

                    if (xChg < 0)
                    {
                        tempX = "-" + tempX;
                    }

                    backlashCompCode += "X" + tempX + " ";
                }
                if (yChg != 0 && yLash != 0)
                {
                    string tempY = yLash.ToString();

                    if (yChg < 0)
                    {
                        tempY = "-" + tempY;
                    }

                    backlashCompCode += "Y" + tempY + " ";
                }
                if (zChg != 0 && zLash != 0)
                {
                    string tempZ = zLash.ToString();

                    if (zChg < 0)
                    {
                        tempZ = "-" + tempZ;
                    }

                    backlashCompCode += "Z" + tempZ + " ";
                }

                backlashCompCode += " F" + lashRate;
                codeList.Add("G1 " + backlashCompCode);
                codeList.Add("G90");

                string g92 = "G92 X%X% Y%Y% Z%Z%";
                g92 = g92.Replace(@"%X%", prev.x);
                g92 = g92.Replace(@"%Y%", prev.y);
                g92 = g92.Replace(@"%Z%", prev.z);
                codeList.Add(g92);

            }

            return codeList;
        }

        #endregion

        public GCodeLine Head()
        {
            GCodeLine head = this;
            while (head.prev != null)
            {
                head = head.prev;
            }
            return head;
        }

        public float MinX
        {
            get
            {
                GCodeLine curLine = this.Head();
                float minX = (float)curLine.X;
                while (curLine.Next() != null)
                {
                    if (curLine.IsPositionCode)
                    {
                        if (curLine.X < minX)
                        {
                            minX = (float)curLine.X;
                        }
                    }
                    curLine = curLine.Next();
                }
                return minX;
            }
        }
        public float MaxX
        {
            get
            {
                GCodeLine curLine = this.Head();
                float maxX = (float)curLine.X;
                while (curLine.Next() != null)
                {
                    if (curLine.IsPositionCode)
                    {
                        if (curLine.X > maxX)
                        {
                            maxX = (float)curLine.X;
                        }
                    }
                    curLine = curLine.Next();
                }
                return maxX;
            }
        }

        public float MinY
        {
            get
            {
                GCodeLine curLine = this.Head();
                float minY = (float)curLine.Y;
                while (curLine.Next() != null)
                {
                    if (curLine.IsPositionCode)
                    {
                        if (curLine.Y < minY)
                        {
                            minY = (float)curLine.Y;
                        }
                    }
                    curLine = curLine.Next();
                }
                return minY;
            }
        }
        public float MaxY
        {
            get
            {
                GCodeLine curLine = this.Head();
                float maxY = (float)curLine.Y;
                while (curLine.Next() != null)
                {
                    if (curLine.IsPositionCode)
                    {
                        if (curLine.Y > maxY)
                        {
                            maxY = (float)curLine.Y;
                        }
                    }
                    curLine = curLine.Next();
                }
                return maxY;
            }
        }

        public float MinZ
        {
            get
            {
                GCodeLine curLine = this.Head();
                float minZ = (float)curLine.Z;
                while (curLine.Next() != null)
                {
                    if (curLine.IsPositionCode)
                    {
                        if (curLine.Z < minZ)
                        {
                            minZ = (float)curLine.Z;
                        }
                    }
                    curLine = curLine.Next();
                }
                return minZ;
            }
        }
        public float MaxZ
        {
            get
            {
                GCodeLine curLine = this.Head();
                float maxZ = (float)curLine.Z;
                while (curLine.Next() != null)
                {
                    if (curLine.IsPositionCode)
                    {
                        if (curLine.Z > maxZ)
                        {
                            maxZ = (float)curLine.Z;
                        }
                    }
                    curLine = curLine.Next();
                }
                return maxZ;
            }
        }
        /// <summary>
        /// Get Movement information for display
        /// </summary>
        /// <returns></returns>
        public string GetComments()
        {
            string cmt = localLine + ";";
            if (IsG())
                cmt += "G:";
            if (IsM())
                cmt += "M:";

            int xChg = 0;
            int yChg = 0;
            int zChg = 0;

            GetDirChange(ref xChg, ref yChg, ref zChg);

            cmt += "x=" + x + ";y=" + y + ";z=" + z + ":";

            if (xChg != 0)
                cmt += "X" + GetSign(xChg);
            if (yChg != 0)
                cmt += "Y" + GetSign(yChg);
            if (zChg != 0)
                cmt += "Z" + GetSign(zChg);

            return cmt;

        }
        public int Count()
        {
            int cnt = 1;
            GCodeLine p = this.next;
            while (p != null)
            {
                cnt++;
                p = p.next;
            }
            return cnt;
        }
        public bool IsPositionCode
        {
            get
            {
                return isPosition;
            }
        }
        public int PositionCount()
        {
            int cnt = 0;
            GCodeLine p = this;
            while (p != null)
            {
                if (p.isPosition)
                {
                    cnt++;
                }
                p = p.Next();
            }
            return cnt;
        }
        public void PrintAll()
        {
            GCodeLine cur = this.Head();

            Console.WriteLine(cur.Count());
            //Console.WriteLine(cur.PrintPosition());
            while (cur.next != null)
            {
                cur = cur.next;
                Console.WriteLine(cur.PrintPosition());
            }
        }
        private string GetSign(int x)
        {
            if (x > 0)
                return "+";
            else if (x < 0)
                return "-";
            else
                return "";
        }
        public string PrintPosition()
        {
            return x + "," + y + "," + z;
        }
        public void GetBoundaryBox(out float xmin, out float xmax, out float ymin, out float ymax)
        {
            xmin = 0;
            xmax = 0;
            ymin = 0;
            ymax = 0;

            GCodeLine head = this.Head();

            while (head != null)
            {
                if (!head.isPosition)
                {
                    head = head.next;
                    continue;
                }
                float xd = float.Parse(head.x);
                float yd = float.Parse(head.y);
                float zd = float.Parse(head.z);

                if (xd < xmin)
                {
                    xmin = xd;
                }
                if (xd > xmax)
                {
                    xmax = xd;
                }
                if (yd < ymin)
                {
                    ymin = yd;
                }
                if (yd > ymax)
                {
                    ymax = yd;
                }

                head = head.next;
            }
        }
        private double CalDistance(GCodeLine p1, GCodeLine p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2));
        }
        public void CalculateTravelDist()
        {
            if (prev == null)
            {
                this.traveledDist = 0;
            }
            else
            {
                this.traveledDist = prev.traveledDist +
                    Math.Sqrt(Math.Pow(this.X - prev.X, 2) + Math.Pow(this.Y - prev.Y, 2) + Math.Pow(this.Z - prev.Z, 2));
            }
        }
        public GCodeLine Next()
        {
            return next;
        }
        public GCodeLine Prev()
        {
            return prev;
        }

    }
}
