using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.ComponentModel;
using OfferManagement.BusinessModel;
using System.Web.Security;
using Imis.Domain;
using System.Threading;
using Newtonsoft.Json;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace OfferManagement.Portal.PortalServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Services : System.Web.Services.WebService
    {
        #region [ Cascading DropDowns ]

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public object GetCities(string prefectureID)
        {
            var cities = CacheManager.GetOrderedCities(int.Parse(prefectureID));

            return cities.Select(city => new { id = city.ID, name = city.Name });
        }
        
        #endregion

        #region [ Store ]

        [WebMethod]
        public object CheckIBAN(string iban)
        {
            var result = ValidationHelper.ValidateIBAN(iban);
            return new { IsValid = result == enIBANValidationResult.IsValid, Message = result.GetLabel() };
        }

        [WebMethod]
        public object CheckAFM(string afm)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var result = new StoreRepository(uow).IsAfmVerified(Thread.CurrentPrincipal.Identity.Name, afm);
                return new { IsValid = !result, Message = "Οι αλλαγές δεν μπορούν να αποθηκευτούν, γιατί υπάρχει ήδη πιστοποιημένος Προμηθευτής με το Α.Φ.Μ. που δηλώσατε. Παρακαλούμε επικοινωνήστε με το Γραφείο Υποστήριξης Χρηστών για να διαπιστωθεί τι συμβαίνει." };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public object GetTags(enTagType? tagTypes)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                return new TagRepository(uow).GetAll(tagTypes)
                                                .Select(x => x.Name)
                                                .ToArray();
            }
        }

        #endregion

        #region [ Registration ]

        [WebMethod]
        public bool UserNameExists(string userName)
        {
            if (Membership.GetUser(userName) == null)
                return false;

            return true;
        }

        [WebMethod]
        public bool EmailExists(string email)
        {
            if (string.IsNullOrEmpty(Membership.GetUserNameByEmail(email)))
                return false;

            return true;
        }

        #endregion

        #region [ Helpdesk ]

        [WebMethod]
        public bool? ChangeEmail(string username, string newEmail)
        {
            var roles = Roles.GetRolesForUser(Thread.CurrentPrincipal.Identity.Name);            

            var isSecure = roles.Any(x => x == RoleNames.Store);

            if (!isSecure)
                return null;

            if (!Regex.IsMatch(newEmail, RegexHelper.GetEmailRegExp()))
                return null;

            if (string.IsNullOrEmpty(username))
            {
                username = Thread.CurrentPrincipal.Identity.Name;
            }

            var user = Membership.GetUser(username);
            bool? isEmailUnique = true;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                Reporter reporter = null;

                enReporterType reporterType = new ReporterRepository(uow).FindReporterTypeByUsername(username);

                switch (reporterType)
                {
                    case enReporterType.Store:                    
                        isEmailUnique = IsEmailUnique(user, newEmail);

                        if (!isEmailUnique.GetValueOrDefault())
                        {
                            return isEmailUnique;
                        }

                        reporter = new ReporterRepository(uow).FindByUsername(username, x => x.Store);
                        break;
                }

                isEmailUnique = IsEmailUnique(user, newEmail);

                if (!isEmailUnique.GetValueOrDefault())
                {
                    return isEmailUnique;
                }

                reporter = new ReporterRepository(uow).FindByUsername(username);

                if (reporter == null)
                    return null;

                reporter.Email = newEmail;
                reporter.ContactEmail = newEmail;

                reporter.IsEmailVerified = false;
                reporter.EmailVerificationCode = Guid.NewGuid().ToString();
                reporter.EmailVerificationDate = null;

                VerificationHelper.SendEmailVerification(reporter, uow);

                if (user != null)
                {
                    user.Email = newEmail;
                    Membership.UpdateUser(user);
                }

                return true;
            }
        }

        private bool? IsEmailUnique(MembershipUser user, string email)
        {
            if (user == null)
                return true;

            if (Membership.FindUsersByEmail(email).Count > 0)
                return false;

            return true;
        }

        #endregion
    }
}