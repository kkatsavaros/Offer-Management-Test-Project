using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal.UserControls.SearchFilters
{
    public enum enBaseOfferSearchFiltersMode
    {
        Store = 0,
        Helpdesk = 1
    }

    public partial class OfferSearchFiltersControl : BaseSearchFiltersControl<OfferSearchFilters>
    {
        public enBaseOfferSearchFiltersMode Mode { get; set; }

        public string ClientSideFiltersChanged { get; set; }

        #region [ Control Inits ]

        protected void ddlIsPublished_Init(object sender, EventArgs e)
        {
            ddlIsPublished.FillTrueFalse();
        }

        protected void ddlIsFranchisorOffer_Init(object sender, EventArgs e)
        {
            ddlIsFranchisorOffer.FillTrueFalse();
        }

        protected void ddlHasEvaluationComments_Init(object sender, EventArgs e)
        {
            ddlHasEvaluationComments.FillTrueFalse();
        }

        #endregion

        #region [ Page Inits ]

        public event EventHandler LastEvaluatedByInit
        {
            add { ddlLastEvaluatedBy.Init += value; }
            remove { ddlLastEvaluatedBy.Init -= value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tdHelpdeskFilters.Visible = Mode == enBaseOfferSearchFiltersMode.Helpdesk;

            if (!string.IsNullOrEmpty(ClientSideFiltersChanged))
            {
                chklOfferStatuses.ClientSideEvents.SelectedIndexChanged = ClientSideFiltersChanged;
            }

            if (!IsPostBack)
            {
                foreach (enOfferStatus item in Enum.GetValues(typeof(enOfferStatus)))
                {
                    if (item == enOfferStatus.Deleted || item == enOfferStatus.Withdrawn)
                        continue;

                    chklOfferStatuses.Items.Add(new ListEditItem(item.GetLabel(), item.GetValue()));
                }
            }
        }

        #endregion

        public override OfferSearchFilters GetSearchFilters()
        {
            var filters = new OfferSearchFilters();

            filters.OfferStatuses = chklOfferStatuses.SelectedValues.OfType<int>().Select(x => (enOfferStatus)x).ToList();
            filters.OfferID = ucOfferID.GetNumbers();
            filters.Code = txtCode.GetText();
            filters.Title = txtTitle.GetText();
            filters.Description = txtDescription.GetText(); // kostas
            filters.SubmissionDate = ucSubmissionDate.GetDates();
            filters.EvaluationDate = ucEvaluationDate.GetDates();

            if (Mode == enBaseOfferSearchFiltersMode.Helpdesk)
            {
                filters.AFM = txtAFM.GetText();
                filters.ClonedOffer = chbxHideClonedOffers.Checked;
                filters.FranchisorOffer = ddlIsFranchisorOffer.GetText();
                filters.StoreName = txtStoreName.GetText();
                filters.Published = ddlIsPublished.GetSelectedString();
                filters.LastEvaluatedBy = ddlLastEvaluatedBy.GetSelectedString();
            }
            return filters;
        }

        public override void SetSearchFilters(OfferSearchFilters filters)
        {
            //TODO: Types/Statuses binds

            txtCode.Text = filters.Code;
            txtTitle.Text = filters.Title;
            //txtDescription.Text = filters.Description;  Δεν έχει νόημα, παίζει και χωρίς αυτό.
        }
    }
}