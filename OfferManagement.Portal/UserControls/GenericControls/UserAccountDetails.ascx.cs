using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Imis.Domain;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal.UserControls.GenericControls
{
    public partial class UserAccountDetails : BaseEntityUserControl<Reporter>
    {
        public UserAccountDetails()
        {
            ShowUsername = true;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public bool ShowMobilePhone { get; set; }

        public bool ShowUsername { get; set; }

        public string Width
        {
            get { return tbl.Width; }
            set { tbl.Width = value; }
        }

        public bool HideEmailChangeButton
        {
            set { btnEmail.Visible = !value; }
        }

        public bool HideMobilePhoneChangeButton
        {
            set { btnMobilePhone.Visible = !value; }
        }

        public bool HideMobilePhoneVerificationInfo { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            trMobilePhone.Visible = ShowMobilePhone;
            trUsername.Visible = ShowUsername;

            if (!string.IsNullOrEmpty(Message))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "showMessage_onDemandNotification", string.Format("Imis.Lib.notify('{0}')", Message), true);
            }

            base.OnPreRender(e);
        }

        protected string Message { get; set; }

        #region [ Bind - Extract ]

        public override void Bind()
        {
            BindData(Entity);
        }

        private void BindData(Reporter reporter)
        {
            if (string.IsNullOrEmpty(reporter.Email))
            {
                btnEmail.Visible = false;
                btnAddEmail.Visible = true;
            }

            if (string.IsNullOrEmpty(reporter.ContactMobilePhone))
            {
                btnMobilePhone.Visible = false;
                btnAddMobilePhone.Visible = true;
            }

            ltrUsername.Text = reporter.UserName;
            ltrEmail.Text = reporter.Email;
            ltrMobilePhone.Text = reporter.ContactMobilePhone;

            imgEmailVerified.Visible = reporter.IsEmailVerified.Value && !string.IsNullOrEmpty(reporter.Email);
            imgEmailNotVerified.Visible = !reporter.IsEmailVerified.Value && !string.IsNullOrEmpty(reporter.Email);

            imgMobilePhoneVerified.Visible = reporter.IsMobilePhoneVerified.Value && !string.IsNullOrEmpty(reporter.ContactMobilePhone);
            imgMobilePhoneNotVerified.Visible = !reporter.IsMobilePhoneVerified.Value && !string.IsNullOrEmpty(reporter.ContactMobilePhone);

            btnVerifyMobilePhone.Visible = !reporter.IsMobilePhoneVerified.Value && !string.IsNullOrEmpty(reporter.ContactMobilePhone);

            if (reporter.IsEmailVerified.Value || string.IsNullOrEmpty(reporter.Email))
            {
                btnSendEmailVerification.Visible = false;
            }

            if (reporter.IsMobilePhoneVerified.Value || string.IsNullOrEmpty(reporter.ContactMobilePhone))
            {
                btnSendMobilePhoneVerification.Visible = false;
                btnVerifyMobilePhone.Visible = false;
                btnInsertVerificationCode.Visible = false;
            }

            if (HideMobilePhoneVerificationInfo)
            {
                imgMobilePhoneVerified.Visible = false;
                imgMobilePhoneNotVerified.Visible = false;
                btnSendMobilePhoneVerification.Visible = false;
                btnVerifyMobilePhone.Visible = false;
                btnInsertVerificationCode.Visible = false;
            }
        }

        public override Reporter Fill()
        {
            return base.Fill();
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnSendEmailVerification_Click(object sender, EventArgs e)
        {
            VerificationHelper.SendEmailVerification(Entity, UnitOfWork);

            Message = "Η επαναποστολή του email επιβεβαίωσης πραγματοποιήθηκε επιτυχώς";
        }

        protected void btnSendMobilePhoneVerification_Click(object sender, EventArgs e)
        {
            if (Entity.IsMobilePhoneVerified.Value || Entity.SMSSentCount.Value >= Config.MaxSMSAllowed)
            {
                lblContactInfoErrors.Text = "Έχετε ξεπεράσει τον μέγιστο επιτρεπόμενο αριθμό επαναποστολών του SMS Επιβεβαίωσης. Παρακαλούμε επικοινωνήστε με το Γραφείο Υποστήριξης Χρηστών.";
                return;
            }

            VerificationHelper.SendMobilePhoneVerificationInfo(Entity, UnitOfWork);

            Message = "Η επαναποστολή του SMS επιβεβαίωσης πραγματοποιήθηκε επιτυχώς";
        }

        protected void btnInsertVerificationCode_Click(object sender, EventArgs e)
        {
            mvAccountDetails.SetActiveView(vMobilePhoneVerification);
        }

        protected void btnVerifyMobilePhone_Click(object sender, EventArgs e)
        {
            if (Entity.MobilePhoneVerificationCode != txtMobilePhoneVerificationCode.GetText())
            {
                lblMobilePhoneVerificationErrors.Text = "Ο κωδικός που εισαγάγατε δεν είναι σωστός.";
                return;
            }

            Entity.IsMobilePhoneVerified = true;
            Entity.MobilePhoneVerificationDate = DateTime.Now;

            UnitOfWork.Commit();

            mvAccountDetails.SetActiveView(vAccountDetails);
            BindData(Entity);

            Message = "Το κινητό σας τηλέφωνο επιβεβαιώθηκε επιτυχώς";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvAccountDetails.SetActiveView(vAccountDetails);
        }

        #endregion
    }
}