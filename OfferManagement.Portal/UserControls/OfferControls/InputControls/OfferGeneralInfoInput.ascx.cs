using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Data.Extensions;
using DevExpress.Web;
using System.Text.RegularExpressions;
using OfferManagement.Portal.Controls;
using OfferManagement.BusinessModel;

namespace OfferManagement.Portal.UserControls.OfferControls.InputControls
{
    public partial class OfferGeneralInfoInput : BaseEntityUserControl<Offer>
    {
        #region [ Control Inits ]

        protected void ddlIsLaptopCaseIncluded_Init(object sender, EventArgs e)
        {
            ddlIsLaptopCaseIncluded.FillTrueFalse("-- παρακαλώ επιλέξτε --");
        }

        #endregion

        #region [ Extract - Bind ]

        public override Offer Fill(Offer entity)
        {
            entity.Code = txtCode.GetText();
            entity.Title = txtTitle.GetText();
            entity.Description = txtDescription.GetText();
            entity.IsLaptopCaseIncluded = ddlIsLaptopCaseIncluded.GetSelectedBoolean().Value;
            entity.OfferUrl = txtOfferUrl.GetHyperLink();
            entity.Price = txtPrice.GetDecimal().Value;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null)
                return;

            txtCode.Text = Entity.Code;
            txtTitle.Text = Entity.Title;
            txtDescription.Text = Entity.Description;
            txtOfferUrl.Bind(Entity.OfferUrl);
            txtPrice.Value = Entity.Price;

            if (Entity.IsLaptopCaseIncluded == true)
            {
                ddlIsLaptopCaseIncluded.SelectedItem = ddlIsLaptopCaseIncluded.Items.FindByValue("1");
            }
            else if (Entity.IsFilled())
            {
                ddlIsLaptopCaseIncluded.SelectedItem = ddlIsLaptopCaseIncluded.Items.FindByValue("0");
            }

            if (!Page.IsPostBack)
                hfExistingCodes.Value = string.Join("|", _offerCodesToCheck);
        }

        #endregion

        #region [ Validation ]

        private List<string> _offerCodesToCheck = new List<string>();

        public void SetOfferCodesToCheck(IEnumerable<string> codes)
        {
            _offerCodesToCheck = new List<string>(codes);
        }

        public string ValidationGroup
        {
            get { return txtCode.ValidationSettings.ValidationGroup; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                    control.ValidationSettings.ValidationGroup = value;
            }
        }

        protected void txtOfferCode_Validation(object sender, ValidationEventArgs e)
        {
            var newOfferCode = txtCode.GetText();

            var match = Regex.Match(newOfferCode, "^[-_./A-Za-z0-9]{1,60}$");
            if (!match.Success)
            {
                e.IsValid = false;
                e.ErrorText = "Ο κωδικός προσφοράς μπορεί να αποτελείται μόνο από λατινικούς χαρακτήρες (μικρούς και κεφαλαίους), αριθμούς και τους ειδικούς χαρακτήρες (.-_/). Μέγιστο μήκος 60 χαρακτήρες.";
                return;
            }

            e.IsValid = true;
        }

        #endregion

        #region [ Properties ]

        public bool ReadOnly
        {
            get { return txtCode.ReadOnly; }
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