using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class EmailRepository : BaseRepository<Email>
    {
        #region [ Constructors ]

        public EmailRepository() : base() { }

        public EmailRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public List<Email> FindByEmailTypeAndEntityIDs(enEmailType type, IEnumerable<int?> entityIDs)
        {
            return BaseQuery.Where(x => x.TypeInt == (int)type && entityIDs.Contains(x.EntityID)).ToList();
        }

        public Email FindByEmailTypeAndEntityID(enEmailType type, int entityID)
        {
            return BaseQuery.Where(x => x.TypeInt == (int)type && x.EntityID == entityID).FirstOrDefault();
        }

        public List<Email> FindByEmailTypesAndEntityIDs(IEnumerable<int> types, IEnumerable<int?> entityIDs)
        {
            return BaseQuery.Where(x => types.Contains(x.TypeInt) && entityIDs.Contains(x.EntityID)).ToList();
        }

        public List<Email> FindByEmailType(IEnumerable<int> types, IEnumerable<int> entityIDs = null)
        {
            if (entityIDs == null)
                return BaseQuery.Where(x => types.Contains(x.TypeInt)).ToList();
            else
                return BaseQuery.Where(x => types.Contains(x.TypeInt) && x.EntityID.HasValue && entityIDs.Contains(x.EntityID.Value)).ToList();
        }
    }
}
