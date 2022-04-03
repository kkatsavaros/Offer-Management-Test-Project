using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using OfferManagement.BusinessModel;
using Imis.Domain;

namespace OfferManagement.Portal.DataSources
{
    public class Prefectures : BaseDataSource<Prefecture>
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IEnumerable<Prefecture> GetAll()
        {
            return CacheManager.Prefectures.GetItems().Where(x => x.ID != 0);
        }
    }
}
