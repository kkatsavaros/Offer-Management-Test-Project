using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using OfferManagement.BusinessModel;

namespace OfferManagement.Portal
{
    namespace CacheManagerExtensions
    {
        public static class PrivateExtensions
        {
            public static City GetCity(this int id)
            {
                return CacheManager.Cities.Get(id);
            }

            public static Prefecture GetPrefecture(this int id)
            {
                return CacheManager.Prefectures.Get(id);
            }
        }
    }

    public class CacheManager
    {
        static CacheManager()
        {   
            Cities.GetItems();
            Prefectures.GetItems();
            Regions.GetItems();
            Banks.GetItems();
        }

        private static string _changedProviderUsersKey = "_changedProviderUsers";
        private static Lazy<HashSet<string>> _changedStoreUsers = new Lazy<HashSet<string>>(() => new HashSet<string>(StringComparer.OrdinalIgnoreCase), true);
        public static HashSet<string> ChangedStoreUsers
        {
            get
            {
                return _changedStoreUsers.Value;
            }
        }

        public static OfferManagementCacheManager<City> Cities
        {
            get { return OfferManagementCacheManager<City>.Current; }
        }

        public static OfferManagementCacheManager<Prefecture> Prefectures
        {
            get { return OfferManagementCacheManager<Prefecture>.Current; }
        }

        public static OfferManagementCacheManager<Region> Regions
        {
            get { return OfferManagementCacheManager<Region>.Current; }
        }

        public static OfferManagementCacheManager<Bank> Banks
        {
            get { return OfferManagementCacheManager<Bank>.Current; }
        }

        #region [ Ordered Lists ]

        public static List<Region> GetOrderedRegions()
        {
            return OfferManagementCacheManager<Region>.Current
                    .GetItems()
                    .Where(x => x.ID != 0)
                    .OrderBy(x => x.Name)
                    .ToList();
        }

        public static List<Prefecture> GetOrderedPrefectures()
        {
            return OfferManagementCacheManager<Prefecture>.Current
                    .GetItems()
                    .Where(x => x.ID != 0)
                    .OrderBy(x => x.Name)
                    .ToList();
        }

        public static List<City> GetOrderedCities(int prefectureID)
        {
            return OfferManagementCacheManager<City>.Current
                        .GetItems()
                        .Where(x => x.PrefectureID == prefectureID)
                        .OrderBy(x => x.Name)
                        .ToList();
        }

        public static List<Bank> GetOrderedBanks()
        {
            return OfferManagementCacheManager<Bank>.Current
                        .GetItems()
                        .OrderBy(x => x.Name)
                        .ToList();
        }

        #endregion

        public static void Initialize()
        {   
            Regions.GetItems();
            Prefectures.GetItems();
            Cities.GetItems();
            Banks.GetItems();
        }

        public static void Refresh()
        {   
            Regions.Refresh();
            Prefectures.Refresh();
            Cities.Refresh();
            Banks.GetItems();
        }
    }
}
