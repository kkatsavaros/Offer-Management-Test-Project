using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.BusinessModel.Flow;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal.Secure.Stores
{
    public partial class OfferDetails : BaseEntityPortalPage<Offer>
    {
        public Store CurrentStore { get; set; }
        protected List<string> ExistingOfferCodes { get; set; }

        #region [ Page Inits ]

        protected override void Fill()
        {
            int offerID = 0;
            if (int.TryParse(Request.QueryString["id"], out offerID))
                Entity = new OfferRepository(UnitOfWork).Load(offerID);

            var rule = new ApplicationRuleService().GetOfferCreationRules();
            if (Entity == null && !rule.IsActive())
            {
                mvOffer.SetActiveView(vOfferCreationNotAllowed);
            }

            CurrentStore = new StoreRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
            CurrentStore.SaveToCurrentContext();

            ExistingOfferCodes = new OfferRepository(UnitOfWork).GetOfferCodes(CurrentStore.ID, Entity == null ? null : (int?)Entity.ID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity != null)
            {
                if (Entity.OfferStatus != enOfferStatus.InEdit)
                {
                    Response.Redirect("Offers.aspx");
                }

                if (!Page.IsPostBack)
                {
                    ucRequirements.Entity = Entity;
                    ucRequirements.Bind();

                    ucOfferGeneralInfoInput.Entity = Entity;
                    ucOfferGeneralInfoInput.SetOfferCodesToCheck(ExistingOfferCodes);
                    ucOfferGeneralInfoInput.Bind();

                    ucOfferInput.Entity = Entity;
                    ucOfferInput.Bind();

                    btnSave.Text = "Αποθήκευση";
                    tdSaveAndSubmit.Visible = true;
                    tdCancel.Visible = true;
                }
            }
            else
            {
                ucOfferInput.Bind();
            }
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isEdit = Entity != null;
            bool isOfferValid = SaveOffer();

            if (!isOfferValid)
                return;

            if (isEdit)
            {
                Response.Redirect("Offers.aspx");
            }
            else
            {
                Response.Redirect(string.Format("OfferPreview.aspx?id={0}", Entity.ID));
            }
        }

        protected void btnSaveAndSubmit_Click(object sender, EventArgs e)
        {
            var rule = new ApplicationRuleService().GetOfferCreationRules();
            if (rule == null || !rule.IsActive())
            {
                ClientScript.RegisterStartupScript(GetType(), "offerSubmissionNotAllowed", "showAlertBox('Η υποβολή της προσφοράς δεν μπορεί να πραγματοποιηθεί, διότι έχει παρέλθει η προθεσμία υποβολής προσφορών για την Α Φάση.', true);", true);
            }

            var isEdit = Entity != null;
            bool isOfferValid = SaveOffer();

            if (!isOfferValid)
                return;

            if (isEdit)
            {
                var stateMachine = new OfferStateMachine(Entity);
                if (stateMachine.CanFire(enOfferTriggers.Submit))
                {
                    stateMachine.Submit(new OfferTriggerParams()
                    {
                        UnitOfWork = UnitOfWork,
                        Username = User.Identity.Name
                    });
                    UnitOfWork.Commit();
                }

                Response.Redirect("Offers.aspx");
            }
            else
            {
                Response.Redirect(string.Format("OfferPreview.aspx?id={0}", Entity.ID));
            }
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Offers.aspx");
        }

        #endregion

        #region [ Helper Methods ]

        private bool SaveOffer()
        {
            var offerCounters = new StoreRepository(UnitOfWork).GetOfferCounters(CurrentStore.ID);

            if (offerCounters != null && offerCounters.CreatedCount >=OfferManagementConstants.MAX_SUBMITTED_OFFERS)
            {
                phErrors.Visible = true;
                lblErrors.Text = string.Format("Η προσφορά δεν μπορεί να δημιουργηθεί, γιατί έχετε φτάσει στο όριο ({0}) των προσφορών.",OfferManagementConstants.MAX_SUBMITTED_OFFERS);
                return false;
            }

            if (Entity == null)
            {
                Entity = new Offer();
                Entity.IsActive = true;
                Entity.StoreID = CurrentStore.ID;

                UnitOfWork.MarkAsNew(Entity);
            }

            ucOfferGeneralInfoInput.Fill(Entity);
            ucOfferInput.Fill(Entity);

            var providerID = CurrentStore.ID;                             

            if (new OfferRepository(UnitOfWork).OfferCodeExists(providerID, Entity.ID, Entity.Code))
            {
                phErrors.Visible = true;
                lblErrors.Text = "Υπάρχει ήδη προσφορά με τον κωδικό που εισάγατε. Ο κωδικός πρέπει να είναι μοναδικός για κάθε προσφορά σας.";
                return false;
            }

            var minimumSpecsService = new MinimumSpecsService();

            MinimumSpecsErrorList errorList;
            bool isValid = minimumSpecsService.Validate(Entity, out errorList);

            phErrors.Visible = !isValid;

            if (!isValid)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");

                foreach (var error in errorList)
                {
                    sb.AppendFormat("<li class='firstListItem'>{0}</li>", error.Message);
                }

                sb.Append("<ul>");

                lblErrors.Text = sb.ToString();

                return false;
            }

            UnitOfWork.Commit();

            return true;
        }

        #endregion
    }
}