using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using OfferManagement.Configuration;

namespace OfferManagement.BusinessModel
{
    public static class Config
    {
        private static string _applicationUrl = null;
        public static string ApplicationUrl
        {
            get
            {
                if (_applicationUrl == null)
                    _applicationUrl = ConfigurationManager.AppSettings["ApplicationUrl"];

                return _applicationUrl;
            }
        }

        private static bool? _enableEmail = null;
        public static bool EnableEmail
        {
            get
            {
                if (_enableEmail == null)
                    _enableEmail = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableEmail"]);

                return _enableEmail.Value;
            }
        }

        private static bool? _enableAdminOffers = null;
        public static bool EnableAdminOffers
        {
            get
            {
                if (_enableAdminOffers == null)
                    _enableAdminOffers = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableAdminOffers"]);

                return _enableAdminOffers.Value;
            }
        }

        private static bool? _enableSMS = null;
        public static bool EnableSMS
        {
            get
            {
                if (_enableSMS == null)
                    _enableSMS = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSMS"]);

                return _enableSMS.Value;
            }
        }

        private static int? _maxSMSAllowed = null;
        public static int MaxSMSAllowed
        {
            get
            {
                if (_maxSMSAllowed == null)
                    _maxSMSAllowed = int.Parse(ConfigurationManager.AppSettings["MaxSMSAllowed"]);

                return _maxSMSAllowed.Value;
            }
        }

        private static bool? _isPilotSite = null;
        public static bool IsPilotSite
        {
            get
            {
                if (_isPilotSite == null)
                    _isPilotSite = Convert.ToBoolean(ConfigurationManager.AppSettings["IsPilotSite"]);

                return _isPilotSite.Value;
            }
        }

        private static bool? _isSSL = null;
        public static bool IsSSL
        {
            get
            {
                if (_isSSL == null)
                    _isSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);

                return _isSSL.Value;
            }
        }

        private static CachingConfigurationSection _caching = null;
        public static CachingConfigurationSection Caching
        {
            get
            {
                if (_caching == null)
                    _caching = (CachingConfigurationSection)ConfigurationManager.GetSection("cachingConfig");

                return _caching;
            }
        }
    }
}
