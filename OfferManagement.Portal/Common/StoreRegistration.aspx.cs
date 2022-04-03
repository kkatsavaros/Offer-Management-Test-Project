using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Controls;
using OfferManagement.Portal.Utils;
using OfferManagement.Utils;
using DevExpress.Web;

namespace OfferManagement.Portal.Common
{
    public partial class StoreRegistration : BaseEntityPortalPage<Store>
    {
        #region [ Entity Fill ]

        protected override void Fill()
        {
            Entity = Store.CreateUser();
        }

        #endregion

        #region [ Page Inits ]

        protected void Page_Load(object sender, EventArgs e)
        {
            var ruleSrv = new ApplicationRuleService();
            ApplicationRule currentRule = ruleSrv.GetStoreRegistrationRules();

            if (currentRule == null || !currentRule.IsActive())
            {
                //mvRegistration.SetActiveView(vNotAllowed);
                mvRegistration.SetActiveView(vTerms);
            }
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnAcceptTerms_Click(object sender, EventArgs e)
        {
            mvRegistration.SetActiveView(vRegister);

            ucStoreInput.Entity = Entity;
            ucStoreInput.Bind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ASPxEdit.ValidateEditorsInContainer(Page, "vgRegistration"))
                return;

            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            try
            {
                var reporter = Entity.Reporter;

                ucRegisterUserInput.Fill(reporter);
                ucStoreInput.Fill(Entity);

                if (new StoreRepository(UnitOfWork).IsAfmVerified(Entity.ID, Entity.AFM))
                {
                    lblErrors.Text = string.Format("Η εγγραφή στην εφαρμογή δεν μπορεί να πραγματοποιηθεί, γιατί υπάρχει ήδη πιστοποιημένος Προμηθευτής με Α.Φ.Μ. {0}<br/><br/>Σε περίπτωση που έχετε πραγματοποιήσει παλαιότερα εγγραφή στην εφαρμογή και δεν θυμάστε τον κωδικό πρόσβασης, μπορείτε να ζητήσετε Υπενθύμιση Κωδικού πατώντας <a style=\"font-weight:bold; text-decoration: underline; color: Blue\" href=\"ForgotPassword.aspx\">εδώ</a><br/><br/>Σε αντίθετη περίπτωση μπορείτε να επικοινωνήσετε με το Γραφείο Υποστήριξης Χρηστών για να διαπιστωθεί τι συμβαίνει.<br/><br/>", Entity.AFM);
                    return;
                }

                MembershipUser user;
                MembershipCreateStatus status;
                user = Membership.CreateUser(ucRegisterUserInput.UserName, ucRegisterUserInput.Password, ucRegisterUserInput.Email, null, null, true, out status);

                if (user != null)
                {
                    reporter.UserName = reporter.CreatedBy = user.UserName;
                    reporter.CreatedAt = DateTime.Now;

                    try
                    {
                        UnitOfWork.MarkAsNew(Entity);
                        UnitOfWork.Commit();
                    }
                    catch (Exception)
                    {
                        Membership.DeleteUser(user.UserName);
                        throw;
                    }

                    var provider = Roles.Provider as OfferManagementRoleProvider;
                    provider.AddUsersToRoles(new[] { user.UserName }, new[] { RoleNames.Store });

                    VerificationHelper.SendVerificationInfo(Entity.Reporter, UnitOfWork);

                    AuthenticationService.LoginReporter(Entity.Reporter);

                    Response.Redirect("~/Secure/Stores/Default.aspx");
                }
            }
            catch (MembershipCreateUserException ex)
            {
                if (ex.StatusCode == MembershipCreateStatus.DuplicateUserName)
                {
                    LogHelper.LogError<StoreRegistration>(ex, this, string.Format("Το UserName ({0}) χρησιμοποιείται", ucRegisterUserInput.UserName));
                }
                else if (ex.StatusCode == MembershipCreateStatus.DuplicateEmail)
                {
                    LogHelper.LogError<StoreRegistration>(ex, this, string.Format("Το Email ({0}) χρησιμοποιείται", ucRegisterUserInput.Email));
                }
            }
        }

        #endregion
    }
}