using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.BusinessModel.Flow;
using OfferManagement.Portal.Controls;
using DevExpress.Web;
using Imis.Domain;

namespace OfferManagement.Portal.Secure.Stores
{
    public partial class StoreDetails : BaseEntityPortalPage<Store>
    {
        #region [ Entity Fill ]

        protected override void Fill()
        {
            Entity = new StoreRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name, x => x.Reporter.Address);
            Entity.SaveToCurrentContext();
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            var reporter = Entity.Reporter;

            if (!IsPostBack)
            {
                BindButtons();
                ucStoreInput.Entity = Entity;
                ucStoreInput.Bind();

                if (Entity.Reporter.VerificationStatus != enVerificationStatus.NoSubmittedRequest)
                {
                    ucStoreInput.Bind();

                    divRequestStatus.SetVerificationStatusBoxColor(Entity.Reporter);
                    SetVerificationStatusBoxText();

                    spCertificationRequestCode.InnerText = Entity.Reporter.VerificationNumber.ToString();
                    spCertificationDate.InnerText = Entity.Reporter.VerificationDate.HasValue ? Entity.Reporter.VerificationDate.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;
                }
                else
                {
                    divRequestStatus.Visible = false;
                    divRequestData.Visible = false;
                }
            }
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ASPxEdit.ValidateEditorsInContainer(Page, "vgStoreInput"))
                return;

            ucStoreInput.Fill(Entity);

            if (new StoreRepository(UnitOfWork).IsAfmVerified(Entity.ID, Entity.AFM))
            {
                phErrors.Visible = true;
                lblErrors.Text = "<br/>Οι αλλαγές δεν μπορούν να αποθηκευτούν, γιατί υπάρχει ήδη πιστοποιημένος Προμηθευτής με το Α.Φ.Μ. που δηλώσατε.<br/>";
                return;
            }

            UnitOfWork.Commit();

            RedirectAndNotify(Request.RawUrl, "Η ενημέρωση των εταιρικών στοιχείων πραγματοποιήθηκε επιτυχώς");
        }

        protected void btnVerificationRequest_Click(object sender, EventArgs e)
        {
            if (!ASPxEdit.ValidateEditorsInContainer(Page, "vgProviderInput"))
                return;

            if (DeadlineExpired())
                return;

            ucStoreInput.Fill(Entity);

            if (new StoreRepository(UnitOfWork).IsAfmVerified(Entity.ID, Entity.AFM))
            {
                phErrors.Visible = true;
                lblErrors.Text = "<br/>Οι αλλαγές δεν μπορούν να αποθηκευτούν, γιατί υπάρχει ήδη πιστοποιημένος Προμηθευτής με το Α.Φ.Μ. που δηλώσατε.<br/>";
                return;
            }

            var verificationStateMachine = new VerificationStateMachine(Entity.Reporter);
            if (verificationStateMachine.CanFire(enVerificationTriggers.Submit))
                verificationStateMachine.Submit(new VerificationTriggerParams() { UnitOfWork = UnitOfWork, Username = Thread.CurrentPrincipal.Identity.Name });

            UnitOfWork.Commit();
            Notify("Η υποβολή της Αίτησης Συμμετοχής στο Μητρώο Προμηθευτών πραγματοποιήθηκε επιτυχώς");

            //Ενημέρωσε την εγγραφή του Log με το σωστό VerificationNumber που μόλις μπήκε μέσω trigger
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var store = new StoreRepository(uow).Load(Entity.ID, x => x.Reporter.VerificationLog);
                var verificationNumber = store.Reporter.VerificationNumber;

                foreach (var item in store.Reporter.VerificationLog)
                {
                    if (item.OldVerificationStatus == enVerificationStatus.NoSubmittedRequest
                        && item.NewVerificationStatus == enVerificationStatus.SubmittedRequest
                        && !item.VerificationNumber.HasValue)
                    {
                        item.VerificationNumber = verificationNumber;
                        uow.Commit();
                    }
                }

                uow.Commit();
            }

            Response.Redirect(Request.RawUrl, true);
        }

        protected void btnRevertVerification_Click(object sender, EventArgs e)
        {
            if (DeadlineExpired())
                return;

            var verificationStateMachine = new VerificationStateMachine(Entity.Reporter);
            if (verificationStateMachine.CanFire(enVerificationTriggers.RevertSubmit))
                verificationStateMachine.RevertSubmit(new VerificationTriggerParams() { UnitOfWork = UnitOfWork, Username = Thread.CurrentPrincipal.Identity.Name });

            UnitOfWork.Commit();

            Response.Redirect(Request.RawUrl, true);
        }

        #endregion

        #region [ Helper Methods ]

        private void BindButtons()
        {
            var reporter = Entity.Reporter;

            btnVerificationRequest.Visible = reporter.VerificationStatus == enVerificationStatus.NoSubmittedRequest;
            btnRevertVerification.Visible = reporter.VerificationStatus == enVerificationStatus.SubmittedRequest;
            btnPrintCertification.Visible = reporter.VerificationStatus == enVerificationStatus.SubmittedRequest;
        }

        private void SetVerificationStatusBoxText()
        {
            switch (Entity.Reporter.VerificationStatus)
            {   
                case enVerificationStatus.SubmittedRequest:
                    divRequestStatus.InnerHtml = @"Η Αίτησή σας για συμμετοχή στο Μητρώο Προμηθευτών έχει εγκριθεί, με βάση τα τρέχοντα εταιρικά στοιχεία σας.
<div class='br'></div>Για να τροποποιήσετε τα στοιχεία σας θα πρέπει να κάνετε αναίρεση υποβολής.";
                    break;
            }
        }

        protected bool DeadlineExpired()
        {
            var ruleService = new ApplicationRuleService();
            var currentRule = ruleService.GetStoreRegistrationRules();

            return currentRule == null || !currentRule.IsActive();
        }

        #endregion
    }
}