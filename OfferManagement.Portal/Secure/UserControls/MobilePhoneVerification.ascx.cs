using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OfferManagement.Portal.Secure.UserControls
{
    public partial class MobilePhoneVerification : BaseEntityUserControl<Reporter, BaseEntityPortalPage<Reporter>>
    {
        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Entity.IsMobilePhoneVerified.Value)
            {
                mvVerification.SetActiveView(vNotVerified);
            }
            else
            {
                mvVerification.SetActiveView(vVerified);

                lblVerificationCode.Text = Entity.MobilePhoneVerificationCode;
                lblVerificationDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}", Entity.MobilePhoneVerificationDate);
            }
        }

        #endregion

        #region [ Button Handlers ]

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Entity.MobilePhoneVerificationCode == txtVerificationCode.GetText())
            {
                Entity.IsMobilePhoneVerified = true;
                Entity.MobilePhoneVerificationDate = DateTime.Now;

                Page.UnitOfWork.Commit();

                Response.Redirect("MobilePhoneVerification.aspx");
            }
            else
            {
                lblErrors.Text = "Ο κωδικός που εισαγάγατε δεν είναι σωστός.";
            }
        }

        #endregion
    }
}