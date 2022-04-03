using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OfferManagement.BusinessModel
{

    public class MinimumSpecsErrorList : List<MinimumSpecsError>
    {
        public MinimumSpecsErrorList() : base() { }
        public MinimumSpecsErrorList(IEnumerable<MinimumSpecsError> collection) : base(collection) { }
        public MinimumSpecsErrorList(int capacity) : base(capacity) { }
    }

    public class MinimumSpecsError
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class MinimumSpecsService
    {
        public bool Validate(Offer offer)
        {
            MinimumSpecsErrorList errors;
            bool isValid = Validate(offer, out errors);


            return isValid;
        }

        public bool Validate(Offer offer, out MinimumSpecsErrorList errors)
        {
            errors = new MinimumSpecsErrorList();

            if (offer.Price == 0)
            {
                errors.Add(new MinimumSpecsError() { Title = "CurrentPrice", Message = "Δεν έχετε εισάγει τρέχουσα τιμή για την προσφορά" });
            }

            var cpuSpeed = _specs.CpuSpeed;
            if ((double)offer.CpuSpeed < cpuSpeed)
            {
                errors.Add(new MinimumSpecsError() { Title = "CpuSpeed", Message = string.Format("Η ταχύτητα χρονισμού του επεξεργαστή πρέπει να είναι τουλάχιστον {0} GHz", cpuSpeed) });
            }

            var ramSize = _specs.RamSize;
            if (offer.RamSize < ramSize)
            {
                errors.Add(new MinimumSpecsError() { Title = "RamSize", Message = string.Format("Η μνήμη RAM πρέπει να είναι τουλάχιστον {0} GB", ramSize) });
            }

            var storageSize = _specs.StorageSize;
            if (offer.StorageSize < storageSize)
            {
                errors.Add(new MinimumSpecsError() { Title = "StorageSize", Message = string.Format("Η χωρητικότητα του δίσκου πρέπει να είναι τουλάχιστον {0} GB ", storageSize) });
            }

            var usbCount = _specs.UsbCount;
            if (offer.UsbCount < usbCount)
            {
                errors.Add(new MinimumSpecsError() { Title = "UsbCount", Message = string.Format("Το Laptop πρέπει να διαθέτει τουλάχιστον {0} θύρες USB", usbCount) });
            }

            var hdmiCount = _specs.HdmiCount;
            if (offer.HdmiCount < hdmiCount)
            {
                errors.Add(new MinimumSpecsError() { Title = "HdmiCount", Message = string.Format("Το Laptop πρέπει να διαθέτει τουλάχιστον {0} θύρα HDMI", hdmiCount) });
            }

            var screenSize = _specs.ScreenSize;
            if (offer.ScreenSize < new decimal(screenSize))
            {
                errors.Add(new MinimumSpecsError() { Title = "ScreenSize", Message = string.Format("Η διαγώνιος της οθόνης πρέπει να είναι τουλάχιστον {0}''", screenSize) });
            }
            var screenResolutionX = _specs.ScreenResolutionX;
            var screenResolutionY = _specs.ScreenResolutionY;
            if (offer.ScreenResolutionX < screenResolutionX
                || offer.ScreenResolutionY < screenResolutionY)
            {
                errors.Add(new MinimumSpecsError() { Title = "ScreenResolution", Message = string.Format("Η ανάλυση της οθόνης πρέπει να είναι τουλάχιστον {0}x{1}", screenResolutionX, screenResolutionY) });
            }

            var guaranteeYears = _specs.GuaranteeYears;
            if (offer.GuaranteeYears < guaranteeYears)
            {
                errors.Add(new MinimumSpecsError() { Title = "GuaranteeYears", Message = string.Format("Το Laptop πρέπει να προσφέρει εγγύηση τουλάχιστον {0} ετών", guaranteeYears) });
            }

            if (!offer.IsWiFi80211acCompliant)
            {
                errors.Add(new MinimumSpecsError() { Title = "IsWiFi80211acCompliant", Message = "Το ασύρματο δίκτυο πρέπει να υποστηρίζει Wi-Fi 802.11 ac" });
            }

            return errors.Count == 0;
        }

        #region [ Static ]

        private static MinimumSpecs _specs = null;

        static MinimumSpecsService()
        {
            _specs = LoadSpecs();
        }

        #endregion

        #region [ XmlParse ]

        private static MinimumSpecs LoadSpecs()
        {
            var xml = string.Empty;

            if (HttpContext.Current == null)
                xml = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/MinimumSpecs.xml"));
            else
                xml = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/MinimumSpecs.xml"));

            return new Serializer<MinimumSpecs>().Deserialize(xml);
        }

        #endregion
    }
}
