using Imis.Domain;

namespace OfferManagement.BusinessModel
{
    public class SmsRepository : BaseRepository<SMS>
    {
        #region [ Constructors ]

        public SmsRepository() : base() { }

        public SmsRepository(IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}
