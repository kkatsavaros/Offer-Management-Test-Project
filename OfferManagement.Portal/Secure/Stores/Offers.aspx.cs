using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel.Flow;

namespace OfferManagement.Portal.Secure.Stores
{
    public partial class Offers : BaseEntityPortalPage<Store>
    {
        #region [ Entity Fill ]

        protected override void Fill()
        {
            Entity = new StoreRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name, x => x.Reporter, x => x.Offers);
            Entity.SaveToCurrentContext();
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Entity.IsVerified())
            {
                mvOffers.SetActiveView(vCannotEdit);
                return;
            }
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnAddNewOffer_Click(object sender, EventArgs e)
        {
            var rule = new ApplicationRuleService().GetOfferCreationRules();
            if (rule.IsActive())
            {
                var offerCounters = new StoreRepository(UnitOfWork).GetOfferCounters(Entity.ID);

                if (offerCounters != null && offerCounters.CreatedCount >=OfferManagementConstants.MAX_SUBMITTED_OFFERS)
                {
                    ClientScript.RegisterStartupScript(GetType(), "offerCountExceeded", string.Format("showAlertBox('Η προσφορά δεν μπορεί να δημιουργηθεί, γιατί έχετε φτάσει στο όριο ({0}) των προσφορών.', true);",OfferManagementConstants.MAX_SUBMITTED_OFFERS), true);
                    return;
                }

                Response.Redirect("OfferDetails.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "offerCreationNotAllowed", "showAlertBox('Η δημιουργία της προσφοράς δεν μπορεί να πραγματοποιηθεί, διότι έχει παρέλθει η προθεσμία υποβολής προσφορών για την Α Φάση.', true);", true);
            }
        }

        #endregion

        #region [ DataSource Events ]

        protected void odsOffers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<Offer> criteria = new Criteria<Offer>();

            criteria.Include(x => x.Store.Reporter);

            criteria.Sort.OrderByDescending(x => x.ID);

            criteria.Expression = criteria.Expression.Where(x => x.StoreID, Entity.ID);

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

            var offerID = int.Parse(parameters[1]);
            var offer = new OfferRepository(UnitOfWork).Load(offerID);

            var stateMachine = new OfferStateMachine(offer);

            if (action == "submit")
            {
                if (DeadlineExpired())
                {
                    gvOffers.Grid.JSProperties["cpError"] = "Η υποβολή της προσφοράς δεν μπορεί να πραγματοποιηθεί, διότι έχει παρέλθει η προθεσμία υποβολής προσφορών για την Α Φάση.";
                    return;
                }

                if (stateMachine.CanFire(enOfferTriggers.Submit))
                {
                    stateMachine.Submit(new OfferTriggerParams()
                    {
                        UnitOfWork = UnitOfWork,
                        Username = User.Identity.Name
                    });
                    UnitOfWork.Commit();
                }
                else
                {
                    gvOffers.Grid.JSProperties["cpError"] = "Η προσφορά δεν μπορεί να υποβληθεί, γιατί δεν πληροί τους κανόνες της δράσης.";
                }
            }
            else if (action == "revertsubmit")
            {
                if (DeadlineExpired())
                {
                    gvOffers.Grid.JSProperties["cpError"] = "Η επαναφορά της προσφοράς δεν μπορεί να πραγματοποιηθεί, διότι έχει παρέλθει η προθεσμία υποβολής προσφορών για την Α Φάση.";
                    return;
                }

                if (stateMachine.CanFire(enOfferTriggers.RevertSubmit))
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
            else if (action == "publish" || action == "publishaccepted")
            {
                if (offer.OfferStatus == enOfferStatus.Submitted)
                {
                    var offerCounters = new StoreRepository(UnitOfWork).GetOfferCounters(offer.StoreID);

                    if (offerCounters != null && offerCounters.PublishedCount >=OfferManagementConstants.MAX_PUBLISHED_OFFERS)
                    {
                        gvOffers.Grid.JSProperties["cpError"] = string.Format("Η δημοσίευση της προσφοράς δεν μπορεί να πραγματοποιηθεί, γιατί έχετε φτάσει στο όριο ({0}) των δημοσιευμένων προσφορών.",OfferManagementConstants.MAX_PUBLISHED_OFFERS);
                    }
                    else
                    {
                        offer.IsPublished = true;
                        UnitOfWork.Commit();
                    }
                }
                else
                {
                    gvOffers.Grid.JSProperties["cpError"] = "Η δημοσίευση της προσφοράς δεν μπορεί να πραγματοποιηθεί, γιατί δεν είναι εγκεκριμένη.";
                }
            }
            else if (action == "unpublish")
            {
                if (offer.OfferStatus == enOfferStatus.Submitted)
                {
                    offer.IsPublished = false;

                    UnitOfWork.Commit();
                }
                else
                {
                    gvOffers.Grid.JSProperties["cpError"] = "Η αποδημοσίευση της προσφοράς δεν μπορεί να πραγματοποιηθεί, γιατί δεν είναι εγκεκριμένη.";
                }
            }
            else if (action == "delete")
            {
                if (stateMachine.CanFire(enOfferTriggers.Delete))
                {
                    stateMachine.Delete(new OfferTriggerParams()
                    {
                        UnitOfWork = UnitOfWork,
                        Username = User.Identity.Name
                    });

                    UnitOfWork.Commit();
                }
            }

            gvOffers.DataBind();
        }

        #endregion

        #region [ GridView Methods ]

        protected string GetTitle(Offer offer)
        {
            if (offer == null)
                return string.Empty;

            return string.IsNullOrEmpty(offer.Title) ? string.Empty : offer.Title.Replace("\"", "\\\"").Replace("'", "\'");
        }

        protected bool HasErrors(Offer offer)
        {
            if (offer == null)
                return false;

            return !new MinimumSpecsService().Validate(offer);
        }

        protected bool CanEdit(Offer offer)
        {
            if (offer == null)
                return false;

            return offer.OfferStatus == enOfferStatus.InEdit && offer.IsActive;
        }

        protected bool CanSubmit(Offer offer)
        {
            if (offer == null)
                return false;

            return new OfferStateMachine(offer).CanFire(enOfferTriggers.Submit) && offer.IsActive;
        }

        protected bool CanRevertSubmit(Offer offer)
        {
            if (offer == null)
                return false;

            return new OfferStateMachine(offer).CanFire(enOfferTriggers.RevertSubmit) && offer.IsActive;
        }

        protected bool CanDelete(Offer offer)
        {
            if (offer == null)
                return false;

            return (new OfferStateMachine(offer).CanFire(enOfferTriggers.Delete) && offer.IsActive);
        }

        protected bool CanPublish(Offer offer)
        {
            if (offer == null)
                return false;

            return offer.IsActive
                    && offer.OfferStatus == enOfferStatus.Submitted
                    && !offer.IsPublished;
        }

        protected bool CanUnPublish(Offer offer)
        {
            if (offer == null)
                return false;

            return offer.IsActive
                    && offer.OfferStatus == enOfferStatus.Submitted
                    && offer.IsPublished;
        }

        protected bool CanPublishOrUnpublish(Offer offer)
        {
            if (offer == null)
                return false;

            return offer.IsActive && offer.OfferStatus == enOfferStatus.Submitted;
        }

        protected bool IsPublished(Offer offer)
        {
            if (offer == null)
                return false;

            return offer.IsPublished;
        }

        protected bool DeadlineExpired()
        {
            var ruleService = new ApplicationRuleService();
            ApplicationRule currentRule = ruleService.GetOfferSubmissionRules();

            return currentRule == null || !currentRule.IsActive();
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvOffers.Grid.Visible = true;
            gvOffers.Grid.DataBind();
            gvOffers.Exporter.FileName = "offers";
            gvOffers.Exporter.ExportWithDefaults();
        }

        protected void gvOffers_ExporterRenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            Offer offer = gvOffers.GetRow(e.VisibleIndex) as Offer;

            if (offer == null)
                return;

            if (e.Column.Name == "OfferStatus")
            {
                e.Text = offer.GetOfferStatus();
            }

            e.TextValue = e.Text;
        }
    }
}