using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OfferManagement.BusinessModel
{
    public static class Extensions
    {
        public static string SubstringByLength(this string s, int length)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            string result = string.Empty;

            if (s.Length > length)
            {
                result = s.Substring(0, length);
            }
            else
            {
                result = s;
            }

            return result;
        }

        public static Dictionary<string, object> ToDictionary(this NameValueCollection values)
        {
            var dict = new Dictionary<string, object>();
            foreach (string item in values)
            {
                int intValue;
                if (int.TryParse(values[item], out intValue))
                {
                    dict.Add(item, intValue);
                    continue;
                }

                bool boolValue;
                if (bool.TryParse(values[item], out boolValue))
                {
                    dict.Add(item, boolValue);
                    continue;
                }

                double floatValue;
                if (double.TryParse(values[item], out floatValue))
                {
                    dict.Add(item, floatValue);
                    continue;
                }

                Guid guidValue;
                if (Guid.TryParse(values[item], out guidValue))
                {
                    dict.Add(item, guidValue);
                    continue;
                }

                DateTime dtValue;
                if (DateTime.TryParse(values[item], out dtValue))
                {
                    dict.Add(item, dtValue);
                    continue;
                }

                dict.Add(item, values[item]);
            }
            return dict;
        }
    }
}
