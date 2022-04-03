namespace OfferManagement.BusinessModel
{
    public class VerificationLogRepository : BaseRepository<VerificationLog>
    {
        #region [ Base .ctors ]

        public VerificationLogRepository() : base() { }

        public VerificationLogRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}
