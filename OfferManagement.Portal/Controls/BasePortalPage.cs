using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfferManagement.BusinessModel;
using Imis.Domain;
using Imis.Web.Controls;
using log4net;
using System.Web.Security;
using System.Web.UI;
using System.Threading;

namespace OfferManagement.Portal.Controls
{
    public class BaseEntityPortalPage<T> : BaseEntityPortalPage
    {
        protected virtual void Fill() { }

        protected override void OnPreInit(EventArgs e)
        {
            Fill();
            base.OnPreInit(e);
        }

        public T Entity { get; protected set; }
    }

    public abstract class BaseSecureEntityPortalPage : BaseEntityPortalPage
    {
        protected bool IsSecure { get; private set; }

        protected abstract bool Authenticate();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            IsSecure = Authenticate();
        }
    }

    public abstract class BaseSecureEntityPortalPage<T> : BaseEntityPortalPage<T>
    {
        protected bool IsAuthorized { get; private set; }

        protected abstract bool Authorize();

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            IsAuthorized = Authorize();
        }
    }

    public class BaseEntityPortalPage : DomainEntityPage
    {
        public new OfferManagementPrincipal User
        {
            get
            {
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    return base.User as OfferManagementPrincipal;
                else
                    return new OfferManagementPrincipal(Thread.CurrentPrincipal.Identity, new string[] { }) { Identity = new OfferManagementIdentity(Thread.CurrentPrincipal.Identity.Name, Thread.CurrentPrincipal.Identity.AuthenticationType) };
            }
        }

        public BaseEntityPortalPage()
        {

        }

        #region [ Overrides ]

        protected override Func<IUnitOfWork> GetUowFactoryMethod()
        {
            return () => { return UnitOfWorkFactory.Create(); };
        }

        protected override void OnInit(EventArgs e)
        {
            var identity = User.Identity;

            if (identity.IsAuthenticated)
            {
                var username = identity.Name.ToLower();
                var changedProviderUsers = CacheManager.ChangedStoreUsers;

                if (changedProviderUsers.Contains(username))
                {
                    Response.Redirect(string.Format("~/Secure/RefreshRoles.aspx?returnURL={0}", Request.Url));
                }
            }

            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //Notify
            if (!string.IsNullOrEmpty(_notifyMsg))
                ClientScript.RegisterStartupScript(GetType(), s_notifyKey + "_redirectNotification", string.Format("Imis.Lib.notify('{0}')", _notifyMsg), true);
        }

        #endregion

        #region [ Notify ]

        private string _notifyMsg = string.Empty;
        private const string s_notifyKey = "_imis_notifyMsg";

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (Session[s_notifyKey] != null)
            {
                _notifyMsg = (string)Session[s_notifyKey];
                Session.Remove(s_notifyKey);
            }
        }

        protected void Notify(string msg)
        {
            ClientScript.RegisterStartupScript(GetType(), s_notifyKey + "_onDemandNotification", string.Format("Imis.Lib.notify('{0}');", msg), true);
        }

        protected void NotifyOnNextRequest(string notifyMsg)
        {
            Session[s_notifyKey] = notifyMsg;
        }

        protected void RedirectAndNotify(string redirectUrl, string notifyMsg)
        {
            Session[s_notifyKey] = notifyMsg;
            Response.Redirect(redirectUrl, true);
        }

        #endregion
    }

    public class BaseMasterPage : DomainMasterPage
    {

    }
}