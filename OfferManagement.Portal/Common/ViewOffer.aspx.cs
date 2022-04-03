using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;

namespace OfferManagement.Portal.Common
{
    public partial class ViewOffer : BaseEntityPortalPage<Offer>
    {
        #region [ Entity Fill ]

        protected override void Fill()
        {
            int offerID;
            if (int.TryParse(Request.QueryString["id"], out offerID))
            {
                Entity = new OfferRepository(UnitOfWork).Load(offerID);
            }
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Entity.Code))
            {
                mvOffer.SetActiveView(vOfferDetailsNotCompleted);
                return;
            }

            ucOfferView.Entity = Entity;
            ucOfferView.Bind();
        }

        #endregion
    }
}
