using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public static class TagManager
    {
        public static void SaveTag(string tag)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var existingTag = new TagRepository(uow).FindByName(tag);
                if (existingTag == null)
                {
                    uow.MarkAsNew(new Tag() { Name = tag, TagType = enTagType.Manufacturer });

                    try
                    {
                        uow.Commit();
                    }
                    catch (UpdateException)
                    {
                        //Tag already exists
                    }
                }
            }
        }

        public static void SaveTags(List<string> tags)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var existingTags = new TagRepository(uow).FindMany(tags);

                tags.ForEach(tag =>
                {
                    if (!string.IsNullOrWhiteSpace(tag) && !existingTags.Any(x => x.Name.Equals(tag, StringComparison.OrdinalIgnoreCase)))
                        uow.MarkAsNew(new Tag() { Name = tag, TagType = enTagType.Manufacturer });
                });

                try
                {
                    uow.Commit();
                }
                catch (UpdateException)
                {
                    //Tag already exists
                }
            }

        }
    }
}
