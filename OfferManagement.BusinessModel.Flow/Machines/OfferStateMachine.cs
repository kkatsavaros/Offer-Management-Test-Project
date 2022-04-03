using System;
using System.Collections.Generic;
using Imis.Domain;
using OfferManagement.Utils;
using Stateless;

namespace OfferManagement.BusinessModel.Flow
{
    public class OfferStateMachine : StateMachine<enOfferStatus, enOfferTriggers>
    {
        #region [ Helpers ]

        #region [ Triggers ]

        Dictionary<enOfferTriggers, TriggerWithParameters<OfferTriggerParams>> _triggers =
            new Dictionary<enOfferTriggers, TriggerWithParameters<OfferTriggerParams>>();

        public TriggerWithParameters<OfferTriggerParams> TriggerFor(enOfferTriggers trigger)
        {
            if (!_triggers.ContainsKey(trigger))
            {
                _triggers.Add(trigger, SetTriggerParameters<OfferTriggerParams>(trigger));
            }
            return _triggers[trigger];
        }

        #endregion

        private OfferLog GetLog(OfferTriggerParams triggerParams, Transition transition)
        {
            return new OfferLog()
            {
                OldStatus = transition.Source,
                NewStatus = transition.Destination,
                CreatedAt = DateTime.Now,
                CreatedBy = triggerParams.Username,
                OfferID = Offer.ID
            };
        }

        #endregion

        public OfferStateMachine(Offer offer)
            : base(offer.OfferStatus)
        {
            Offer = offer;
            ConfigureInitial();
        }

        protected Offer Offer { get; set; }

        #region [ Configuration Methods ]

        private void ConfigureInitial()
        {
            Configure(enOfferStatus.Deleted)
                .OnEntryFrom(TriggerFor(enOfferTriggers.Delete),
                    (triggerParams, transition) =>
                    {
                        IUnitOfWork uow = triggerParams.UnitOfWork;
                        uow.MarkAsNew(GetLog(triggerParams, transition));

                        Offer.OfferStatus = transition.Destination;
                    });

            Configure(enOfferStatus.Withdrawn)
                .Permit(enOfferTriggers.RevertWithdraw, enOfferStatus.Submitted)
                .OnEntryFrom(TriggerFor(enOfferTriggers.Withdraw),
                    (triggerParams, transition) =>
                    {
                        IUnitOfWork uow = triggerParams.UnitOfWork;
                        uow.MarkAsNew(GetLog(triggerParams, transition));

                        Offer.OfferStatus = transition.Destination;
                    });

            Configure(enOfferStatus.InEdit)
                .PermitIf(enOfferTriggers.Delete, enOfferStatus.Deleted,
                    () => Offer.SubmittedAt == null)
                .PermitIf(enOfferTriggers.Submit, enOfferStatus.Submitted,
                    () =>
                    {
                        return new MinimumSpecsService().Validate(Offer);
                    })
                .OnEntryFrom(TriggerFor(enOfferTriggers.RevertSubmit),
                    (triggerParams, transition) =>
                    {
                        IUnitOfWork uow = triggerParams.UnitOfWork;
                        uow.MarkAsNew(GetLog(triggerParams, transition));

                        Offer.SubmittedAt = null;
                        Offer.SubmittedBy = null;

                        Offer.OfferStatus = transition.Destination;
                    });

            Configure(enOfferStatus.Submitted)
                .Permit(enOfferTriggers.RevertSubmit, enOfferStatus.InEdit)                
                .Permit(enOfferTriggers.Withdraw, enOfferStatus.Withdrawn)
                .OnEntryFrom(TriggerFor(enOfferTriggers.Submit),
                    (triggerParams, transition) =>
                    {
                        IUnitOfWork uow = triggerParams.UnitOfWork;
                        uow.MarkAsNew(GetLog(triggerParams, transition));

                        Offer.SubmittedAt = DateTime.Today;
                        Offer.SubmittedBy = triggerParams.Username;

                        Offer.EvaluatedAt = DateTime.Today;
                        Offer.EvaluatedBy = triggerParams.Username;

                        Offer.OfferStatus = transition.Destination;
                    })                
                .OnEntryFrom(TriggerFor(enOfferTriggers.RevertWithdraw),
                    (triggerParams, transition) =>
                    {
                        IUnitOfWork uow = triggerParams.UnitOfWork;
                        uow.MarkAsNew(GetLog(triggerParams, transition));

                        Offer.SubmittedAt = DateTime.Today;
                        Offer.OfferStatus = transition.Destination;
                    });
        }

        #endregion

        #region [ ShortCut Methods ]

        public void Delete(OfferTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enOfferTriggers.Delete), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        public void Submit(OfferTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enOfferTriggers.Submit), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        public void RevertSubmit(OfferTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enOfferTriggers.RevertSubmit), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        public void PreviousState(OfferTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enOfferTriggers.PreviousState), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        public void Withdraw(OfferTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enOfferTriggers.Withdraw), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        #endregion
    }
}
