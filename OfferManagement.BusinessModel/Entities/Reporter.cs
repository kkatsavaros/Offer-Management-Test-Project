using System;

namespace OfferManagement.BusinessModel
{
    public partial class Reporter : IUserChangeTracking
    {
        protected override void OnPropertyChanged(string property)
        {

        }

        public enReporterType ReporterType
        {
            get { return (enReporterType)ReporterTypeInt; }
            set
            {
                if (ReporterTypeInt != (int)value)
                    ReporterTypeInt = (int)value;
            }
        }

        public enReporterDeclarationType DeclarationType
        {
            get { return (enReporterDeclarationType)DeclarationTypeInt; }
            set
            {
                if (DeclarationTypeInt != (int)value)
                    DeclarationTypeInt = (int)value;
            }
        }

        public enVerificationStatus VerificationStatus
        {
            get
            {
                if (VerificationStatusInt.HasValue)
                {
                    return (enVerificationStatus)VerificationStatusInt;
                }
                else
                {
                    return enVerificationStatus.NoSubmittedRequest;
                }
            }
            set
            {
                if (VerificationStatusInt != (int)value)
                    VerificationStatusInt = (int)value;
            }
        }

        public virtual string GetLabel()
        {
            return string.Empty;
        }

        public static Reporter CreateReporter(enReporterType reporterType)
        {
            var reporter = new Reporter()
            {
                ReporterType = reporterType,
                DeclarationType = enReporterDeclarationType.FromRegistration,
                IsApproved = true,
                IsContactInfoCompleted = true,
                IsEmailVerified = false,
                EmailVerificationCode = Guid.NewGuid().ToString(),
                IsMobilePhoneVerified = false,
                MobilePhoneVerificationCode = CodeGenerationHelper.GenerateVerificationCode(),
                VerificationStatus = enVerificationStatus.NoSubmittedRequest,
                SMSSentCount = 0
            };

            return reporter;
        }
    }
}
