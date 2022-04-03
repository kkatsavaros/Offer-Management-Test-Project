using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class ApplicationRuleRepository : BaseRepository<ApplicationRule>
    {
        #region [ Base .ctors ]

        public ApplicationRuleRepository() : base() { }

        public ApplicationRuleRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}
