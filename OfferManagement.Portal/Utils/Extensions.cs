using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using OfferManagement.BusinessModel;
using Imis.Domain;

namespace OfferManagement.Portal
{
    public static class Extensions
    {
        private const string s_CurrentReporter = "_currentReporter";
        private const string s_CurrentStudent = "_currentStudent";
        private const string s_CurrentStore = "_currentStore";

        public static string CrLfToBr(this string s)
        {
            return s != null ? s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "<br />") : "";
        }

        public static string[] SplitLines(this string s)
        {
            return s.Replace("\r", "\n").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            while (s.Contains("  "))
                s = s.Replace("  ", " ");

            return s.Trim();
        }

        public static DateTime GetDateOnly(this DateTime s)
        {
            return s.Date;
        }

        public static DateTime? GetDateOnly(this DateTime? s)
        {
            if (!s.HasValue)
                return null;

            return s.Value.Date;
        }

        public static string AddZeroPadding(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            int sLength = s.Length;
            int numberOfZeros = maxLength - sLength;

            string sPadded = "";

            for (int i = 0; i < numberOfZeros; i++)
                sPadded += "0";

            sPadded += s;

            return sPadded;
        }

        public static bool Is(this int enumeration, int value)
        {
            return (enumeration & value) == value;
        }

        public static void SaveToCurrentContext(this Reporter reporter)
        {
            if (HttpContext.Current != null && reporter != null)
            {
                if (HttpContext.Current.Items.Contains(s_CurrentReporter))
                    HttpContext.Current.Items[s_CurrentReporter] = reporter;
                else
                    HttpContext.Current.Items.Add(s_CurrentReporter, reporter);
            }
        }

        public static void SaveToCurrentContext(this Store store)
        {
            if (HttpContext.Current != null && store != null)
            {
                if (HttpContext.Current.Items.Contains(s_CurrentStore))
                    HttpContext.Current.Items[s_CurrentStore] = store;
                else
                    HttpContext.Current.Items.Add(s_CurrentStore, store);
            }
        }

        public static Reporter LoadReporter(this HttpContext context)
        {
            if (context.Items.Contains(s_CurrentReporter))
            {
                return context.Items[s_CurrentReporter] as Reporter;
            }
            return null;
        }

        public static Store LoadStore(this HttpContext context)
        {
            if (context.Items.Contains(s_CurrentStore))
            {
                return context.Items[s_CurrentStore] as Store;
            }
            return null;
        }

        public static void SetContactPhone(this Reporter reporter, string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                if (phone.StartsWith("69"))
                {
                    reporter.ContactPhone = null;
                    reporter.ContactMobilePhone = phone;
                }
                else
                {
                    reporter.ContactPhone = phone;
                    reporter.ContactMobilePhone = null;
                }
            }
            else
            {
                reporter.ContactPhone = null;
                reporter.ContactMobilePhone = null;
            }
        }
    }
}
