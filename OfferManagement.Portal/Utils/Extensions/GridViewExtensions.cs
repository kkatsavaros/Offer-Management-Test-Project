using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using OfferManagement.BusinessModel;
using DevExpress.Web;
using System.Web;
using System.Web.Security;

namespace OfferManagement.Portal
{
    public static class GridViewExtensions
    {
        #region [ Reporter Extensions ]

        public static string GetContactDetails(this Reporter reporter)
        {
            if (reporter == null)
                return string.Empty;

            List<string> contactDetails = new List<string>();

            contactDetails.Add(reporter.ContactName);
            contactDetails.Add(reporter.ContactPhone);
            contactDetails.Add(reporter.ContactMobilePhone);
            contactDetails.Add(reporter.ContactEmail);

            contactDetails.RemoveAll(x => x == null);

            return string.Join("<br/>", contactDetails);
        }

        public static string GetReporterDetails(this Reporter reporter)
        {
            if (reporter == null)
                return string.Empty;

            StringBuilder reporterDetails = new StringBuilder();

            if (reporter.ReporterType == enReporterType.Store)
            {
                var store = reporter.Store;

                if (store != null)
                {
                    reporterDetails.Append(store.GetStoreDetails());
                }
            }

            if (!string.IsNullOrEmpty(reporter.OtherDetails))
            {
                reporterDetails.Append(string.Format("Λοιπά Στοιχεία: {0}", reporter.OtherDetails));
            }

            return reporterDetails.ToString();
        }

        #endregion

        #region [ Address Extensions ]

        public static string GetAddressDetails(this Address address, bool inline = false)
        {
            if (address == null)
                return string.Empty;

            List<string> addressDetails = new List<string>();

            addressDetails.Add(address.AddressName);
            addressDetails.Add(address.ZipCode);

            var city = CacheManager.Cities.Get(address.CityID.GetValueOrDefault());
            if (city != null)
            {
                addressDetails.Add(city.Name);
            }

            var prefecture = CacheManager.Prefectures.Get(address.PrefectureID.GetValueOrDefault());
            if (prefecture != null)
            {
                addressDetails.Add(prefecture.Name);
            }

            if (inline)
            {
                return string.Join(", ", addressDetails);
            }
            else
            {
                return string.Join("<br/>", addressDetails);
            }
        }

        #endregion

        #region [ Store Extensions ]

        public static string GetStoreDetails(this Store store)
        {
            if (store == null)
                return string.Empty;

            List<string> providerDetails = new List<string>();

            providerDetails.Add(store.Name);
            providerDetails.Add(store.AFM);

            providerDetails.RemoveAll(x => x == null);

            return string.Join("<br/>", providerDetails);
        }

        public static string GetAddressDetails(this Store store)
        {
            if (store == null)
                return string.Empty;

            return store.Reporter.Address.GetAddressDetails();
        }

        #endregion

        #region [ Offer Extensions ]

        public static string GetOfferStatus(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            if (offer.OfferStatus == enOfferStatus.Submitted)
            {
                if (offer.IsActive)
                {
                    if (offer.IsPublished)
                    {
                        return "Υποβεβλημένη<br/>(Δημοσιευμένη)";
                    }
                    else
                    {
                        return "Υποβεβλημένη<br/>(Μη Δημοσιευμένη)";
                    }
                }
                else
                {
                    return "Υποβεβλημένη<br/>(Ανενεργή)";
                }
            }

            return offer.OfferStatus.GetLabel();
        }

        public static string GetGeneralInfo(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            return string.Format("Κωδικός: {0}<br/>Τίτλος: {1}", offer.Code ?? "-", offer.Title ?? "-");
        }

        public static string GetOfferDescription(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            return offer.Description.Replace("\n", "<br />");
        }

        public static string GetSummary(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            return OfferDetailsHelper.RenderSummaryShort(offer.GetOfferDetails());
        }

        public static string GetSubmissionDetails(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            return string.Format("{0:dd/MM/yyyy}", offer.SubmittedAt);
        }

        public static string GetLastEvaluatorDetails(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            return string.Format("{0:dd/MM/yyyy}<br/>{1}", offer.EvaluatedAt, offer.EvaluatedBy);
        }

        public static string GetEvaluationDetails(this Offer offer)
        {
            if (offer == null)
                return string.Empty;

            if (offer.OfferStatusInt < enOfferStatus.Submitted.GetValue())
                return string.Empty;

            return string.Format("{0:dd/MM/yyyy}", offer.EvaluatedAt);
        }

        #endregion
    }
}