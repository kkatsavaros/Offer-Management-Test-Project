using Imis.Web.Controls;
using OfferManagement.Portal.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OfferManagement.Portal
{
    public partial class PopUp : BaseMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var urls = new
            {
                ViewConsultantUrl = ResolveClientUrl("~/Secure/Providers/EditorPopups/ViewConsultant.aspx"),
                ViewMapUrl = ResolveClientUrl("~/Common/ViewMap.aspx")
            };

            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "popupUrls",
                string.Format("var popupUrls = {0};", new JavaScriptSerializer().Serialize(urls)),
                true);
        }
    }
}