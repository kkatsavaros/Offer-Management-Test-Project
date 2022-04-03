using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public partial class Offer : IUserChangeTracking
    {
        public enOfferStatus OfferStatus
        {
            get { return (enOfferStatus)OfferStatusInt; }
            set
            {
                if (OfferStatusInt != (int)value)
                    OfferStatusInt = (int)value;
            }
        }

        public enLaptopColor Color
        {
            get { return (enLaptopColor)ColorInt; }
            set
            {
                if (ColorInt != (int)value)
                    ColorInt = (int)value;
            }
        }

        public bool IsFilled()
        {
            return !string.IsNullOrEmpty(Code);
        }

        public OfferDetails GetOfferDetails()
        {
            var details = new OfferDetails();

            if (!string.IsNullOrEmpty(Manufacturer))
            {
                details[enOfferDetails.Manufacturer] = Manufacturer;
                details[enOfferDetails.Model] = Model;
                details[enOfferDetails.Cpu] = Cpu;
                details[enOfferDetails.CpuSpeed] = CpuSpeed;
                details[enOfferDetails.RamSize] = RamSize;
                details[enOfferDetails.StorageSize] = StorageSize;
                details[enOfferDetails.UsbCount] = UsbCount;
                details[enOfferDetails.HdmiCount] = HdmiCount;
                details[enOfferDetails.ScreenSize] = ScreenSize;
                details[enOfferDetails.ScreenResolutionX] = ScreenResolutionX;
                details[enOfferDetails.ScreenResolutionY] = ScreenResolutionY;
                details[enOfferDetails.OperatingSystem] = OperatingSystem;
                details[enOfferDetails.GuaranteeYears] = GuaranteeYears;
                details[enOfferDetails.IsWiFi80211acCompliant] = IsWiFi80211acCompliant;
                details[enOfferDetails.OfficialSpecsUrl] = OfficialSpecsUrl;
                details[enOfferDetails.Price] = Price;
            }

            return details;
        }
    }
}
