using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public partial class ApplicationRule
    {
        public bool IsActive()
        {
            if (EnabledFrom.HasValue && EnabledTo.HasValue)
            {
                return IsEnabled && EnabledFrom.Value < DateTime.Now && EnabledTo.Value.AddDays(1) > DateTime.Now;
            }
            else
            {
                return IsEnabled;
            }
        }

        public string GetErrorMessage()
        {
            return ErrorMessage;
        }
    }
}
