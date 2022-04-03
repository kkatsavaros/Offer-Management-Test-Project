using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Imis.Domain.EF.Extensions;

namespace OfferManagement.BusinessModel
{
    public class StoreRepository : BaseRepository<Store>
    {
        #region [ Base .ctors ]

        public StoreRepository() : base() { }

        public StoreRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public Store FindByUsername(string username, params Expression<Func<Store, object>>[] includeExpressions)
        {
            var query = BaseQuery;

            if (includeExpressions.Length > 0)
            {
                foreach (var item in includeExpressions)
                    query = query.Include(item);
            }

            return query
                    .Include(x => x.Reporter)
                    .FirstOrDefault(x => x.Reporter.UserName == username);
        }

        public List<int> FindIDsWithCriteria(Imis.Domain.EF.Search.Criteria<Store> expression)
        {
            var query = BaseQuery;

            if (!string.IsNullOrEmpty(expression.CommandText))
                query = query.Where(expression.CommandText, expression.Parameters);

            return query.Select(x => x.ID).ToList();
        }

        public bool IsAfmVerified(int currentReporterID, string afm)
        {
            return BaseQuery                    
                    .Where(x => x.AFM == afm)
                    .Where(x => x.Reporter.VerificationStatusInt == (int)enVerificationStatus.SubmittedRequest)
                    .Any(x => x.ID != currentReporterID);
        }

        public bool IsAfmVerified(string username, string afm)
        {
            return BaseQuery                    
                    .Where(x => x.AFM == afm)
                    .Where(x => x.Reporter.VerificationStatusInt == (int)enVerificationStatus.SubmittedRequest)
                    .Any(x => x.Reporter.UserName != username);
        }        
        public StoreOfferCounters GetOfferCounters(int storeID)
        {
            var views = GetCurrentObjectContext().GetStoreOfferCounters(storeID).ToList();
            if (views.Count != 1)
                return null;
            else
                return views[0];
        }
    }
}
