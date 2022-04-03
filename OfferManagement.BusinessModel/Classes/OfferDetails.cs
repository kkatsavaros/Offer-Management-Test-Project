using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public enum enOfferDetails
    {
        Manufacturer,
        Model,
        Cpu,
        CpuSpeed,
        RamSize,
        StorageSize,
        UsbCount,
        HdmiCount,
        ScreenSize,
        ScreenResolutionX,
        ScreenResolutionY,
        OperatingSystem,
        GuaranteeYears,
        IsWiFi80211acCompliant,
        OfficialSpecsUrl,
        Price
    }

    public class OfferDetails : Dictionary<string, object>
    {
        public object this[enOfferDetails details]
        {
            get { return this[details.ToString()]; }
            set { this[details.ToString()] = value; }
        }

        public bool ContainsKey(enOfferDetails details)
        {
            return ContainsKey(details.ToString());
        }
    }
}
