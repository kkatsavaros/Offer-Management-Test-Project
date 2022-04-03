using OfferManagement.BusinessModel;
using OfferManagement.BusinessModel.Flow;
using OfferManagement.Portal.Controls;
using OfferManagement.Portal.UserControls.SearchFilters;
using Stateless;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OfferManagement.Portal.Admin
{
    public partial class Offers : BaseEntityPortalPage<Store>
    {
        public Offers()
        {
            if (!Config.EnableAdminOffers)
            {
                throw new HttpException(404, "Are you sure you're in the right place?");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucSearchFiltersControl.Mode = enBaseOfferSearchFiltersMode.Helpdesk;
        }

        #region [ DataSource Events ]

        protected void odsOffers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<Offer> criteria = new Criteria<Offer>();

            criteria.Include(x => x.Store.Reporter);

            //criteria.Sort.OrderByDescending(x => x.ID);

            criteria.Sort.OrderByDescending(x => x.StorageSize);

            criteria.Expression = criteria.Expression.Where(x => x.OfferStatusInt, (int)enOfferStatus.InEdit, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);

            var exp = ucSearchFiltersControl.GetSearchFilters().GetExpression();

            if (exp != null)
                criteria.Expression = criteria.Expression.And(exp);

            e.InputParameters["criteria"] = criteria;
        }

        #endregion

        #region [ GridView Events ]

        protected void gvOffers_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            var parameters = e.Parameters.Split(':');
            var action = parameters[0].ToLower();

           

            if (action == "refresh")
            {
                gvOffers.DataBind();
                return;
            }

            if (action == "revertsubmit")
            {
                var offerID = int.Parse(parameters[1]);
                var offer = new OfferRepository(UnitOfWork).Load(offerID);
                var stateMachine = new OfferStateMachine(offer);

                if (stateMachine.CanFire(enOfferTriggers.PreviousState))
                {
                    stateMachine.RevertSubmit(new OfferTriggerParams()
                    {
                        UnitOfWork = UnitOfWork,
                        Username = User.Identity.Name
                    });

                    UnitOfWork.Commit();
                }
                else
                {
                    gvOffers.Grid.JSProperties["cpError"] = "Η επαναφορά της προσφοράς δεν μπορεί να πραγματοποιηθεί, γιατί βρίσκεται σε διαδικασία άξιολόγησης.";
                }
            }

            gvOffers.DataBind();
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvOffers.Grid.Visible = true;
            gvOffers.Grid.DataBind();
            gvOffers.Exporter.FileName = "admin-offers";
            gvOffers.Exporter.ExportWithDefaults();

        }
    }
}