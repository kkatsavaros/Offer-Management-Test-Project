namespace OfferManagement.BusinessModel
{
    public class PrefectureRepository : BaseRepository<Prefecture>
    {
        #region [ Base .ctors ]

        public PrefectureRepository() : base() { }

        public PrefectureRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}