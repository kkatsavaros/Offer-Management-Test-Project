using DevExpress.Web;
using Imis.Domain;
using OfferManagement.BusinessModel;
using System;
using System.Web;
using System.Linq;
using OfferManagement.Portal.DataSources;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OfferManagement.Portal
{
    public static class VerificationHelper
    {
        public static void SendVerificationInfo(Reporter reporter, IUnitOfWork UnitOfWork)
        {
            switch (reporter.ReporterType)
            {   
                case enReporterType.Store:
                    SendEmailVerification(reporter, UnitOfWork);
                    break;
            }

            UnitOfWork.Commit();
        }

        #region [ Helper Methods ]

        public static void SendEmailVerification(Reporter reporter, IUnitOfWork UnitOfWork)
        {
            Uri baseURI;
            if (Config.IsSSL)
            {
                baseURI = new Uri("https://" + HttpContext.Current.Request.Url.Authority + "/Common/");
            }
            else
            {
                baseURI = new Uri("http://" + HttpContext.Current.Request.Url.Authority + "/Common/");
            }

            Uri uri = new Uri(baseURI, "VerifyEmail.aspx?id=" + reporter.EmailVerificationCode);

            Email email = new Email();
            if (reporter.ReporterType == enReporterType.Store)
            {
                email = EmailFactory.GetStoreEmailVerification(reporter, uri);
            }

            UnitOfWork.MarkAsNew(email);
            UnitOfWork.Commit();

            EmailQueueWorker.Current.AddEmailDispatchToQueue(email.ID);
        }

        public static void SendMobilePhoneVerificationInfo(Reporter reporter, IUnitOfWork UnitOfWork)
        {
            var sms = SMSFactory.GetVerificationCode(reporter);
            UnitOfWork.MarkAsNew(sms);
            reporter.SMSSentCount++;

            UnitOfWork.Commit();

            SMSQueueWorker.Current.AddSmsDispatchToQueue(sms.ID);
        }

        #endregion
    }

    public static class DropDownHelper
    {
        public static void FillFromEnum<T>(this ASPxComboBox dropDown, string promptValue = "-- αδιάφορο --", bool showPromptValue = true, bool includeZeroValue = false)
           where T : struct
        {
            dropDown.Items.Clear();

            if (showPromptValue)
            {
                dropDown.Items.Add(new ListEditItem(promptValue, null));
            }

            foreach (Enum item in Enum.GetValues(typeof(T)))
            {
                if (item.GetValue() == 0 && !includeZeroValue)
                    continue;

                dropDown.Items.Add(new ListEditItem(item.GetLabel(), item.GetValue()));
            }
        }

        public static void FillFromEnum<T>(this ASPxCheckBoxList checkBoxList, bool includeZeroValue = false)
          where T : struct
        {
            checkBoxList.Items.Clear();

            foreach (Enum item in Enum.GetValues(typeof(T)))
            {
                if (item.GetValue() == 0 && !includeZeroValue)
                    continue;

                checkBoxList.Items.Add(new ListEditItem(item.GetLabel(), item.GetValue()));
            }
        }

        public static void FillFromEnum<T>(this ASPxRadioButtonList radioButtonList, bool includeZeroValue = false)
           where T : struct
        {
            radioButtonList.Items.Clear();

            foreach (Enum item in Enum.GetValues(typeof(T)))
            {
                if (item.GetValue() == 0 && !includeZeroValue)
                    continue;

                radioButtonList.Items.Add(new ListEditItem(item.GetLabel(), item.GetValue()));
            }
        }

        public static void FillTrueFalse(this ASPxComboBox dropDown, string promptValue = "-- αδιάφορο --")
        {
            dropDown.Items.Add(new ListEditItem(promptValue, null));

            dropDown.Items.Add(new ListEditItem("ΝΑΙ", 1));
            dropDown.Items.Add(new ListEditItem("ΟΧΙ", 0));
        }

        public static void FillRegions(this ASPxComboBox ddlRegion, string promptValue = "-- επιλέξτε περιφέρεια --")
        {
            ddlRegion.Items.Add(new ListEditItem(promptValue, null));

            foreach (var item in CacheManager.GetOrderedRegions())
            {
                ddlRegion.Items.Add(new ListEditItem(item.Name, item.ID));
            }
        }

        public static void FillPrefectures(this ASPxComboBox ddlPrefecture, string promptValue = "-- επιλέξτε περιφερειακή ενότητα --")
        {
            ddlPrefecture.Items.Add(new ListEditItem(promptValue, null));

            foreach (var item in CacheManager.GetOrderedPrefectures())
            {
                ddlPrefecture.Items.Add(new ListEditItem(item.Name, item.ID));
            }
        }

        public static void FillCities(this ASPxComboBox ddlCity, string prefID, string promptValue = "-- επιλέξτε καλλικρατικό δήμο --")
        {
            int prefectureID;
            if (int.TryParse(prefID, out prefectureID) && prefectureID > 0)
            {
                var cities = CacheManager.GetOrderedCities(prefectureID);

                ddlCity.Items.Clear();
                if (cities.Count() == 1)
                {
                    ddlCity.Items.Add(new ListEditItem(cities.FirstOrDefault().Name, cities.FirstOrDefault().ID));
                }
                else
                {
                    ddlCity.Items.Add(new ListEditItem(promptValue, null));

                    foreach (City item in cities)
                    {
                        ddlCity.Items.Add(new ListEditItem(item.Name, item.ID));
                    }
                }
            }
        }

        public static void FillDOYs(this ASPxComboBox ddlDOY, string promptValue = "-- επιλέξτε Δ.Ο.Υ. --")
        {
            ddlDOY.Items.Add(new ListEditItem(promptValue, null));

            foreach (XElement elem in DOY.DOYsXml.Descendants("DOY"))
            {
                ddlDOY.Items.Add(new ListEditItem(elem.Value, elem.Value));
            }
        }

        public static void FillBanks(this ASPxComboBox ddlBank, string promptValue = "-- επιλέξτε τράπεζα --")
        {
            ddlBank.Items.Add(new ListEditItem(promptValue, null));

            foreach (Bank bank in CacheManager.GetOrderedBanks())
            {
                ddlBank.Items.Add(new ListEditItem(bank.Name, bank.ID));
            }
        }

        public static void FillOffers(this ASPxComboBox ddlOffer, IEnumerable<Offer> offers, string promptValue = "-- επιλέξτε προσφορά --")
        {
            ddlOffer.Items.Clear();
            ddlOffer.Items.Add(new ListEditItem(promptValue, null));

            foreach (var offer in offers)
            {
                ddlOffer.Items.Add(new ListEditItem(string.Format("{0} - {1}", offer.Code, offer.Title), offer.Code));
            }
        }
    }
}