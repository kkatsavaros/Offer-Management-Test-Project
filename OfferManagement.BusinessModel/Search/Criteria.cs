using System.Data.Objects.DataClasses;

namespace OfferManagement.BusinessModel
{
    public class Criteria<T> : Imis.Domain.EF.DomainCriteria<T> where T : EntityObject
    {
    }
}