using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class ApplicationRuleService
    {
        private static readonly Dictionary<string, ApplicationRule> _rules = null;

        private static ApplicationRule GetApplicationRule(string key)
        {
            if (!_rules.ContainsKey(key))
                throw new Exception(string.Format("Application rule with key '{0}' not found.", key));

            return _rules[key];
        }

        static ApplicationRuleService()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var rules = new ApplicationRuleRepository(uow).LoadAll().ToList();
                _rules = rules.ToDictionary(x => x.Name);
            }
        }

        public ApplicationRule GetStudentRegistrationRules()
        {
            return GetApplicationRule("StudentRegistration");
        }

        public ApplicationRule GetStoreRegistrationRules()
        {
            return GetApplicationRule("StoreRegistration");
        }

        public ApplicationRule GetOfferCreationRules()
        {
            return GetApplicationRule("OfferCreation");
        }

        public ApplicationRule GetOfferSubmissionRules()
        {
            return GetApplicationRule("OfferSubmission");
        }

        public ApplicationRule GetStoreSearchRules()
        {
            return GetApplicationRule("StoreSearch");
        }
    }
}
