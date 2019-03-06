using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class GrblErrorEventArgs : EventArgs
    {
        public string Line { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public override string ToString()
        {
            return $"{Code}:{Desc}";

        }
    }
}
