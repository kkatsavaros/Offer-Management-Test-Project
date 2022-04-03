using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public static class CodeGenerationHelper
    {
        public static string GenerateVerificationCode()
        {
            return RandomNumber(10000000, 99999999);
        }

        private static string RandomNumber(int min, int max)
        {
            var random = new Random();

            return random.Next(min, max).ToString();
        }        
    }
}
