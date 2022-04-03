using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class OfferChangeValues
    {
        public string Code { get; set; }
        public string OfferUrl { get; set; }
        public decimal? Price { get; set; }
        public decimal? CurrentPrice { get; set; }
    }
}
