using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class GrblResponseEventArgs : EventArgs
    {
        private string _response;
        private string _status;
        private Position _mpos = new Position() { X = 0f, Y = 0f, Z = 0f };
        private Position _wpos = new Position() { X = 0f, Y = 0f, Z = 0f };
        private Position _wco = new Position() { X = 0f, Y = 0f, Z = 0f };
        private MachineState _state { get; set; }
        public string Version { get; set; }

        public bool PositionChanged = false;
        public bool FeedrateChanged = false;
        public bool StateChanged = false;
        public bool OffsetChanged = false;
        public bool ProbeCompleted = false;

        public string EventType { get; set; }

        public MachineState State
        {
            get
            {
                return _state;
            }
        }


        public string Status
        {
            get
            {
                return _status;
            }
        }

        public Position MPos
        {
            get
            {
                return _mpos;
            }
        }
        public Position WPos
        {
            get
            {
                return _wpos;
            }
        }
        public Position WCO
        {
            get
            {
                return _wco;
            }
        }
        public float Feed
        {
            get; set;
        }

        public float Speed
        {
            get; set;
        }

        public void ProcessStatusData()
        {
            ProbeCompleted = false;
            StateChanged = true;
            string content = _response.Substring(1, _response.Length - 2);
            //            logger.Info("Process Status Data >>>" + content);
            string[] flds = content.Split('|');
            _status = flds[0];

            if (_status.Contains(":"))
            {
                _status = _status.Substring(0, _status.IndexOf(':'));
            }
            switch (_status)
            {
                case "Idle":
                    _state = MachineState.IDLE;
                    break;
                case "Run":
                    _state = MachineState.RUN;
                    break;
                case "Hold":
                    _state = MachineState.HOLD;
                    break;
                case "Jog":
                    _state = MachineState.JOG;
                    break;
                case "Alarm":
                    _state = MachineState.ALARM;
                    break;
                case "Door":
                    _state = MachineState.DOOR;
                    break;
                case "Check":
                    _state = MachineState.CHECK;
                    break;
                case "Home":
                    _state = MachineState.HOLD;
                    break;
                case "Sleep":
                    _state = MachineState.SLEEP;
                    break;
            }

            for (int i = 1; i < flds.Length; i++)
            {
                string[] subflds = flds[i].Split(':');
                if (subflds[0].Equals("MPos"))
                {
                    PositionChanged = true;
                    string[] xyz = subflds[1].Split(',');
                    _mpos = new Position()
                    {
                        X = float.Parse(xyz[0]),
                        Y = float.Parse(xyz[1]),
                        Z = float.Parse(xyz[2])
                    };

                }
                else if (subflds[0].Equals("WPos"))
                {
                    PositionChanged = true;
                    string[] xyz = subflds[1].Split(',');
                    _wpos = new Position()
                    {
                        X = float.Parse(xyz[0]),
                        Y = float.Parse(xyz[1]),
                        Z = float.Parse(xyz[2])
                    };
                }
                else if (subflds[0].Equals("FS"))
                {
                    FeedrateChanged = true;
                    string[] fs = subflds[1].Split(',');
                    Feed = float.Parse(fs[0]);
                    Speed = float.Parse(fs[1]);
                }
                else if (subflds[0].Equals("WCO"))
                {
                    OffsetChanged = true;
                    string[] xyz = subflds[1].Split(',');
                    _wco = new Position()
                    {
                        X = float.Parse(xyz[0]),
                        Y = float.Parse(xyz[1]),
                        Z = float.Parse(xyz[2])
                    };
                }
                else if (subflds[0].Equals("Ov"))
                {

                    //                    Console.WriteLine(subflds[0]);

                }
                else
                {
                    //                    Console.WriteLine(subflds[0]);
                }

            }
        }

        private void ProcessVersionData()
        {
            ProbeCompleted = false;
            string sub = _response.Substring(0, _response.IndexOf("["));
            string[] flds = sub.Split(' ');
            Version = flds[1].Trim();

        }

        private void ProcessOffsetData()
        {
            ProbeCompleted = false;
            string content = _response.Substring(1, _response.Length - 2);
            string code = content.Substring(0, content.IndexOf(":"));
            string[] flds = content.Substring(content.IndexOf(":") + 1).Split(',');
            switch (code)
            {
                case "G54":
                case "G55":
                case "G56":
                case "G57":
                    break;
                case "G92":
                    _wco = new Position()
                    {
                        X = float.Parse(flds[0]),
                        Y = float.Parse(flds[1]),
                        Z = float.Parse(flds[2])
                    };
                    OffsetChanged = true;
                    break;
            }
        }

        private void ProcessProbeData()
        {
            string response = _response.Trim();
            int colonIdx = response.LastIndexOf(":");
            string lastStr = response.Substring(colonIdx + 1, 1);
            if (!lastStr.Equals("1"))
            {
                ProbeCompleted = false;
                return;
            }

            string posStr = response.Substring(5, colonIdx - 5);
            string[] flds = posStr.Split(',');
            _mpos = new Position()
            {
                X = float.Parse(flds[0]),
                Y = float.Parse(flds[1]),
                Z = float.Parse(flds[2])
            };
            ProbeCompleted = true;

        }

        /// <summary>
        /// Parse Grbl response and prepare the update event argument
        /// </summary>
        public string Response
        {

            get
            {
                return _response;
            }
            //Set 
            set
            {
                //                logger.Info("Grbl Response:[" + value + "]");
                _response = value;

                //Grbl Status Update
                if (_response.StartsWith("<"))
                {
                    ProcessStatusData();
                }
                //Grbl Probe Status
                else if (_response.StartsWith("[PRB:"))
                {
                    ProcessProbeData();
                }
                //Grbl offset update
                else if (_response.StartsWith("["))
                {
                    ProcessOffsetData();
                }
                //Grbl Version Update
                else if (_response.StartsWith("Grbl"))
                {
                    ProcessVersionData();
                }
            }
        }

    }

    public enum MachineState
    {
        IDLE,
        RUN,
        HOLD,
        JOG,
        ALARM,
        DOOR,
        CHECK,
        HOME,
        SLEEP,
        NOTCONNECTED
    }

    public class Position
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static bool operator ==(Position a, Position b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            else
            {
                return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
            }
        }
        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }
        public override string ToString()
        {
            return "(" + X.ToString("0.000") + ", " + Y.ToString("0.000") + ", " + Z.ToString("0.000") + ")";
        }
    }
}
