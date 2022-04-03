using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public enum enOfferStatus
    {
        Deleted = -2,
        Withdrawn = -1,
        InEdit = 0,
        Submitted = 1        
    }
}
