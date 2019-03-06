using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.WinForm.Control
{
    public class CommandEventArgs:EventArgs
    {
        public string Command { get; set; }
    }
}
