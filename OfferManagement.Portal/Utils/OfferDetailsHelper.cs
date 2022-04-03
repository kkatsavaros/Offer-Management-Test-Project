using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using OfferManagement.BusinessModel;

namespace OfferManagement.Portal
{
    public static class OfferDetailsHelper
    {
        public static string RenderSummaryShort(OfferDetails details)
        {
            if (details.ContainsKey(enOfferDetails.Manufacturer))
            {
                StringBuilder summary = new StringBuilder();

                var manufacturer = details[enOfferDetails.Manufacturer];
                var model = details[enOfferDetails.Model];
                var cpu = details[enOfferDetails.Cpu];
                var screenSize = details[enOfferDetails.ScreenSize].FormatDecimalNumber();
                var screenResolution = string.Format("{0}x{1}", details[enOfferDetails.ScreenResolutionX], details[enOfferDetails.ScreenResolutionY]);
                var ramSize = details[enOfferDetails.RamSize].FormatDecimalNumber();
                var storageSize = details[enOfferDetails.StorageSize];

                summary.AppendFormat("{0} {1}", manufacturer, model)
                       .Append("<br/>")
                       .AppendFormat("Επεξεργαστής: {0}", cpu)
                       .Append("<br/>")
                       .AppendFormat("RAM: {0} GB, Δίσκος: {1} GB, Οθόνη: {2}'', Ανάλυση: {3}", ramSize, storageSize, screenSize, screenResolution)
                       .Append("<br/>");

                summary.Append(RenderPriceSummary(details));

                return summary.ToString();
            }

            return string.Empty;
        }

        public static string RenderSummaryInline(OfferDetails details)
        {
            if (details.ContainsKey(enOfferDetails.Manufacturer))
            {
                StringBuilder summary = new StringBuilder();

                var operatingSystem = details[enOfferDetails.OperatingSystem];
                var screenSize = details[enOfferDetails.ScreenSize].FormatDecimalNumber();
                var screenResolution = string.Format("{0}x{1}", details[enOfferDetails.ScreenResolutionX], details[enOfferDetails.ScreenResolutionY]);
                var cpu = details[enOfferDetails.Cpu];
                var ramSize = details[enOfferDetails.RamSize].FormatDecimalNumber();
                var storageSize = details[enOfferDetails.StorageSize];
                var guaranteeYears = details[enOfferDetails.GuaranteeYears];

                summary.AppendFormat("{0}, ", operatingSystem)
                       .AppendFormat("Επεξεργαστής: {0}, ", cpu)
                       .AppendFormat("RAM: {0} GB, Δίσκος: {1} GB, Οθόνη: {2}'', Ανάλυση: {3}, ", ramSize, storageSize, screenSize, screenResolution)
                       .AppendFormat("{0} χρόνια εγγύηση.", guaranteeYears);

                return summary.ToString();
            }

            return string.Empty;
        }

        #region [ Helper Methods ]

        private static string RenderPriceSummary(OfferDetails details)
        {
            decimal? price = details[enOfferDetails.Price] as decimal?;

            StringBuilder priceSummary = new StringBuilder();

            priceSummary.AppendFormat("Τιμή: {0:C}", price);

            return priceSummary.ToString();
        }

        #endregion
    }
}