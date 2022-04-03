using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel.Flow
{
    public enum enOfferTriggers
    {
        Delete,
        Submit,
        RevertSubmit,
        Withdraw,
        RevertWithdraw,
        PreviousState
    }
}
