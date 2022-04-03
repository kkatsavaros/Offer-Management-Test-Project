using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.Portal.Controls;
using OfferManagement.BusinessModel;

namespace OfferManagement.Portal.UserControls.OfferControls.ViewControls
{
    public partial class LaptopOfferView : BaseEntityUserControl<Offer>
    {
        public override void Bind()
        {
            if (Entity == null)
                return;

            lblManufacturer.Text = Entity.Manufacturer;
            lblModel.Text = Entity.Model;
            lblCpu.Text = Entity.Cpu;
            lblCpuSpeed.Text = string.Format("{0} GHz", Entity.CpuSpeed.FormatDecimalNumber());
            lblRamSize.Text = string.Format("{0} GB", Entity.RamSize.FormatDecimalNumber());
            lblStorageSize.Text = string.Format("{0} GB", Entity.StorageSize);
            lblUsb.Text = Entity.UsbCount.ToString();
            lblHdmi.Text = Entity.HdmiCount.ToString();
            lblScreenSize.Text = string.Format("{0}''", Entity.ScreenSize.FormatDecimalNumber());
            lblScreenResolution.Text = string.Format("{0}x{1}", Entity.ScreenResolutionX, Entity.ScreenResolutionY);

            List<string> colors = new List<string>();

            foreach (enLaptopColor item in Enum.GetValues(typeof(enLaptopColor)))
            {
                if (item == enLaptopColor.None)
                    continue;

                if (Entity.Color.Has(item))
                {
                    colors.Add(item.GetLabel());
                }
            }

            lblColor.Text = string.Join(", ", colors);

            lblOperatingSystem.Text = Entity.OperatingSystem;
            lblGuaranteeYears.Text = string.Format("{0} έτη", Entity.GuaranteeYears);
            lblIsWiFi80211acCompliant.Text = Entity.IsWiFi80211acCompliant == true ? "ΝΑΙ" : "ΟΧΙ";
            txtOfficialSpecsUrl.Bind(Entity.OfficialSpecsUrl);
        }
    }
}