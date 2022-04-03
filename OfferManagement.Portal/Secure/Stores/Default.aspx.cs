using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OfferManagement.Portal.Secure.Stores
{
    public partial class Default : BaseEntityPortalPage<Store>
    {
        protected override void Fill()
        {
            Entity = new StoreRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
            Entity.SaveToCurrentContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            var reporter = Entity.Reporter;

            switch (reporter.VerificationStatus)
            {
                case enVerificationStatus.NoSubmittedRequest:
                    mvVerificationStatus.SetActiveView(vNoSubmittedRequest);
                    break;
                case enVerificationStatus.SubmittedRequest:
                    mvVerificationStatus.SetActiveView(vSubmittedRequest);

                    lblStoreName.Text = string.Format("{0}", Entity.Name);


                    phActions.Visible = true;
                    
                    break;
            }
        }
    }
}