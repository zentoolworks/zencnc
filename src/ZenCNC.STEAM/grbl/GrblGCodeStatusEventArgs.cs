using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class GrblGCodeStatusEventArgs : EventArgs
    {
        public FileStatus FileStatus { get; set; }
        public int TotalLine { get; set; }
        public int CurrentLine { get; set; }
        public string gcode { get; set; }
    }

    public enum FileStatus
    {
        [Description("Loaded")]
        Loaded,
        [Description("Stopped")]
        Stopped,
        [Description("Running")]
        Running,
        [Description("Paused")]
        Paused,
        [Description("Error")]
        Error
    }
}
