using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class TagRepository : BaseRepository<Tag>
    {
        #region [ Constructors ]

        public TagRepository() : base() { }

        public TagRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public Tag FindByName(string name)
        {
            return BaseQuery.FirstOrDefault(x => x.Name == name);
        }

        public bool TagExists(string name)
        {
            return BaseQuery.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Tag> FindMany(IEnumerable<string> tagNames)
        {
            return BaseQuery.Where(x => tagNames.Contains(x.Name)).ToList();
        }

        public List<Tag> GetAll(enTagType? tagTypes = null)
        {
            if (tagTypes.HasValue)
            {
                return BaseQuery
                        .Where(x => (x.TagTypeInt & (int)tagTypes.Value) == x.TagTypeInt)
                        .OrderBy(x => x.Name)
                        .ToList();
            }
            else
            {
                return BaseQuery
                        .OrderBy(x => x.Name)
                        .ToList();
            }
        }
    }
}
