using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Imis.Domain.EF.Extensions;

namespace OfferManagement.BusinessModel
{
    public class OfferRepository : BaseRepository<Offer>
    {
        #region [ Base .ctors ]

        public OfferRepository() : base() { }

        public OfferRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        protected ObjectQuery<Offer> ActiveOfferQuery
        {
            get
            {
                return BaseQuery.Where(string.Format("it.IsActive = true AND it.OfferStatusInt NOT IN MULTISET ({0}, {1})", enOfferStatus.Deleted.GetValue(), enOfferStatus.Withdrawn.GetValue()));
            }
        }

        protected ObjectQuery<Offer> PublishedOfferQuery
        {
            get
            {
                return BaseQuery.Where(string.Format("it.IsActive = true AND it.IsPublished = true AND it.OfferStatusInt = {0}", enOfferStatus.Submitted.GetValue()));
            }
        }        

        public Offer FindByIDAndStoreID(int offerID, int storeID, params Expression<Func<Offer, object>>[] includes)
        {
            var query = BaseQuery;

            foreach (var item in includes)
                query = query.Include(item);

            return query
                    .Where(x => x.ID == offerID)
                    .Where(x => x.StoreID == storeID)
                    .FirstOrDefault();
        }

        public Offer LoadActive(int id, params Expression<Func<Offer, object>>[] includes)
        {
            var query = ActiveOfferQuery;

            foreach (var item in includes)
                query = query.Include(item);

            return query.FirstOrDefault(x => x.ID == id);
        }

        public Offer Load(string code, int storeID, int? masterStoreID, params Expression<Func<Offer, object>>[] includes)
        {
            var query = BaseQuery;

            foreach (var item in includes)
                query = query.Include(item);

            return query.Where(x => x.Code == code)
                        .Where(x => x.StoreID == storeID || x.StoreID == masterStoreID)
                        .FirstOrDefault();
        }

        public Offer LoadPublished(string code, int storeID, int? masterStoreID, params Expression<Func<Offer, object>>[] includes)
        {
            var query = PublishedOfferQuery;

            foreach (var item in includes)
                query = query.Include(item);

            return query.Where(x => x.Code == code)
                        .Where(x => x.StoreID == storeID || x.StoreID == masterStoreID)
                        .FirstOrDefault();
        }

        public List<Offer> GetStorePublishedOffers(int storeID)
        {
            return PublishedOfferQuery
                    .Where(x => x.StoreID == storeID)
                    .ToList();
        }

        public int GetOfferCountForProvider(string username)
        {
            return BaseQuery.Where(x => x.CreatedBy == username).Count();
        }

        public int GetOfferCountForProvider(IEnumerable<string> usernames)
        {
            return BaseQuery.Where(x => usernames.Contains(x.CreatedBy)).Count();
        }        

        public List<string> GetOfferCodes(int storeID, int? excludedOfferID = null)
        {
            var query = BaseQuery
                    .Where(string.Format("it.OfferStatusInt NOT IN MULTISET ({0}, {1})", enOfferStatus.Deleted.GetValue(), enOfferStatus.Withdrawn.GetValue()))
                    .Where(x => x.StoreID == storeID);

            if (excludedOfferID.HasValue)
                return query.Where(x => x.ID != excludedOfferID.Value).Select(x => x.Code).Distinct().ToList();
            else
                return query.Select(x => x.Code).Distinct().ToList();
        }

        public bool OfferCodeExists(int storeID, int offerID, string code)
        {
            return BaseQuery
                    .Where(string.Format("it.OfferStatusInt NOT IN MULTISET ({0}, {1})", enOfferStatus.Deleted.GetValue(), enOfferStatus.Withdrawn.GetValue()))
                    .Where(x => x.StoreID == storeID)
                    .Where(x => x.ID != offerID)
                    .Any(x => x.Code == code);
        }

        public List<string> GetLastEditByValues()
        {
            return BaseQuery
                    .Where(x => x.UpdatedBy != null)
                    .Select(x => x.UpdatedBy)
                    .Distinct()
                    .ToList();
        }

        public List<string> GetLastEvaluatedByValues()
        {
            return BaseQuery
                    .Where(x => x.EvaluatedBy != null)
                    .Select(x => x.EvaluatedBy)
                    .Distinct()
                    .ToList();
        }

        public List<Offer> GetOffersOfStore(int storeID, params Expression<Func<Offer, object>>[] includes)
        {
            var query = PublishedOfferQuery;

            foreach (var item in includes)
                query = query.Include(item);

            return query.Where(x => x.StoreID == storeID).ToList();
        }
    }
}
