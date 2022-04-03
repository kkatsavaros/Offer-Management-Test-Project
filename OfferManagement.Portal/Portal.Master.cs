using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OfferManagement.BusinessModel;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Imis.Web.Controls;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal
{
    public partial class Portal : BaseMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var urls = new
            {
                ParticipationTermsUrl = ResolveClientUrl("~/Common/ParticipationTerms.aspx"),
                UsageTermsUrl = ResolveClientUrl("~/Common/UsageTerms.aspx")
            };

            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "portalUrlLinks",
                string.Format("var portalUrls = {0};", new JavaScriptSerializer().Serialize(urls)),
                true);

            phPilotApplication.Visible = Config.IsPilotSite && !Request.IsLocal;
        }
    }
}