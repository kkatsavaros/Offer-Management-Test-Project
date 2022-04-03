using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OfferManagement.BusinessModel
{
    public static class RequestHelper
    {
        public static string GetClientIP(this HttpRequest request)
        {
            var ip = request.Headers["X-Forwarded-For"];

            return string.IsNullOrEmpty(ip)
                    ? request.UserHostAddress
                    : ip;
        }

        public static string GetScheme(this HttpRequest request)
        {
            var scheme = request.Headers["X-Proto"];

            return !string.IsNullOrEmpty(scheme) && scheme == "SSL"
                    ? "https"
                    : request.Url.Scheme;
        }
    }
}
