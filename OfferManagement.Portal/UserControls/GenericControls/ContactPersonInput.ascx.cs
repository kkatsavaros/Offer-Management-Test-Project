using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using DevExpress.Web;
using OfferManagement.Portal.Utils;
using Imis.Domain;

namespace OfferManagement.Portal.UserControls.GenericControls
{
    public partial class ContactPersonInput : BaseEntityUserControl<Reporter>
    {
        #region [ Page Inits ]

        protected void Page_Init(object sender, EventArgs e)
        {
            txtContactName.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetNameRegExp(0, 100, true);
            txtContactPhone.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetGreekFixedPhoneRegExp();
            txtContactMobilePhone.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetGreekMobilePhoneRegExp();
            txtContactEmail.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetEmailRegExp();
        }

        #endregion

        #region [ Extract - Bind ]

        public override Reporter Fill(Reporter entity)
        {
            entity.ContactName = txtContactName.GetText();
            entity.ContactPhone = txtContactPhone.GetText();
            entity.ContactMobilePhone = txtContactMobilePhone.GetText();
            entity.ContactEmail = txtContactEmail.GetText();

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null)
                return;

            if (string.IsNullOrEmpty(Title))
            {
                Title = "Στοιχεία Υπευθύνου για τη δράση";
            }

            txtContactName.Text = Entity.ContactName;
            txtContactPhone.Text = Entity.ContactPhone;
            txtContactMobilePhone.Text = Entity.ContactMobilePhone;
            txtContactEmail.Text = Entity.ContactEmail;
        }

        #endregion

        #region [ Validation ]

        public string ValidationGroup
        {
            get { return txtContactName.ValidationSettings.ValidationGroup; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                    control.ValidationSettings.ValidationGroup = value;
            }
        }

        #endregion

        #region [ Properties ]

        public string LabelWidth { get; set; }
        public string Title { get; set; }
        public ASPxEdit ContactName { get { return txtContactName; } }
        public ASPxEdit ContactPhone { get { return txtContactPhone; } }
        public ASPxEdit ContactMobilePhone { get { return txtContactMobilePhone; } }
        public ASPxEdit ContactEmail { get { return txtContactEmail; } }

        public bool ReadOnly
        {
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                {
                    control.ReadOnly = value;
                    control.ClientEnabled = !value;
                }
            }
        }

        #endregion
    }
}