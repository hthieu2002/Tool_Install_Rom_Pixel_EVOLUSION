using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InstallRom.Data
{
    public class DevicesCustomData 
    {
        public string Serial { get; set; } = "";
        public string deviceName { get; set; } = "";
        public string rom { get; set; } = "";
        public string boot { get; set; } = "";
        public bool checkEdit { get; set; } = true;
        //public int deviceNameSerial { get; set; }

        public int deviceNameSerial { get; set; }
       
    }
}
