using System;
using System.Web.UI;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace OfferManagement.Portal.UserControls.OfferControls.ViewControls
{
    public partial class OfferView : BaseEntityUserControl<Offer>
    {
        #region [ Bind ]

        public override void Bind()
        {
            if (Entity == null)
                return;

            ucOfferGeneralInfoView.Entity = Entity;
            ucOfferGeneralInfoView.Bind();

            ucLaptopOfferView.Entity = Entity;
            ucLaptopOfferView.Bind();
        }

        #endregion
    }
}