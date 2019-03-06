using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class GCodeFile
    {
        //File Name
        private string _filename;

        //File Stream which remains open while executing gcode
        private FileStream stream;

        //Stream for gcode file continous reading
        private StreamReader stream_in = null;

        //Buffer counter for grbl buffer limit
        private Queue<int> _counter = new Queue<int>();

        private int queueTotal = 0;

        //MAX buffer size can be handled by grbl
        private int MAX_BUFFER = 127;

        //Length of the gcode file
        private long fileLength = 0;

        private StringBuilder sb = new StringBuilder();

        //Indicator whether the end of file reached
        private bool isEnd = false;

        private List<string> lines;

        public int CurrentLineNum { get; set; }

        /// <summary>
        /// Reset GCodeFile status, and clear all lines in memory
        /// </summary>
        public void Clear()
        {
            if (stream != null)
            {
                stream.Close();
            }

            _counter = new Queue<int>();
            queueTotal = 0;
            isEnd = false;
            Status = GCodeFileStatusEnum.NoFile;
            currentPosition = 0;
            CurrentLine = 0;
            CurrentLineNum = 0;
            lines = null;

        }

        /// <summary>
        /// GCode File Name
        /// </summary>
        public string FileName
        {
            get
            {
                return _filename;
            }
        }

        /// <summary>
        /// Total lines of the GCode File
        /// </summary>
        public int TotalLines { get; set; }

        /// <summary>
        /// Current executing line
        /// </summary>
        public int CurrentLine { get; set; }


        public string FilePath { get; set; }

        public void CommandOK()
        {
            if (_counter.Count > 0)
            {
                int deqCnt = (int)_counter.Dequeue();
                queueTotal -= deqCnt;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GCodeFile()
        {
            _filename = string.Empty;
            TotalLines = -1;
            CurrentLine = -1;
            Status = GCodeFileStatusEnum.NoFile;
        }

        Regex regexXVal = new Regex(@"\s?X\d*\.?\d*");
        Regex regexYVal = new Regex(@"\s?Y\d*\.?\d*");
        Regex regexZVal = new Regex(@"\s?Z\d*\.?\d*");
        Regex regexIVal = new Regex(@"\s?I\d*\.?\d*");
        Regex regexJVal = new Regex(@"\s?J\d*\.?\d*");


        //GCode Lines saved in memory after file reading
        List<GCodeLine> gcodeLines;

        public void AddGCodeLine(string ln)
        {
            if (gcodeLines == null)
                gcodeLines = new List<GCodeLine>();
            ln = ln.Trim();

            if (gcodeLines.Count == 0)
            {
                GCodeLine newLn = new GCodeLine(ln.Trim());
                newLn.SetInitialPosition();
                gcodeLines.Add(newLn);
            }
            else
            {

                GCodeLine prevLn = gcodeLines[gcodeLines.Count - 1];
                GCodeLine newLn = new GCodeLine(ln.Trim(), prevLn);
                gcodeLines.Add(newLn);
            }
        }

        GCodeLine curLine = null;

        public void OpenFileForContiniourReading(string filename)
        {
            if(filename == null || filename.Length == 0)
            {
                return;
            }
            stream_in = new StreamReader(filename);
            using (StreamReader sr = new StreamReader(filename))
            {
                string content = sr.ReadToEnd();
                sr.Close();
                string[] lns = content.Split('\n');
                TotalLines = lns.Length;
                CurrentLineNum = 0;
                content = string.Empty;
            }

            FilePath = filename;
            Status = GCodeFileStatusEnum.Loaded;
        }

        public GCodeLine ReadNextGCodeLine()
        {
            GCodeLine gcodeLn = null;
            string ln = stream_in.ReadLine();
            if (ln == null)
            {
                ln = "EOF";
                CurrentLineNum++;
            }
            else
            {
                gcodeLn = new GCodeLine(ln);
                CurrentLineNum++;
            }

            return gcodeLn;
        }

        /// <summary>
        /// Open the gcode file
        /// </summary>
        /// <param name="filename"></param>
        public void OpenFile(string filename)
        {

            //Set the file name
            _filename = filename;

            //If the file exist
            if (File.Exists(filename))
            {

                gcodeLines = new List<GCodeLine>();
                ///Open the file and get the line count
                using (StreamReader sr = new StreamReader(_filename))
                {
                    string content = sr.ReadToEnd();
                    sr.Close();
                    string[] lns = content.Split('\n');
                    lines = new List<string>();
                    foreach (string ln in lns)
                    {
                        AddGCodeLine(ln);
                        lines.Add(ln.Trim());
                    }
                    TotalLines = lns.Length;
                    CurrentLineNum = 0;
                    content = string.Empty;

                    sr.Close();
                }

                curLine = gcodeLines[0];

                //stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                //fileLength = stream.Length;

                Status = GCodeFileStatusEnum.Loaded;

            }
            else
            {
                Status = GCodeFileStatusEnum.Error;
            }

            ResetSeek();
        }


        public GCodeLine GetStartLine()
        {
            GCodeLine line = curLine;
            while (!line.IsSupported)
            {
                line = line.next;
            }
            curLine = line;
            return curLine;
        }

        //Current file stream seek position
        private int currentPosition = 0;

        /// <summary>
        /// For continious reading, set the seek position to begining
        /// </summary>
        public void ResetSeek()
        {
            currentPosition = 0;
            if (stream != null)
                stream.Seek(currentPosition, SeekOrigin.Begin);

            isEnd = false;
            queueTotal = 0;
        }

        static object lockNextLine = new object();

        public GCodeLine NextSupportedLine()
        {
            GCodeLine ln = curLine.next;
            while (ln != null && !ln.IsSupported)
            {
                ln = ln.next;
            }
            curLine = ln;
            return curLine;
        }

        //Get next line from the gcode file stream
        public string NextLine()
        {
            if (isEnd)
            {
                return "EOF";
            }

            lock (lockNextLine)
            {
                if (CurrentLineNum < TotalLines)
                {
                    string retLine = lines[CurrentLineNum];
                    CurrentLineNum++;
                    return retLine;
                }
                else
                {
                    isEnd = true;
                    return "EOF";
                }
            }
            //The counter of line characters, initialize to 0
            int readCnt = 0;
            //Initialize the output line
            string result = string.Empty;

            //Read buffer
            byte[] b = new byte[100];

            //Read from the stream
            int charRead = stream.Read(b, 0, b.Length);

            for (int i = 0; i < charRead; i++)
            {
                //If the total length greater than the file length, file end reached
                if (currentPosition + readCnt >= fileLength)
                {
                    //Set isEnd to true
                    isEnd = true;
                    //Construct the result string
                    result = sb.ToString().Trim();
                    //Clean the string buffer
                    sb.Clear();
                    //end the search loop
                    break;
                }

                //Get the char from the buffer array for checking line break
                char c = Convert.ToChar(b[i]);

                //read until the line break reached
                if (c == '\n' || c == '\r')
                {
                    if (c == '\r' && i < b.Length)
                    {
                        char cn = Convert.ToChar(b[i + 1]);
                        if (cn == '\n')
                        {
                            readCnt++;
                        }
                    }
                    //Constructor the result string from the string builder
                    result = sb.ToString().Trim();
                    //Empty string build for the next line
                    sb.Clear();
                    readCnt++;
                    //Break the character loop to return the line
                    break;
                }
                else  //If the character is not line break, continue appending the char to string builder
                {
                    sb.Append(c);
                    readCnt++;
                }
            }


            //If the total buffer size less than grbl MAX buffer, the line will be sent, set seek position
            int addedTotal = queueTotal + result.Length + 1;

            if (queueTotal + result.Length <= MAX_BUFFER)
            {
                currentPosition += readCnt;
                queueTotal += result.Length + 1;

                stream.Seek(currentPosition, SeekOrigin.Begin);
                _counter.Enqueue(result.Length + 1);
            }
            //If the total buffer size reached the limit, the current line will be ignored and return empty string,
            //the grbl will not send empty line
            else
            {
                stream.Seek(currentPosition, SeekOrigin.Begin);
                result = string.Empty;
            }

            return result;
        }

        public string NextLineNew()
        {
            if (isEnd)
            {
                return "EOF";
            }

            //The counter of line characters, initialize to 0
            int readCnt = 0;

            //Initialize the output line
            string result = string.Empty;

            //Read buffer
            byte[] b = new byte[100];

            //Read from the stream
            int charRead = stream.Read(b, 0, b.Length);

            for (int i = 0; i < charRead; i++)
            {
                //If the total length greater than the file length, file end reached
                if (currentPosition + readCnt >= fileLength)
                {
                    //Set isEnd to true
                    isEnd = true;
                    //Construct the result string
                    result = sb.ToString().Trim();
                    //Clean the string buffer
                    sb.Clear();
                    //end the search loop
                    break;
                }

                //Get the char from the buffer array for checking line break
                char c = Convert.ToChar(b[i]);

                //read until the line break reached
                if (c == '\n' || c == '\r')
                {
                    if (c == '\r' && i < b.Length)
                    {
                        char cn = Convert.ToChar(b[i + 1]);
                        if (cn == '\n')
                        {
                            readCnt++;
                        }
                    }
                    //Constructor the result string from the string builder
                    result = sb.ToString().Trim();
                    //Empty string build for the next line
                    sb.Clear();
                    readCnt++;
                    //Break the character loop to return the line
                    break;
                }
                else  //If the character is not line break, continue appending the char to string builder
                {
                    sb.Append(c);
                    readCnt++;
                }
            }


            //If the total buffer size less than grbl MAX buffer, the line will be sent, set seek position
            int addedTotal = queueTotal + result.Length + 1;

            if (queueTotal + result.Length <= MAX_BUFFER)
            {
                currentPosition += readCnt;
                queueTotal += result.Length + 1;

                stream.Seek(currentPosition, SeekOrigin.Begin);
                _counter.Enqueue(result.Length + 1);
            }
            //If the total buffer size reached the limit, the current line will be ignored and return empty string,
            //the grbl will not send empty line
            else
            {
                stream.Seek(currentPosition, SeekOrigin.Begin);
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// Close the file stream
        /// </summary>
        public void Close()
        {
            if (stream != null)
                stream.Close();

            _counter.Clear();
            queueTotal = 0;
        }

        public GCodeFileStatusEnum Status { get; set; }

        /// <summary>
        /// Get the verbose description of the File status
        /// </summary>
    }

    //GCode file status ENUM
    public enum GCodeFileStatusEnum
    {
        NoFile, //No file has been opened
        Loaded, //File has been loaded
        Runing, //File is Running
        Paused, //File is Paused
        Stopped, //File is stopped
        Error //Error in File, either running error or format error
    }
}
