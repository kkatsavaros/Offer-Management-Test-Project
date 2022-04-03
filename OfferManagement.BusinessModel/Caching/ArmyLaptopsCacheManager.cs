using Imis.Domain.EF;

namespace OfferManagement.BusinessModel
{
    public class OfferManagementCacheManager<TEntity> : DomainCacheManager<DBEntities, TEntity, int>
        where TEntity : DomainEntity<DBEntities>
    {
        protected OfferManagementCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        #region Thread-safe, lazy Singleton

        public static OfferManagementCacheManager<TEntity> Current
        {
            get { return Nested._cacheManager; }
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private sealed class Nested
        {
            static Nested() { }
            internal static readonly OfferManagementCacheManager<TEntity> _cacheManager = new OfferManagementCacheManager<TEntity>();
        }

        #endregion
    }
}
