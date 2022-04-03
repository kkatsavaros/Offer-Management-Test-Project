using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using OfferManagement.BusinessModel;
using System.Web.Profile;
using OfferManagement.Portal.Controls;
using log4net.Repository.Hierarchy;
using System.Collections.Generic;
using Imis.Domain;
using System.Threading;
using OfferManagement.BusinessModel.Flow;
using OfferManagement.Portal.Utils;

namespace OfferManagement.Portal.Secure.Providers
{
    public partial class OfferPreview : BaseEntityPortalPage<Offer>
    {
        #region [ Entity Fill ]

        public Store CurrentStore { get; set; }

        protected override void Fill()
        {
            CurrentStore = new StoreRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
            CurrentStore.SaveToCurrentContext();

            int offerID;
            if (int.TryParse(Request.QueryString["id"], out offerID))
            {
                Entity = EntityAccessManager.GetOffer(offerID, CurrentStore.ID, UnitOfWork);
            }

            if (Entity == null)
            {
                Response.Redirect("Offers.aspx");
            }
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity.OfferStatus != enOfferStatus.InEdit)
            {
                Response.Redirect("Offers.aspx");
            }

            if (string.IsNullOrEmpty(Entity.Code))
            {
                Response.Redirect("Offers.aspx");
            }

            ucOfferView.Entity = Entity;
            ucOfferView.Bind();
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("Offers.aspx");
        }

        protected void btnSaveAndSubmit_Click(object sender, EventArgs e)
        {
            var stateMachine = new OfferStateMachine(Entity);
            if (stateMachine.CanFire(enOfferTriggers.Submit))
            {
                stateMachine.Submit(new OfferTriggerParams()
                {
                    UnitOfWork = UnitOfWork,
                    Username = User.Identity.Name
                });
                UnitOfWork.Commit();
            }

            Response.Redirect("Offers.aspx");
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("OfferDetails.aspx?id={0}", Entity.ID));
        }

        #endregion
    }
}
