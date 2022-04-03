using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using OfferManagement.BusinessModel;

namespace OfferManagement.Portal.Controls
{
    public class BaseUserControl : UserControl
    {
        public OfferManagementPrincipal User
        {
            get { return base.Page.User as OfferManagementPrincipal; }
        }
    }

    public class BaseUserControl<TPage> : BaseUserControl where TPage : Page
    {
        public new TPage Page { get { return (TPage)base.Page; } }
    }
}
