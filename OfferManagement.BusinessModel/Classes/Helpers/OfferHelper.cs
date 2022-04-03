using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public static class OfferHelper
    {
        public static string FormatDecimalNumber(this object number)
        {
            string numberStr = Convert.ToDecimal(number) % 1 == 0
                                ? Convert.ToInt32(number).ToString()
                                : Convert.ToDecimal(number).ToString();

            if (numberStr.EndsWith("0"))
                numberStr = numberStr.Substring(0, numberStr.Length - 1);

            if (numberStr.EndsWith(","))
                numberStr = numberStr.Substring(0, numberStr.Length - 1);

            return numberStr;
        }
    }
}
