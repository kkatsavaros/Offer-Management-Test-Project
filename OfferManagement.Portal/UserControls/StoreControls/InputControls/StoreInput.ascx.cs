using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using DevExpress.Web;
using Imis.Domain;

namespace OfferManagement.Portal.UserControls.StoreControls.InputControls
{
    public partial class StoreInput : BaseEntityUserControl<Store>
    {
        #region [ Control Inits ]

        protected void ddlCompanyType_Init(object sender, EventArgs e)
        {
            ddlCompanyType.FillFromEnum<enCompanyType>("-- επιλέξτε τύπο επιχείρησης --");
        }

        protected void ddlDOY_Init(object sender, EventArgs e)
        {
            ddlDOY.FillDOYs();
        }

        protected void ddlBank_Init(object sender, EventArgs e)
        {
            ddlBank.FillBanks();
        }

        protected void ddlLegalPersonIdentificationType_Init(object sender, EventArgs e)
        {

            mvIdentificationType.SetActiveView(vID);
        }

        protected void cbpIdentificationType_Callback(object source, CallbackEventArgsBase e)
        {
            int identificationType;
            if (int.TryParse(e.Parameter, out identificationType) && identificationType > 0)
            {
                ShowIdentificationType((enIdentificationType)identificationType);
            }
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Init(object sender, EventArgs e)
        {
            txtStoreFax.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetGreekFixedPhoneRegExp();
            txtStoreEmail.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetEmailRegExp();

            txtIDNumber.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetGreekIdCardRegExp();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region [ Extract - Bind ]

        public override Store Fill(Store entity)
        {
            var name = Regex.Replace(txtName.GetText(), @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)", string.Empty).ToNull();


            entity.CompanyType = ddlCompanyType.GetSelectedEnum<enCompanyType>();
            entity.Name = name;
            entity.AFM = txtAFM.GetText();
            entity.DOY = ddlDOY.GetSelectedString();
            entity.Phone = txtStorePhone.GetText();
            entity.Fax = txtStoreFax.GetText();
            entity.Email = txtStoreEmail.GetText();
            entity.URL = txtStoreUrl.GetHyperLink();

            //Στοιχεία Διεύθυνσης Προμηθευτή
            ucGreekAddressInfoInput.Fill(entity.Reporter.Address);


            //Στοιχεία Υπευθύνου του Προμηθευτή
            ucContactPersonInput.Fill(entity.Reporter);

            //Τραπεζικά Στοιχεία
            if (!entity.IsNew)
            {
                entity.BankID = ddlBank.GetSelectedInteger();
                var iban = txtIBAN.GetText();
                entity.IBAN = string.IsNullOrEmpty(iban) ? string.Empty : iban.Replace(" ", "");
            }

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null)
                return;

            //Στοιχεία Προμηθευτή
            ddlCompanyType.SelectedItem = ddlCompanyType.Items.FindByValue(Entity.CompanyTypeInt);
            txtName.Text = Entity.Name;
            txtAFM.Text = Entity.AFM;
            ddlDOY.SelectedItem = ddlDOY.Items.FindByValue(Entity.DOY);
            txtStorePhone.Text = Entity.Phone;
            txtStoreFax.Text = Entity.Fax;
            txtStoreEmail.Text = Entity.Email;
            txtStoreUrl.Bind(Entity.URL);

            //Στοιχεία Διεύθυνσης Προμηθευτή
            ucGreekAddressInfoInput.Entity = Entity.Reporter.Address;
            ucGreekAddressInfoInput.Bind();
            
            //Στοιχεία Υπευθύνου του Προμηθευτή
            ucContactPersonInput.Entity = Entity.Reporter;
            ucContactPersonInput.Bind();

            //Τραπεζικά Στοιχεία
            if (!Entity.IsNew)
            {
                phBankDetails.Visible = true;

                ddlBank.SelectedItem = ddlBank.Items.FindByValue(Entity.BankID);
                txtIBAN.Text = Entity.IBAN;
            }
        }

        #endregion

        #region [ Validation ]

        public string ValidationGroup
        {
            get { return txtName.ValidationSettings.ValidationGroup; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                {
                    control.ValidationSettings.ValidationGroup = value;
                }

                ucGreekAddressInfoInput.ValidationGroup = value;                
            }
        }

        protected void txtAFM_Validation(object sender, ValidationEventArgs e)
        {
            e.IsValid = ValidationHelper.CheckAFM(e.Value.ToString());
            e.ErrorText = "Το Α.Φ.Μ. δεν είναι έγκυρο";
        }

        protected void txtIBAN_Validation(object sender, ValidationEventArgs e)
        {
            if (e.Value != null)
            {
                var iban = e.Value.ToString().Replace(" ", "");

                e.IsValid = ValidationHelper.CheckIBAN(iban);
                e.ErrorText = ValidationHelper.ValidateIBAN(iban).GetLabel();
            }
            else
            {
                e.IsValid = true;
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnPreRender(EventArgs e)
        {
            if (Entity != null && !ReadOnly)
            {
                switch (Entity.Reporter.VerificationStatus)
                {
                    case enVerificationStatus.NoSubmittedRequest:
                        ReadOnly = false;
                        break;
                    case enVerificationStatus.SubmittedRequest:                    
                        ReadOnly = true;
                        break;
                }
            }

            base.OnPreRender(e);
        }

        #endregion

        #region [ Properties ]

        public bool ReadOnly
        {
            get { return txtName.ReadOnly; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                {
                    control.ReadOnly = value;
                    control.ClientEnabled = !value;
                }

                txtStoreUrl.ReadOnly =
                    ucGreekAddressInfoInput.ReadOnly =
                    ucContactPersonInput.ReadOnly = value;
            }
        }

        #endregion

        #region [ Helper Methods ]

        private void ShowIdentificationType(enIdentificationType identificationType)
        {
            if (identificationType == enIdentificationType.ID)
            {
                txtLegalPersonIdentificationIssueDate.MaxDate = DateTime.Today;
                mvIdentificationType.SetActiveView(vID);
            }
            else if (identificationType == enIdentificationType.Passport)
            {
                mvIdentificationType.SetActiveView(vPassport);
            }

            cbpIdentificationType.DataBind();
        }

        #endregion
    }
}