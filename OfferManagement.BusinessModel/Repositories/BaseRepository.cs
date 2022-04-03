using Imis.Domain.EF;

namespace OfferManagement.BusinessModel
{
    public class BaseRepository<TEntity> : DomainRepository<DBEntities, TEntity, int>
        where TEntity : DomainEntity<DBEntities>
    {
        public BaseRepository() : base() { }

        public BaseRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }
    }
}
