using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal.Secure.Stores
{
    public partial class AccountDetails : BaseEntityPortalPage<Store>
    {
        #region [ Entity Fill ]

        protected override void Fill()
        {
            Entity = new StoreRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
            Entity.SaveToCurrentContext();
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Init(object sender, EventArgs e)
        {
            ucAccountDetails.UnitOfWork = UnitOfWork;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var reporter = Entity.Reporter;

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", string.Format(@"var _REPORTERID = {0};
                var _USERNAME = '{1}';", Entity.ID, reporter.UserName), true);

            ucAccountDetails.Entity = Entity.Reporter;
            ucAccountDetails.Bind();
        }

        #endregion
    }
}