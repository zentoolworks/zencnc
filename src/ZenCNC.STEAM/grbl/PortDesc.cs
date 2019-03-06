using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class PortDesc
    {
        public string DeviceId { get; set; }
        public string Caption { get; set; }

        public override string ToString()
        {
            return $"{DeviceId} - {Caption}";
        }
    }
}
