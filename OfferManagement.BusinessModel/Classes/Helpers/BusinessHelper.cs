using Imis.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;

namespace OfferManagement.BusinessModel
{
    public static class BusinessHelper
    {
        public static string FullNameTrim(string contactName, int maxLength)
        {
            string fullName = string.Empty;

            if (contactName.Length >= maxLength)
            {
                fullName = contactName.SubstringByLength(maxLength - contactName.Length - 1);
            }
            else
            {
                fullName = contactName;
            }

            return fullName;
        }
    }
}