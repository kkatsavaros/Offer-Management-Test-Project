using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imis.Domain;

namespace OfferManagement.BusinessModel.Flow
{
    public class OfferTriggerParams
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public string Username { get; set; }
        public string EvaluationComments { get; set; }
    }
}
