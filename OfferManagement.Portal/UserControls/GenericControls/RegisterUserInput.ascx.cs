using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.Portal.Controls;
using OfferManagement.BusinessModel;
using System.Web.Security;
using DevExpress.Web;
using System.ComponentModel;
using OfferManagement.Portal.Utils;
using Imis.Domain;

namespace OfferManagement.Portal.UserControls.GenericControls
{
    public partial class RegisterUserInput : BaseEntityUserControl<Reporter>
    {
        #region [ Properties ]

        public string UserName
        {
            get { return txtUserName.GetText(); }
            set { txtUserName.Text = value; }
        }
        public string Password
        {
            get { return txtPassword.GetText(); }
            set { txtPassword.Text = value; }
        }
        public string Email
        {
            get { return txtEmail.GetText(); }
            set { txtEmail.Text = txtEmailConfirmation.Text = value; }
        }
        public string MobilePhone
        {
            get { return txtContactMobilePhone.Text; }
            set { txtContactMobilePhone.Text = txtContactMobilePhoneConfirmation.Text = value; }
        }

        #endregion

        #region [ Control Inits ]

        protected void txtUserName_Callback(object source, CallbackEventArgsBase e)
        {

            if (Membership.GetUser(e.Parameter) != null)
            {
                txtUserName.IsValid = false;
                txtUserName.ErrorText = "Το Όνομα Χρήστη χρησιμοποιείται";
            }
        }

        protected void txtEmail_Callback(object source, CallbackEventArgsBase e)
        {

            if (!string.IsNullOrEmpty(Membership.GetUserNameByEmail(e.Parameter)))
            {
                txtEmail.IsValid = false;
                txtEmail.ErrorText = "Το 'Email χρησιμοποιείται";
            }
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Init(object sender, EventArgs e)
        {
            txtUserName.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetUsernameRegExp();
            txtPassword.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetPasswordRegExp();
            txtEmail.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetEmailRegExp();
            txtContactMobilePhone.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetGreekMobilePhoneRegExp();
        }

        #endregion

        #region [ Extract - Bind ]

        public override Reporter Fill(Reporter entity)
        {
            if (!string.IsNullOrEmpty(entity.ContactEmail))
            {
                MembershipUser mu = Membership.GetUser(entity.UserName);
                mu.Email = txtEmail.GetText();
                Membership.UpdateUser(mu);
            }

            entity.Email = txtEmail.GetText();
            entity.ContactMobilePhone = txtContactMobilePhone.GetText();
            entity.ContactEmail = entity.Email;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null)
                return;

            if (string.IsNullOrEmpty(Header))
            {
                Header = "Στοιχεία Λογαριασμού Χρήστη";
            }

            txtUserName.Text = Entity.UserName;
            txtEmail.Text = Entity.Email;
            txtContactMobilePhone.Text = Entity.ContactMobilePhone;
        }

        #endregion

        #region [ Helper Methods ]

        public string CreateUser()
        {
            try
            {
                MembershipCreateStatus status;
                MembershipUser mu;

                mu = Membership.CreateUser(txtUserName.Text.Trim(), txtPassword.Text, txtEmail.Text, null, null, true, out status);

                if (mu == null)
                    throw new MembershipCreateUserException(status);

                _CreatedUser = mu;
                return mu.UserName;
            }
            catch (MembershipCreateUserException)
            {
                throw;
            }
        }

        #endregion

        #region [ Properties ]

        public bool ReadOnlyEmail
        {
            get { return txtEmail.ReadOnly; }
            set
            {
                txtEmail.ReadOnly = txtEmailConfirmation.ReadOnly = value;
                txtEmail.ClientEnabled = txtEmailConfirmation.ClientEnabled = !value;
            }
        }

        public bool ReadOnly
        {
            get { return txtUserName.ReadOnly; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                {
                    control.ReadOnly = value;
                    control.ClientEnabled = !value;
                }
            }
        }

        public bool ShowHintPopup
        {
            set
            {
                if (!value)
                {
                    txtUserName.CssClass = txtUserName.CssClass.Replace("hint", "");
                    txtPassword.CssClass = txtPassword.CssClass.Replace("hint", "");
                    txtEmail.CssClass = txtEmail.CssClass.Replace("hint", "");
                    txtContactMobilePhone.CssClass = txtContactMobilePhone.CssClass.Replace("hint", "");
                }
            }
        }

        public string EmailInfo
        {
            set
            {
                ltrEmailInfo.Text = value;
            }
        }

        public string LabelWidth { get; set; }
        public string Header { get; set; }

        public bool HideMobilePhoneFields
        {
            set
            {
                if (value)
                {
                    trMobilePhoneInfo.Visible =
                        trMobilePhone.Visible =
                        trMobilePhoneConfirmation.Visible = false;
                }
            }
        }

        public bool IsMobilePhoneOptional
        {
            set
            {
                if (value)
                {
                    trMobilePhoneInfo.Visible =
                        txtContactMobilePhone.ValidationSettings.RequiredField.IsRequired =
                        txtContactMobilePhoneConfirmation.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
        }

        public bool HideConfirmationInfoFields
        {
            set
            {
                trPasswordInfo.Visible =
                trEmailInfo.Visible =
                trMobilePhoneInfo.Visible = !value;
            }
        }

        public bool HideConfirmationFields
        {
            set
            {
                trPasswordInfo.Visible =
                trEmailConfirmation.Visible =
                trMobilePhoneConfirmation.Visible = !value;
            }
        }

        MembershipUser _CreatedUser = null;
        public string ProviderUserKey
        {
            get
            {
                if (_CreatedUser == null)
                    throw new InvalidOperationException("No MembershipUser was found. Please check CreateUser() or SetUser().");
                return _CreatedUser.ProviderUserKey.ToString();
            }
        }

        #endregion

        #region [ Validation ]

        public string ValidationGroup
        {
            get
            {
                return txtEmail.ValidationSettings.ValidationGroup;
            }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                    control.ValidationSettings.ValidationGroup = value;
            }
        }

        protected void txtUserName_Validation(object sender, DevExpress.Web.ValidationEventArgs e)
        {
            e.IsValid = !new PortalServices.Services().UserNameExists(e.Value.ToString());
            e.ErrorText = "Το Όνομα Χρήστη χρησιμοποιείται";
        }

        protected void txtEmail_Validation(object sender, DevExpress.Web.ValidationEventArgs e)
        {
            e.IsValid = !new PortalServices.Services().EmailExists(e.Value.ToString());
            e.ErrorText = "Το Email χρησιμοποιείται";
        }

        #endregion
    }
}