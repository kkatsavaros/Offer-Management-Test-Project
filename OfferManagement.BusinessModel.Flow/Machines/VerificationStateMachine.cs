using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfferManagement.Utils;
using Imis.Domain;
using Stateless;

namespace OfferManagement.BusinessModel.Flow
{
    public class VerificationStateMachine : StateMachine<enVerificationStatus, enVerificationTriggers>
    {
        #region [ Helpers ]

        #region [ Triggers ]

        Dictionary<enVerificationTriggers, TriggerWithParameters<VerificationTriggerParams>> _triggers =
            new Dictionary<enVerificationTriggers, TriggerWithParameters<VerificationTriggerParams>>();

        public TriggerWithParameters<VerificationTriggerParams> TriggerFor(enVerificationTriggers trigger)
        {
            if (!_triggers.ContainsKey(trigger))
            {
                _triggers.Add(trigger, SetTriggerParameters<VerificationTriggerParams>(trigger));
            }
            return _triggers[trigger];
        }

        #endregion

        private VerificationLog GetLog(VerificationTriggerParams triggerParams, Transition transition)
        {
            return new VerificationLog()
            {
                OldVerificationStatus = transition.Source,
                NewVerificationStatus = transition.Destination,
                CreatedAt = DateTime.Now,
                CreatedBy = triggerParams.Username,
                ReporterID = Reporter.ID,
                VerificationNumber = Reporter.VerificationNumber
            };
        }

        #endregion

        public VerificationStateMachine(Reporter reporter)
            : base(reporter.VerificationStatus)
        {
            Reporter = reporter;
            ConfigureInitial();
        }

        protected Reporter Reporter { get; set; }

        #region [ Configuration Methods ]

        private void ConfigureInitial()
        {
            Configure(enVerificationStatus.NoSubmittedRequest)
                .Permit(enVerificationTriggers.Submit, enVerificationStatus.SubmittedRequest)
                .OnEntryFrom(TriggerFor(enVerificationTriggers.RevertSubmit),
                (triggerParams, transition) =>
                {
                    IUnitOfWork uow = triggerParams.UnitOfWork;
                    uow.MarkAsNew(GetLog(triggerParams, transition));

                    Reporter.VerificationStatus = transition.Destination;
                });

            Configure(enVerificationStatus.SubmittedRequest)
                .Permit(enVerificationTriggers.RevertSubmit, enVerificationStatus.NoSubmittedRequest)
                .OnEntryFrom(TriggerFor(enVerificationTriggers.Submit),
                (triggerParams, transition) =>
                {
                    IUnitOfWork uow = triggerParams.UnitOfWork;
                    uow.MarkAsNew(GetLog(triggerParams, transition));

                    Reporter.VerificationStatus = transition.Destination;
                });
        }

        #endregion

        #region [ ShortCut Methods ]

        public void Submit(VerificationTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enVerificationTriggers.Submit), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        public void RevertSubmit(VerificationTriggerParams triggerParams)
        {
            try
            {
                Fire(TriggerFor(enVerificationTriggers.RevertSubmit), triggerParams);
            }
            catch (InvalidOperationException ex)
            {
                LogHelper.LogError(ex, this);
            }
        }

        #endregion
    }
}
