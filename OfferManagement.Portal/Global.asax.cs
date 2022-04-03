using System;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Imis.Domain;
using OfferManagement.BusinessModel;
using OfferManagement.Portal.Utils;
using OfferManagement.Utils;
using OfferManagement.Utils.Worker;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Security;
using log4net;
using OfferManagement.Utils.Queue;

namespace OfferManagement.Portal
{
    public class Global : HttpApplication
    {
        public static string VersionNumber { get; private set; }

        #region [ Init Functions ]

        private void InitWorker()
        {
            try
            {
                var runTimes = new List<TaskLastRunTime>();
                using (var uow = UnitOfWorkFactory.Create())
                    runTimes = new TaskLastRunTimeRepository(uow).LoadAll().ToList();

                AsyncWorker.Instance.AsyncWorkerItemProcessed += (s, e) =>
                {
                    using (var ctx = UnitOfWorkFactory.Create())
                    {
                        var existing = new TaskLastRunTimeRepository(ctx).FindByName(e.TaskName);
                        if (existing == null)
                            ctx.MarkAsNew(new TaskLastRunTime() { Name = e.TaskName, LastRunTime = e.ProcessedAt });
                        else
                            existing.LastRunTime = e.ProcessedAt;

                        ctx.Commit();
                    }
                };

                AsyncWorker.Instance.Initialize();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ApplicationStart").Fatal("Application_Start Error - Init Worker", ex);
            }
        }

        private void InitQueue()
        {
            try
            {
                ServiceQueue.Instance.AddWorker(EmailQueueWorker.Current);
                ServiceQueue.Instance.AddWorker(SMSQueueWorker.Current);                

                ServiceQueue.Instance.Initialize();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ApplicationStart").Fatal("Application_Start Error - Init Queue", ex);
            }
        }

        private void InitCache()
        {
            try
            {
                if (Config.Caching.InitializeCacheOnStart)
                    CacheManager.Initialize();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ApplicationStart").Fatal("Application_Start Error - Init Cache", ex);
            }
        }

        #endregion

        protected void Application_Start(object sender, EventArgs e)
        {
            VersionNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            log4net.Config.XmlConfigurator.Configure();

            InitQueue();
            InitWorker();
            InitCache();            
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var sessionCookie = Response.Cookies.Get(Response.Cookies.AllKeys.SingleOrDefault(c => c.ToLower() == "asp.net_sessionid"));
            if (sessionCookie != null)
            {
                sessionCookie.HttpOnly = true;
            }

            if (Request.Url.AbsolutePath.ToLower().Contains("/api/"))
            {
                Response.Cookies.Clear();

                if (HttpContext.Current.Items.Contains("APIUnauthorized"))
                {
                    Response.StatusDescription = "Invalid access token";
                    Response.StatusCode = 401;
                }
            }
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var data = AuthenticationService.GetUserData();
                if (data.ReporterID == 0 && string.IsNullOrEmpty(data.ContactName))
                {
                    AuthenticationService.LoginReporter(Thread.CurrentPrincipal.Identity.Name);
                    data = AuthenticationService.GetUserData();
                }

                if (!AuthenticationService.IsCookieValid())
                {
                    AuthenticationService.InvalidateCookie(Thread.CurrentPrincipal.Identity.Name, false);
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }

                var newIdentity = new OfferManagementIdentity(Thread.CurrentPrincipal.Identity.Name, Thread.CurrentPrincipal.Identity.AuthenticationType)
                {
                    ReporterID = data.ReporterID,
                    ContactName = data.ContactName
                };

                var newPrincipal = new OfferManagementPrincipal(newIdentity, Roles.GetRolesForUser(Thread.CurrentPrincipal.Identity.Name)) { Identity = newIdentity };

                Thread.CurrentPrincipal = newPrincipal;
                HttpContext.Current.User = newPrincipal;
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
                LogHelper.LogError(e.ExceptionObject as Exception, "Global", string.Format("Unhandler Exception. Is Application Terminating:{0}", e.IsTerminating));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception error = Server.GetLastError();

            if (error != null)
            {
                if (error is HttpUnhandledException && HttpContext.Current.Request.Url.ToString().Contains("UploadPhoto"))
                {
                    //Do nothing...
                }
                else
                    LogHelper.LogError(error, "Global");
            }
            else if (HttpContext.Current == null || HttpContext.Current.Error == null)
            {
                LogHelper.LogError(new Exception("Unknown Error - No Context"), "Global");
            }
            else
            {
                LogHelper.LogError(new Exception("Unknown error"), "Global");
            }
        }
    }
}