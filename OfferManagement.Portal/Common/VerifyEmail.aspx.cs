using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using System.Threading;

namespace OfferManagement.Portal.Common
{
    public partial class VerifyEmail : BaseEntityPortalPage<Reporter>
    {
        #region [ Entity Fill ]

        protected override void Fill()
        {
            Entity = new ReporterRepository(UnitOfWork).FindByEmailVerificationCode(Request.QueryString["id"]);
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                lblMessage.Text = "Η διεύθυνση ενεργοποίησης δεν είναι έγκυρη.";
            }
            else
            {
                if (Entity == null)
                {
                    lblMessage.Text = "Δεν βρέθηκε χρήστης με το email που δηλώσατε. Αν τυχόν αλλάξατε το email που είχατε δηλώσει ή πατήσατε 2 φορές το κουμπί για επαναποστολή Email Επιβεβαίωσης, βεβαιωθείτε ότι πατάτε στο σύνδεσμο του τελευταίου email που σας έχει έρθει.";
                }
                else if (Entity.IsEmailVerified.Value)
                {
                    lblMessage.Text = "Έχετε ήδη επιβεβαιώσει το email σας.";
                }
                else
                {
                    Entity.IsEmailVerified = true;
                    Entity.EmailVerificationDate = DateTime.Now;

                    UnitOfWork.Commit();

                    lblMessage.Text = "Η επιβεβαίωση του email σας πραγματοποιήθηκε επιτυχώς. Για να συνεχίσετε πατήστε <a runat='server' style='hyperlink' href='../Default.aspx'>εδώ</a>";
                }
            }
        }

        #endregion
    }
}
