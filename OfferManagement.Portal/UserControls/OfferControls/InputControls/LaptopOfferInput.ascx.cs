using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using Microsoft.Data.Extensions;
using DevExpress.Web;

namespace OfferManagement.Portal.UserControls.OfferControls.InputControls
{
    public partial class LaptopOfferInput : BaseEntityUserControl<Offer>
    {
        #region [ Control Inits ]

        protected void ddlIsWifi80211acCompliant_Init(object sender, EventArgs e)
        {
            ddlIsWifi80211acCompliant.FillTrueFalse("-- παρακαλώ επιλέξτε --");
        }

        #endregion

        #region [ Extract - Bind ]

        public override Offer Fill(Offer entity)
        {
            entity.Manufacturer = ucManufacturer.TextBox.GetText();
            entity.Model = txtModel.GetText();
            entity.ScreenSize = txtScreenSize.GetDecimal().Value;
            entity.ScreenResolutionX = txtScreenResolutionX.GetInteger().Value;
            entity.ScreenResolutionY = txtScreenResolutionY.GetInteger().Value;
            entity.RamSize = txtRamSize.GetDecimal().Value;
            entity.StorageSize = txtStorageSize.GetInteger().Value;

            var selectedColors = cbxlLaptopColor.SelectedValues.OfType<int>().Select(x => (enLaptopColor)x).ToList();
            entity.Color = enLaptopColor.None;

            foreach (enLaptopColor color in selectedColors)
            {
                entity.Color = entity.Color | color;
            }

            entity.OperatingSystem = txtOperatingSystem.GetText();
            entity.GuaranteeYears = txtGuaranteeYears.GetInteger().Value;
            entity.OfficialSpecsUrl = txtOfficialSpecsUrl.GetHyperLink();
            entity.Cpu = txtCpu.GetText();
            entity.CpuSpeed = txtCpuSpeed.GetDecimal().Value;
            entity.UsbCount = txtUsbCount.GetInteger().Value;
            entity.HdmiCount = txtHdmiCount.GetInteger().Value;
            entity.IsWiFi80211acCompliant = ddlIsWifi80211acCompliant.GetSelectedBoolean().Value;

            TagManager.SaveTag(entity.Manufacturer);

            return entity;
        }

        public override void Bind()
        {
            cbxlLaptopColor.Items.Add(new ListEditItem(enLaptopColor.Black.GetLabel(), enLaptopColor.Black.GetValue()));
            cbxlLaptopColor.Items.Add(new ListEditItem(enLaptopColor.Anthracite.GetLabel(), enLaptopColor.Anthracite.GetValue()));

            if (Entity == null)
                return;

            ucManufacturer.TextBox.Text = Entity.Manufacturer;
            txtModel.Text = Entity.Model;
            txtCpu.Text = Entity.Cpu;
            txtCpuSpeed.Value = Entity.CpuSpeed;
            txtRamSize.Value = Entity.RamSize;
            txtStorageSize.Value = Entity.StorageSize;
            txtUsbCount.Value = Entity.UsbCount;
            txtHdmiCount.Value = Entity.HdmiCount;
            txtScreenSize.Value = Entity.ScreenSize;
            txtScreenResolutionX.Value = Entity.ScreenResolutionX;
            txtScreenResolutionY.Value = Entity.ScreenResolutionY;

            foreach (enLaptopColor item in Enum.GetValues(typeof(enLaptopColor)))
            {
                if (item == enLaptopColor.None)
                    continue;

                if (Entity.Color.Has(item))
                {
                    cbxlLaptopColor.Items.FindByValue(item.GetValue()).Selected = true;
                }
            }

            txtOperatingSystem.Text = Entity.OperatingSystem;
            txtGuaranteeYears.Value = Entity.GuaranteeYears;

            if (Entity.IsWiFi80211acCompliant == true)
            {
                ddlIsWifi80211acCompliant.SelectedItem = ddlIsWifi80211acCompliant.Items.FindByValue("1");
            }
            else if (Entity.IsFilled())
            {
                ddlIsWifi80211acCompliant.SelectedItem = ddlIsWifi80211acCompliant.Items.FindByValue("0");
            }

            txtOfficialSpecsUrl.Bind(Entity.OfficialSpecsUrl);
        }

        #endregion

        #region [ Validation ]

        public string ValidationGroup
        {
            get { return ucManufacturer.ValidationSettings.ValidationGroup; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                    control.ValidationSettings.ValidationGroup = value;

                txtOfficialSpecsUrl.ValidationGroup = value;
            }
        }

        #endregion

        #region [ Properties ]

        public bool ReadOnly
        {
            get { return ucManufacturer.ReadOnly; }
            set
            {
                foreach (var control in this.RecursiveOfType<ASPxEdit>())
                {
                    control.ReadOnly = value;
                    control.ClientEnabled = !value;
                }

                txtOfficialSpecsUrl.ReadOnly = value;
            }
        }

        #endregion

    }
}