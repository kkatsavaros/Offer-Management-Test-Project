using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using System.Web.Security;

namespace OfferManagement.Portal.Secure.Stores
{
    public partial class Stores : System.Web.UI.MasterPage
    {
        public Store CurrentStore { get; set; }

        public bool HideSiteMap { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            if (HideSiteMap)
            {
                alertsArea.Visible = false;
                registeredUsersMenu.Visible = false;
            }

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var urls = new
            {
                ChangePasswordUrl = ResolveClientUrl("~/Common/AlterPassword.aspx"),
                ViewOfferUrl = ResolveClientUrl("~/Common/ViewOffer.aspx")
            };

            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "storeUrls",
                string.Format("var storeUrls = {0};", new JavaScriptSerializer().Serialize(urls)),
                true);

            CurrentStore = Context.LoadStore() ??
                               new StoreRepository().FindByUsername(Page.User.Identity.Name, x => x.Reporter);

            if (CurrentStore != null)
            {
                BindControls(CurrentStore.Reporter);

                lblName.Text = CurrentStore.Name;
            }

            SetAlerts(CurrentStore);
        }
        protected void BindControls(Reporter reporter)
        {
            if (reporter != null)
            {
                if (reporter.MustChangePassword)
                {
                    Response.Redirect("~/Common/ChangePassword.aspx");
                }
            }
        }

        protected bool IsCurrentOrParentNode(object node)
        {
            var smNode = node as SiteMapNode;
            if (smNode != null && smNode.Provider.CurrentNode != null)
            {
                return smNode == smNode.Provider.CurrentNode || smNode == smNode.Provider.CurrentNode.ParentNode;
            }

            return false;
        }

        protected void repMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SiteMapNode thisMapNode = (SiteMapNode)e.Item.DataItem;
            }
        }

        private void SetAlerts(Store store)
        {
            bool haveAlerts = false;
            StringBuilder sb = new StringBuilder();

            var reporter = store.Reporter;

            switch (reporter.VerificationStatus)
            {
                case enVerificationStatus.NoSubmittedRequest:
                    haveAlerts = true;
                    sb.Append("Δεν έχετε ακόμη ενταχθεί στο Μητρώο Προμηθευτών.<br/>");
                    sb.Append("Θα πρέπει να ακολουθήσετε τις οδηγίες που δίνονται στην <a runat='server' class='hyperlink' href='Default.aspx'>Αρχική Σελίδα</a> για να προχωρήσει η διαδικασία της ένταξής σας.");
                    break;
            }

            if (!reporter.IsEmailVerified.Value)
            {
                haveAlerts = true;
                sb.Append("<ul>");
                sb.Append("<li class='firstListItem'>Δεν έχετε ακόμη επιβεβαιώσει το email που έχετε δηλώσει (");
                sb.Append(reporter.Email);
                sb.Append(@"). Για οδηγίες πατήστε <a href='javascript:void(0)' onclick='window.open(""../../EmailVerificationInfo.aspx?t=4"",""colourExplanation"",""toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=700, height=350""); return false;' target='_blank'>εδώ</a></li>");
                sb.Append("</ul>");
            }

            if (haveAlerts)
            {
                alertsArea.Visible = true;
                ltAlerts.Text = sb.ToString();
            }
            else
            {
                alertsArea.Visible = false;
            }
        }
    }
}