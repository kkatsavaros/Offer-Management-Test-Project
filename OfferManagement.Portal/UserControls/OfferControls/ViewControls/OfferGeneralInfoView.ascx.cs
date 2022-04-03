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
    public partial class OfferGeneralInfoView : BaseEntityUserControl<Offer>
    {
        public override void Bind()
        {
            if (Entity == null)
                return;

            lblCode.Text = Entity.Code;
            lblTitle.Text = Entity.Title;
            lblDescription.Text = Entity.Description;
            lblIsLaptopCaseIncluded.Text = Entity.IsLaptopCaseIncluded == true ? "ΝΑΙ" : "ΟΧΙ";
            txtOfferUrl.Bind(Entity.OfferUrl);
            lblPrice.Text = string.Format("{0:C}", Entity.Price);
        }
    }
}