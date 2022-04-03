using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OfferManagement.Portal
{
    public partial class EmailVerificationInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltSubject.Text = "Επιβεβαίωση Email Λογαριασμού";

            var type = Request.QueryString["t"];

            if (type == "4")
            {
                ltSubject.Text = "Επιβεβαίωση Email Λογαριασμού Προμηθευτή";
                ltFirstBullet.Text = "Πηγαίνετε στην καρτέλα «Στοιχεία Λογαριασμού» για να δείτε το email που έχετε δηλώσει και, εάν έχετε κάνει λάθος να το διορθώσετε. Μόλις το διορθώσετε, θα σας έρθει νέο email επιβεβαίωσης.";
                ltSecondBullet.Text = "Το email επιβεβαίωσης που σας στάλθηκε, να έχει μαρκαριστεί ως SPAM και να έχει καταλήξει στην Ανεπιθύμητη Αλληλογραφία του γραμματοκιβωτίου σας. Ψάξτε, λοιπόν, στον φάκελο της Ανεπιθύμητης Αλληλογραφίας (Junk). Σε αυτήν την περίπτωση, να έχετε υπόψη σας ότι κάθε email που σας στέλνει η εφαρμογή θα καταλήγει στην Ανεπιθύμητη Αλληλογραφία σας. Για να το αποφύγετε αυτό, μπορείτε να ορίσετε ένα διαφορετικό email λογαριασμό (εφόσον διαθέτετε) που να μην μαρκάρει τα email της εφαρμογής ως Ανεπιθύμητη Αλληλογραφία.";
            }
        }
    }
}