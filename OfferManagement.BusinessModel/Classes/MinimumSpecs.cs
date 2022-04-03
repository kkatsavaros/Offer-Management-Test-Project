using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class MinimumSpecs
    {
        public double CpuSpeed { get; set; }
        public int RamSize { get; set; }
        public int StorageSize { get; set; }
        public int UsbCount { get; set; }
        public int HdmiCount { get; set; }
        public double ScreenSize { get; set; }
        public int ScreenResolutionX { get; set; }
        public int ScreenResolutionY { get; set; }
        public int GuaranteeYears { get; set; }
    }
}
