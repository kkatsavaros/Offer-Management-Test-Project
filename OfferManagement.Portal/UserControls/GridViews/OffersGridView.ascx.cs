using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal.UserControls.GridViews
{
    public partial class OffersGridView : BaseGridViewUserControl
    {
        #region [ Properties ]

        public override ASPxGridView Grid
        {
            get { return gvOffers; }
        }

        public bool UseDefaultColors { get; set; }

        public ASPxGridViewExporter Exporter { get { return gveOffers; } }

        public event EventHandler<ASPxGridViewExportRenderingEventArgs> ExporterRenderBrick;

        #endregion

        #region [ GridView Events ]

        protected void gvOffers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (UseDefaultColors)
                return;

            var offer = (Offer)gvOffers.GetRow(e.VisibleIndex);

            if (offer != null)
            {
                if (!offer.IsActive)
                {
                    e.Row.BackColor = Color.FromArgb(255, 153, 51);
                    return;
                }

                switch (offer.OfferStatus)
                {
                    case enOfferStatus.Submitted:
                        if (offer.IsPublished)
                        {
                            e.Row.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            e.Row.BackColor = Color.LightBlue;
                        }
                        break;
                }
            }
        }

        protected void gveOffers_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {

            Offer offer = gvOffers.GetRow(e.VisibleIndex) as Offer;

            if (offer == null)
                return;

            if (e.Column.Name == "OfferStatus")
            {
                e.Text = offer.GetOfferStatus();
            }

            if (e.Column.Name == "ProviderDetails")
            {
                e.Text = offer.Store.Reporter.GetReporterDetails().Replace("<br/>", " ");
            }

            if (e.Column.Name == "ContactDetails")
            {
                e.Text = offer.Store.Reporter.GetContactDetails().Replace("<br/>", " ");
            }

            if (e.Column.Name == "OfferDetails")
            {
                e.Text = offer.GetSummary().Replace("<br/>", " ");
            }

            gvOffers.Columns["UndoAction"].Visible = false;
            e.TextValue = e.Text;
        }

        #endregion
    }
}