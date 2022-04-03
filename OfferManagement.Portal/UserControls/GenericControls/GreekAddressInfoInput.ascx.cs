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
    public partial class GreekAddressInfoInput : BaseEntityUserControl<Address>
    {
        #region [ Control Inits ]

        protected void ddlPrefecture_Init(object sender, EventArgs e)
        {
            ddlPrefecture.FillPrefectures();
        }

        protected void FillCities(string prefectureID)
        {
            ddlCity.FillCities(prefectureID);
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Init(object sender, EventArgs e)
        {
            txtAddressName.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetAddressRegExp(0, 200, true);
            txtZipCode.ValidationSettings.RegularExpression.ValidationExpression = RegexHelper.GetGreekZipCodeRegExp();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ReadOnly)
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                {
                    control.ReadOnly = true;
                    control.ClientEnabled = false;
                }
            }
        }

        #endregion

        #region [ Extract - Bind ]

        public override Address Fill(Address entity)
        {
            entity.AddressName = txtAddressName.GetText();
            entity.ZipCode = txtZipCode.GetText();
            entity.PrefectureID = ddlPrefecture.GetSelectedInteger().Value;
            entity.CityID = ddlCity.GetSelectedInteger().Value;

            return entity;
        }

        public override void Bind()
        {
            if (string.IsNullOrEmpty(Title))
                Title = "Στοιχεία Διεύθυνσης";

            if (Entity == null)
                return;

            txtAddressName.Text = Entity.AddressName;
            txtZipCode.Text = Entity.ZipCode;
            ddlPrefecture.SelectedItem = ddlPrefecture.Items.FindByValue(Entity.PrefectureID);

            FillCities(Entity.PrefectureID.ToString());
            ddlCity.SelectedItem = ddlCity.Items.FindByValue(Entity.CityID);
        }

        #endregion

        #region [ Validation ]

        public string ValidationGroup
        {
            get { return txtAddressName.ValidationSettings.ValidationGroup; }
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
        public bool ReadOnly { get; set; }

        public string TextBoxWidth
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.EndsWith("%"))
                    {
                        txtAddressName.Width =
                            txtZipCode.Width =
                            ddlCity.Width =
                            ddlPrefecture.Width = Unit.Percentage(int.Parse(value.Replace("%", "")));
                    }
                    else if (value.EndsWith("px"))
                    {
                        txtAddressName.Width =
                            txtZipCode.Width =
                            ddlCity.Width =
                            ddlPrefecture.Width = Unit.Pixel(int.Parse(value.Replace("px", "")));
                    }
                }
            }
        }

        public ASPxEdit Address { get { return txtAddressName; } }
        public ASPxEdit ZipCode { get { return txtZipCode; } }
        public ASPxEdit Prefecture { get { return ddlPrefecture; } }
        public ASPxEdit City { get { return ddlCity; } }

        #endregion
    }
}