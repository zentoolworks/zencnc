using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class DelayedCommand
    {
        public DateTime ExecuteAfter { get; set; }
        public string Command { get; set; }
        public DelayedCommand(double delaymillisecond, string command)
        {
            ExecuteAfter = DateTime.Now.AddMilliseconds(delaymillisecond);
            Command = command;
        }

        public bool IsTimeExpried()
        {
            return DateTime.Now.Subtract(ExecuteAfter).Milliseconds >= 0;
        }
    }
}
