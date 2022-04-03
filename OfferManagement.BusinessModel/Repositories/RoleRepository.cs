namespace OfferManagement.BusinessModel
{
    public class RoleRepository : BaseRepository<Role>
    {
        #region [ Base .ctors ]

        public RoleRepository() : base() { }

        public RoleRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}
