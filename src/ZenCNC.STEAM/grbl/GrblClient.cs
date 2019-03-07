using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ZenCNC.STEAM.grbl
{
    /// <summary>
    /// Main Grbl client class
    /// </summary>
    public class GrblClient
    {
        #region "Private variables"
        //Grbl Serial Port
        private SerialPort grblPort;

        public PortDesc portDesc = null;

        //Error description hashtable
        private static Hashtable errorHash;

        //Alarm description hashtable
        private static Hashtable alarmHash;

        //GCode file
        private GCodeFile gcodeFile = new GCodeFile();

        //Locking object for accessing
        private readonly static object lockObject = new object();

        //If GCode File
        public bool IsGCodeLoaded = false;

        //If Pause triggered
        private bool IsPause = false;

        //If need to query
        private bool doQuery = true;

        public bool IsConnected = false;

        private bool CommandComplted = true;

        private Queue gcodeQueue = new Queue();

        private string lastLineSent = string.Empty;

        private float oldFeedRate = 0f;

        private float oldSpindleRPM = 0f;

        private MachineState oldState = MachineState.NOTCONNECTED;

        private Position oldPosition = new Position() { X = 0, Y = 0, Z = 0 };

        private Position oldWCO = new Position() { X = 0, Y = 0, Z = 0 };

        private Position prbPosition = new Position() { X = 0, Y = 0, Z = 0 };

        private SortedDictionary<string, string> parameterHash = new SortedDictionary<string, string>();

        private Queue<byte> receivedData = new Queue<byte>(); //Queue handles received bytes

        private Queue<string> responses = new Queue<string>(); //Queue handles received response string

        private GrblResponseRouter respRouter = new GrblResponseRouter(ActionFlowType.Default);

        private float _jogFeedrate = 300;

        GrblGCodeStatusEventArgs gCodeArgs = new GrblGCodeStatusEventArgs();

        GrblResponseEventArgs respArgs = new GrblResponseEventArgs();

        //Query interval in miliseconds
        private int QUERY_INTERVAL = 1000;

        private char interruptChar;

        static int FAST_INTERVAL = 100;
        static int SLOW_INTERVAL = 1000;

        private Thread queryThread = null;


        private DelayedCommand DelayedCommand = null;

        private void ExecuteDelayedCommand()
        {
            if (DelayedCommand != null && DelayedCommand.IsTimeExpried())
            {
                Send(DelayedCommand.Command);
                DelayedCommand = null;
            }
        }

        #endregion

        #region "Private Functions"
        public void SetFastInterval()
        {
            QUERY_INTERVAL = FAST_INTERVAL;
        }
        public void SetSlowInterval()
        {
            QUERY_INTERVAL = SLOW_INTERVAL;
        }

        public float JogFeedRate
        {
            get
            {
                return _jogFeedrate;
            }
            set
            {
                this._jogFeedrate = value;
            }
        }
        #endregion
        /// <summary>
        /// Constructor
        /// </summary>
        public GrblClient()
        {
            //Open grbl configuration file
            XmlDocument xmlDoc = new XmlDocument();

            string grblVersion = "1.1f";
            string[] resources = this.GetType().Assembly.GetManifestResourceNames();
            string resource = string.Empty;
            foreach(var res in resources)
            {
                if(res.ToUpper().IndexOf(grblVersion.ToUpper()) >= 0)
                {
                    resource = res;
                    break;
                }
            }
            if(resource.Length > 0)
            {
                string grblXmlStr = string.Empty;
                using (Stream stream = this.GetType().Assembly.
                    GetManifestResourceStream(resource))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        grblXmlStr = sr.ReadToEnd();
                        xmlDoc.LoadXml(grblXmlStr);
                    }
                }
            }
            else
            {
                throw new Exception("No GRBL Definition File Found");
            }

            //xmlDoc.Load("config\\grbl_v1.1f.xml");
            //For each parameter defined in config file

            errorHash = new Hashtable();
            foreach (XmlNode errorNode in xmlDoc.SelectNodes("/grbl/Errors/Error"))
            {
                //error code
                string code = errorNode.Attributes["code"].Value;
                //error description
                string desc = errorNode.InnerText;
                //Set error hash
                errorHash.Add(code, desc);
            }
            alarmHash = new Hashtable();
            foreach (XmlNode errorNode in xmlDoc.SelectNodes("/grbl/Alarms/Alarm"))
            {
                //error code
                string code = errorNode.Attributes["code"].Value;
                //error description
                string desc = errorNode.InnerText;
                //Set error hash
                alarmHash.Add(code, desc);
            }
        }



        public void SaveMachineAs(string machinename, string machineid, string machinemodel)
        {
            List<GrblParameterBase> currentParameters = this.GetParameters();
            XDocument xml = new XDocument();
            
            XElement root = new XElement("Machine");
            xml.Add(root);
            root.Add(new XAttribute("id", machineid));
            root.Add(new XAttribute("name", machinename));
            root.Add(new XAttribute("model", machinemodel));
            XElement parameters =  new XElement("Parameters");
            root.Add(parameters);
            foreach(var para in currentParameters)
            {
                XElement paramElem = new XElement("Parameter");
                paramElem.Add(new XAttribute("id", para.ID));
                paramElem.Add(new XAttribute("desc", para.Desc));
                paramElem.Add(new XAttribute("type", para.ValueType));
                paramElem.Add(new XAttribute("value", para.Val));
                parameters.Add(paramElem);
            }
            string file = "config\\machines\\machine_" + machinename + ".xml";
            if(File.Exists(file))
            {
                string filePattern = "machine_"+machinename + "*.xml";
                string directory = "config\\machines";
                string[] files = Directory.GetFiles(directory, filePattern);
                file = file.Replace(".xml", "_" + (files.Length) + ".xml");
            }
            xml.Save(file);
        }
        string machinesConfig = "config\\machines.xml";

        public List<string> GetAllMachineNames()
        {
            List<string> machineNames = new List<string>();

            string[] resources = this.GetType().Assembly.GetManifestResourceNames();
            string resource = string.Empty;
            foreach (var res in resources)
            {
                if (res.IndexOf("machine_") >= 0)
                {
                    XmlDocument machineXml = LoadMachineByResourceName(res);
                    string name = machineXml.SelectSingleNode("/Machine").Attributes["name"].Value;
                    machineNames.Add(name);
                }
            }

            return machineNames;
        }
        
        public string GetResourceByMachineName(string machinename)
        {
            string resource = string.Empty;
            string[] resources = this.GetType().Assembly.GetManifestResourceNames();
            foreach (var res in resources)
            {
                if (res.IndexOf("machine_") >= 0)
                {
                    XmlDocument machineXml = LoadMachineByResourceName(res);
                    string name = machineXml.SelectSingleNode("/Machine").Attributes["name"].Value;
                    if(name.Equals(machinename))
                    {
                        resource = res;
                        break;
                    }
                }
            }
            return resource;
        }
        public XmlDocument LoadMachineByName(string name)
        {
            string resource = GetResourceByMachineName(name);
            if(resource.Length > 0)
            {
                return LoadMachineByResourceName(resource);
            }
            return null;
        }
        public XmlDocument LoadMachineByResourceName(string resource)
        {
            XmlDocument machineXml = new XmlDocument();
            string xmlStr = string.Empty;
            using (Stream stream = this.GetType().Assembly.
                GetManifestResourceStream(resource))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    xmlStr = sr.ReadToEnd();
                    machineXml.LoadXml(xmlStr);
                }
            }
            return machineXml;
        }

        public List<string> GetAllMachines()
        {
            List<string> machines = new List<string>();
            XmlDocument xml = new XmlDocument();
            if(!File.Exists(machinesConfig))
            {
                return machines;
            }
            xml.Load(machinesConfig);
            XmlNodeList list = xml.SelectNodes("/Machines/Machine");
            foreach(XmlNode node in list)
            {
                string name = node.Attributes["name"].Value;
                machines.Add(name);
            }
            return machines;
        }
        public void LoadMachine(string name)
        {
            XmlDocument machineXml = new XmlDocument();
            machineXml.Load(machinesConfig);
            XmlNode machineConfig = machineXml.SelectSingleNode("/Machines/Machine[@name=\"" + name + "\"]");
            if(machineConfig != null)
            {
                XmlNodeList attrList = machineConfig.SelectNodes("Parameters/Parameter");
                foreach(XmlNode attrNode in attrList)
                {
                    string id = attrNode.Attributes["id"].Value;
                    string val = attrNode.Attributes["value"].Value;
                    UpdateParameter(id, val);
                    Thread.Sleep(200);
                }
            }
        }
        /// <summary>
        /// Load a gcode file
        /// </summary>
        /// <param name="fileName">GCode file path</param>
        public void OpenGCode(string fileName)
        {
            gcodeFile.Clear();

            //            gcodeFile.OpenFile(fileName);

            gcodeFile.OpenFileForContiniourReading(fileName);

            IsGCodeLoaded = true;

            gCodeArgs.TotalLine = gcodeFile.TotalLines;
            gCodeArgs.CurrentLine = 0;
            gCodeArgs.FileStatus = FileStatus.Loaded;
            //Raised grbl helper event
            GCodeStatusUpdate(this, gCodeArgs);
        }

        /// <summary>
        /// Start to run a gcode file, with option setting current location to Zero
        /// </summary>
        /// <param name="resetZero"></param>
        public void StartFile(bool resetZero)
        {
            if (resetZero)
            {
                Send("G92X0Y0Z0");
                StartFile();
            }
            else
            {
                StartFile();
            }
        }

        /// <summary>
        /// Start to run a GCode file, if the file has been loaded
        /// </summary>
        public void StartFile()
        {
            if(this.gcodeFile == null)
            {
                return;
            }
            gCodeArgs.FileStatus = FileStatus.Running;
            //GCodeLine startLine = gcodeFile.GetStartLine();
            //Send(startLine.inputLine);

            GCodeLine startLn = gcodeFile.ReadNextGCodeLine();
            gcodeFile.CurrentLine++;
            if (startLn == null)
            {
                string filePath = gcodeFile.FilePath;
                OpenGCode(filePath);
                startLn = gcodeFile.ReadNextGCodeLine();
            }
            if (startLn.IsSupported)
            {
                Send(startLn.inputLine);
            }
            else
            {
                Send("?");
            }

        }

        /// <summary>
        /// Get all arduino ports
        /// </summary>
        /// <returns></returns>
        public static List<PortDesc> GetSerialPorts()
        {
            List<PortDesc> result = new List<PortDesc>();
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            //            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM WIN32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();

                //if(portnames.Length > 0)
                //{
                //    result.Add(new PortDesc() { DeviceId = portnames[0], Caption = "Unknown" });
                //    return result;
                //}
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();

                foreach (var port in ports)
                {
                    string caption = port["Caption"].ToString();
                    int idx1 = caption.IndexOf("(");
                    int idx2 = caption.LastIndexOf(")");
                    int len = idx2 - idx1 - 1;

                    string portName = caption.Substring(idx1 + 1, len);
                    if (port["Caption"].ToString().ToUpper().Contains("ARDUINO")
                        || port["Caption"].ToString().ToUpper().Contains("CH340"))
                    {
                        foreach (string testName in portnames)
                        {
                            if (testName.ToUpper().Equals(portName))
                            {
                                result.Add(new PortDesc() { DeviceId = portName, Caption = port["Caption"].ToString() });
                                break;
                            }
                        }

                    }

                }
            }

            return result;
        }

        /// <summary>
        /// Close Serial Port
        /// </summary>
        public void Close()
        {

            if (grblPort != null && grblPort.IsOpen)
            {
                if (queryThread.IsAlive)
                    queryThread.Abort();
                grblPort.Close();
                grblPort.Dispose();
            }
        }
        /// <summary>
        /// Open grbl port
        /// </summary>
        /// <param name="portDesc">port description object</param>
        /// <returns></returns>
        public bool Open(PortDesc portDesc)
        {

            bool IsConnected = false;



            //Initiate parameter descriptions and types
            GrblParameterBase.Init();

            //Initialize the serial port
            grblPort = new SerialPort(portDesc.DeviceId, 115200, Parity.None, 8, StopBits.One);

            //Setup event handler
            grblPort.DataReceived += GrblPort_DataReceived;

            grblPort.ErrorReceived += GrblPort_ErrorReceived;

            //Open the port
            if (!grblPort.IsOpen)
            {
                try
                {
                    grblPort.Open();

                    this.portDesc = portDesc;

                    System.Threading.Thread.Sleep(1000);
                    CMD_RESET();
                    System.Threading.Thread.Sleep(1000);
                    CMD_QUERY();
                }
                catch (Exception e)
                {
                    //Raise port open error event
                }
            }
            else
            {
                //Raise port already open event;
            }

            if (grblPort.IsOpen)
            {
                PortStateChangeArg arg = new PortStateChangeArg();
                arg.State = "Connected";
                OnPortEvent(arg);

                CMD_GETOFFSET();

                queryThread = new Thread(delegate ()
                {
                    StartQueryLoop();
                });

                queryThread.Start();

                IsConnected = true;
            }

            return IsConnected;
        }

        private void GrblPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Event handler for grbl port response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrblPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //Initiate a read buffer and read the data into buffer
            byte[] data = new byte[grblPort.BytesToRead];
            int readCnt = grblPort.Read(data, 0, data.Length);

            //Queue all characters into recievedData queue for processing
            data.ToList().ForEach(b => receivedData.Enqueue(b));
            //Processing data
            ProcessData();
        }

        /// <summary>
        /// Process grbl response data
        /// </summary>
        private void ProcessData()
        {
            //Console.WriteLine("ProcessData ...");
            int currentStart = 0;

            //            DisplayQueue("B:",receivedData);
            //Convert the receivedData into an array for easy processing
            //exp1: 123456\n789
            byte[] bytes = receivedData.ToArray();

            int count = 0;

            //Loop through each byte inside the array
            for (int i = 0; i < bytes.Length; i++)
            {
                //Get a line break indicator
                if (bytes[i] == '\n')
                {
                    //if the beginning of the line is a line break, 
                    //It is left over from previous line, ignore it and continue to the next
                    if (i == 0)
                    {
                        count++;
                        continue;
                    }

                    count += i - currentStart + 1;

                    //Convertthe byte array into a string
                    string response = System.Text.Encoding.UTF8.GetString(bytes, currentStart, i - currentStart);

                    currentStart = i + 1;

                    //Trime the response
                    string orig = response;
                    response = response.Trim();

                    respRouter.Route(response);

                    //If there is content inside the response string
                    if (response.Length > 0)
                    {
                        //Create a grbl update argument with response string
                        respArgs.Response = response;


                        //If received a ok, 
                        if (response.Trim().ToUpper().Equals("OK"))
                        {
                            ExecuteQueue();
                            //If the file is running, 
                            if (IsGCodeLoaded)
                            {
                                //Execute CommandOK to recalculate buffer 
                                gcodeFile.CommandOK();
                                //Send next line
                                SendNextLineNew();
                            }
                            else
                            {
                                CommandComplted = true;
                            }
                        }
                        else if (response.StartsWith("$"))
                        {
                            ProcessParameter(response);
                            continue;
                        }
                        else if (response.ToUpper().StartsWith("GRBL"))
                        {
                            CMC_QUERYPARAMS();
                        }
                        else if (response.ToUpper().StartsWith("ERROR"))
                        {
                            string[] errFlds = response.Split(':');
                            GrblErrorEventArgs errArg = new GrblErrorEventArgs();
                            errArg.Code = errFlds[1];
                            errArg.Desc = GetErrorDesc(errFlds[1]);
                            errArg.Line = lastLineSent;
                            try
                            {
                                ErrorEvent(this, errArg);
                            }
                            catch
                            {

                            }
                        }
                        else if (response.ToUpper().StartsWith("ALARM"))
                        {
                            string[] errFlds = response.Split(':');
                            OnError(new GrblErrorEventArgs() { Code = errFlds[1], Desc = GetAlarmDesc(errFlds[1]), Line = lastLineSent });
                        }

                        if (respArgs.State == MachineState.RUN ||
                            respArgs.State == MachineState.JOG)
                        {
                            //SetQueryTimerInterval(100);
                            QUERY_INTERVAL = 100;
                        }
                        else
                        {
                            //SetQueryTimerInterval(1000);
                            QUERY_INTERVAL = 100;
                        }

                        if (AnythingChanged(respArgs))
                        {
                            //                            logger.Info(">>>Changled<<<");
                            OnResponseReceived(respArgs);
                        }
                    }

                }
            }

            for (int j = 0; j < count; j++)
            {
                byte db = receivedData.Dequeue();
            }

        }

        /// <summary>
        /// Get all grbl parameters
        /// </summary>
        /// <returns>List of grbl parameter object</returns>
        public List<GrblParameterBase> GetParameters()
        {
            List<GrblParameterBase> result = new List<GrblParameterBase>();
            foreach (var entry in parameterHash)
            {
                string id = entry.Key.ToString();
                string val = entry.Value.ToString();
                GrblParameterBase param = new GrblParameterBase(id, val);
                result.Add(param);
            }

            return result.OrderBy(p => Convert.ToInt16(p.ID)).ToList<GrblParameterBase>();
        }
 
        /// <summary>
        /// Process Grbl parameter string
        /// </summary>
        /// <param name="parameterStr"></param>
        private void ProcessParameter(string parameterStr)
        {
            parameterStr = parameterStr.Substring(1);
            string[] flds = parameterStr.Split('=');
            SetParameter(flds[0].Trim(), flds[1].Trim());
            if (GrblParameterBase.IsLastParameter(flds[0]))
            {
                GrblResponseEventArgs args = new GrblResponseEventArgs() { EventType = "ParameterUpdate" };
                OnResponseReceived(args);
            }
        }

        /// <summary>
        /// Update a Grbl parameter
        /// </summary>
        /// <param name="id">Grbl parameter id</param>
        /// <param name="val">Value</param>
        public void UpdateParameter(string id, string val)
        {
            Send("$" + id + "=" + val);
            System.Threading.Thread.Sleep(500);
            CMC_QUERYPARAMS();
        }

        /// <summary>
        /// Set in memory grbl parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="val"></param>
        private void SetParameter(string id, string val)
        {
            if (!parameterHash.ContainsKey(id))
            {
                parameterHash.Add(id, val);
            }
            else
            {
                parameterHash[id] = val;
            }
        }

        /// <summary>
        /// Check any grbl state change, position, status and etc
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool AnythingChanged(GrblResponseEventArgs args)
        {
            if (args.Response.StartsWith("Grbl"))
            {
                string s1 = "1";
                return true;
            }

            bool result =
            args.MPos != oldPosition || args.Feed != oldFeedRate || args.Speed != oldSpindleRPM || args.State != oldState || args.WCO != oldWCO;
            if (args.MPos != null)
            {
                oldPosition = args.MPos;
            }
            if (args.WCO != null)
            {
                oldWCO = args.WCO;
            }
            if (args.Feed != null)
            {
                oldFeedRate = args.Feed;
            }
            if (args.Speed != null)
            {
                oldSpindleRPM = args.Feed;
            }
            if (args.State != null)
            {
                oldState = args.State;
            }
            IsConnected = true;


            return result;

        }

        /// <summary>
        /// Get Error description by grbl error code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetErrorDesc(string code)
        {
            return (string)errorHash[code];
        }

        /// <summary>
        /// Get Alarm description by grbl alarm code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetAlarmDesc(string code)
        {
            return (string)alarmHash[code];
        }

        /// <summary>
        /// Execute command in the queue
        /// </summary>
        private void ExecuteQueue()
        {
            if (this.gcodeQueue.Count > 0)
            {
                string command = (string)this.gcodeQueue.Dequeue();
                Send(command);
            }
        }

        /// <summary>
        /// Get next line from GCode file and send it to grbl port
        /// </summary>
        private void SendNextLineNew()
        {
            //
            if (IsPause)
            {
                return;
            }

            //string line = gcodeFile.NextLine();
            //GCodeLine gcodeline = gcodeFile.NextSupportedLine();

            GCodeLine gcodeline = gcodeFile.ReadNextGCodeLine();
            //GCodeLine gcodeline = gcodeFile.NextSupportedLine();

            if (gcodeline != null)
            {
                Send(gcodeline.inputLine);
                gCodeArgs.gcode = gcodeline.inputLine;
                gCodeArgs.CurrentLine = gcodeFile.CurrentLineNum;
                gCodeArgs.TotalLine = gcodeFile.TotalLines;
                GCodeStatusUpdate(this, gCodeArgs);
            }
            else
            {
                gCodeArgs.FileStatus = FileStatus.Stopped;
                GCodeStatusUpdate(this, gCodeArgs);
            }

            //if (!line.Trim().Equals("EOF") && line.Trim().Length > 0)
            //{
            //    logger.Info("Sending Line .......");
            //    Send(line);
            //    gcodeFile.CurrentLine++;
            //    gCodeArgs.gcode = line;
            //    gCodeArgs.CurrentLine = gcodeFile.CurrentLine;
            //    gCodeArgs.TotalLine = gcodeFile.TotalLines;
            //    GCodeStatusUpdate(this, gCodeArgs);
            //}
            //else if (line.Trim().Equals("EOF"))
            //{
            //    logger.Info("End of File ......");
            //    IsFile = false;
            //}
            //else if (line.Trim().Length == 0)
            //{
            //    logger.Info("Empty Line ....");
            //    return;
            //}
            //else
            //{
            //    logger.Info("Unknown ......................");
            //}
        }

        /// <summary>
        /// Send a string command
        /// </summary>
        /// <param name="cmd"></param>
        public void Send(string cmd)
        {

            if (!IsGCodeLoaded && !CommandComplted)
            {
                //throw new Exception("Command Incompleted Exceptioin");
            }
            cmd = cmd.Trim() + "\n";

            if (cmd.Trim().Length == 0 && !cmd.Equals("\n"))
            {
                return;
            }
            char[] charArray = cmd.ToCharArray();
            Send(charArray);
            lastLineSent = cmd.Trim();
            CommandComplted = false;
            QUERY_INTERVAL = 200;
            //            this.CMD_QUERY();
        }

        /// <summary>
        /// Send a char array command to grbl port
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(char[] bytes)
        {
            if (grblPort != null && grblPort.IsOpen)
            {
                lock (lockObject)
                {
                    try
                    {
                        grblPort.Write(bytes, 0, bytes.Length);
                    }
                    catch (Exception e)
                    {
                        PortStateChangeArg arg = new PortStateChangeArg();
                        arg.State = "Disconnected";
                        OnPortEvent(arg);
                    }
                }
            }
            else
            {
                PortStateChangeArg arg = new PortStateChangeArg();
                arg.State = "Disconnected";
                OnPortEvent(arg);
            }
        }

        public void SendByte(int byteInt)
        {
            byte b = Convert.ToByte(byteInt);
            byte[] barray = { b };
            grblPort.Write(barray, 0, barray.Length);
        }

        #region "Command Commands"
        /// <summary>
        /// Send parameter query command
        /// </summary>
        public void CMC_QUERYPARAMS()
        {
            Send("$$");
        }

        /// <summary>
        /// Send get offset command
        /// </summary>
        public void CMD_GETOFFSET()
        {
            Send("$#");
        }

        /// <summary>
        /// Send soft reset command
        /// </summary>
        public void CMD_RESET()
        {
            char ch = (char)Int16.Parse("18", System.Globalization.NumberStyles.AllowHexSpecifier);
            char[] charray = new char[1];
            charray[0] = ch;
            Send(charray);
            IsGCodeLoaded = false;
            if (gcodeFile != null)
            {
                gcodeFile.Close();
            }

        }
        public void CMD_HOME()
        {
            Send("$H");
        }
        public void CMD_ZERO(string axis)
        {
            Send("G92 " + axis.ToUpper() + "0");
        }

        public void CMD_ZEROALL()
        {
            Send("G92X0Y0Z0");
        }
        public void CMD_UNLOCK()
        {
            Send("$X");
        }

        public void CMD_JOGCANDEL()
        {
            // 0x85  : Jog Cancel
            SendByte(55);
            //SendHex("85");
        }

        private bool prbCompleted = false;
        public void CMD_PROBE(float x, float y)
        {
            prbCompleted = false;
            string xStr = " X" + x.ToString("0.000");
            string yStr = " Y" + y.ToString("0.000");

            Send("G38.2 Z-100 F100");
        }

        /// <summary>
        /// Combination Communication Function - Query
        /// </summary>
        public void CMD_QUERY()
        {
            if (grblPort != null)
            {
                char[] terminateBytes = { '?' };
                Send(terminateBytes);
            }
        }

        /// <summary>
        /// Stop Jogging
        /// </summary>
        public void CMD_JC()
        {
            byte ch = Convert.ToByte('\x0085');
            SendByte(ch);
        }

        /// <summary>
        /// Start Jogging
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="isPositive"></param>
        public void CMD_JOG(string axis, bool isPositive)
        {
            SetFastInterval();
            string dist = "500";

            string feedRateStr = " F" + JogFeedRate.ToString("0");
            if (!isPositive)
            {
                dist = "-" + dist;
            }

            string cmdStr = "$J=" + axis + dist + feedRateStr;
            Send(cmdStr);
        }
        public bool IsRelative { get; set; }

        /// <summary>
        /// Set to relative movement
        /// </summary>
        public void CMD_TORELETIVE()
        {
            Send("G91");
            IsRelative = true;
        }
        /// <summary>
        /// Set to absolute movement
        /// </summary>
        public void CMD_TOABSOLUTE()
        {
            Send("G90");
            IsRelative = false;

        }

        /// <summary>
        /// Set machine location
        /// </summary>
        /// <param name="x">X Location</param>
        /// <param name="y">Y Location</param>
        /// <param name="z">Z Location</param>
        public void CMD_SETTO(float x, float y, float z)
        {
            Send("G92 X" + x.ToString("0.000") + " Y" + y.ToString("0.000") + " Z" + z.ToString("0.000"));
        }

        /// <summary>
        /// Move to a location relatively
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="feedrate"></param>
        public void CMD_MOVERELATIVE(float x, float y, float z, float feedrate)
        {
            CMD_TORELETIVE();
            string cmd = "G1 X" + x.ToString("0.000") + " Y" + y.ToString("0.000") + " Z" + z.ToString("0.000") + " F" + feedrate.ToString("0");
            Send(cmd);

            CMD_TOABSOLUTE();
        }

        /// <summary>
        /// MOve one axis to a location relatively
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="step"></param>
        /// <param name="feedrate"></param>
        public void CMD_MOVERELATIVE(string axis, float step, float feedrate)
        {
            CMD_TORELETIVE();
            string cmd = $"G1 {axis.ToUpper()}" + step.ToString("0.000") + " F" + feedrate.ToString("0");
            Send(cmd);
            CMD_TOABSOLUTE();
        }

        /// <summary>
        /// Move to a location
        /// </summary>
        /// <param name="x">X Location</param>
        /// <param name="y">Y Location</param>
        /// <param name="z">Z Location</param>
        /// <param name="feedrate"></param>
        public void CMD_MOVETO(float x, float y, float z, float feedrate)
        {
            float rate = oldFeedRate;
            if (rate == 0)
                rate = 100f;
            string cmd = "G1 X" + x.ToString("0.000") + " Y" + y.ToString("0.000") + " Z" + z.ToString("0.000") + " F" + feedrate.ToString("0");
            Send(cmd);
        }
        /// <summary>
        /// Move one axis to a location
        /// </summary>
        /// <param name="axis">Axis, X, Y or Z</param>
        /// <param name="to">To location</param>
        /// <param name="feedrate">Feed rate</param>
        public void CMD_MOVETO(string axis, float to, float feedrate)
        {
            float rate = oldFeedRate;
            if (rate == 0)
                rate = 100f;
            string cmd = $"G1 {axis.ToUpper()}" + to.ToString("0.000") + " F" + feedrate.ToString("0");
            Send(cmd);
        }

        #endregion

        /// <summary>
        /// Send a char command
        /// </summary>
        /// <param name="c"></param>
        public void SendChar(char c)
        {
            char[] array = { c };
            Send(array);
        }

        /// <summary>
        /// Start a query loop, until grbl port closed
        /// </summary>
        private void StartQueryLoop()
        {
            while (doQuery && this.grblPort.IsOpen)
            {
                if (interruptChar != '0')
                {
                    SendChar(interruptChar);
                    interruptChar = '0';
                }
                this.ExecuteDelayedCommand();
                this.CMD_QUERY();
                //QUERY_INTERVAL is a variable, it can be changed based on needs, if it is not running, the interval can be longer than while it is moving

                System.Threading.Thread.Sleep(QUERY_INTERVAL);
            }
        }

        #region "Events"

        /// <summary>
        /// COM Port Event
        /// </summary>
        public event EventHandler PortEvent;
        protected virtual void OnPortEvent(PortStateChangeArg e)
        {
            EventHandler handler = PortEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Grbl Error Event
        /// </summary>
        public event EventHandler ErrorEvent;
        protected virtual void OnError(GrblErrorEventArgs e)
        {
            EventHandler handler = ErrorEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Grbl Response Event
        /// </summary>
        public event EventHandler ResponseReceived;
        protected virtual void OnResponseReceived(EventArgs e)
        {
            EventHandler handler = ResponseReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        
        protected virtual void OnGCodeStatusUpdate(EventArgs e)
        {
            EventHandler handler = GCodeStatusUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// GCode Execution status update event
        /// </summary>
        public event EventHandler GCodeStatusUpdate;
        #endregion
    }
}
