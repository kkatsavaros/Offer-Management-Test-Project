using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using OfferManagement.BusinessModel;
using Imis.Domain;
using OfferManagement.Portal.CacheManagerExtensions;

namespace OfferManagement.Portal.Secure.Stores
{
    /// <summary>
    /// Summary description for Store Certification
    /// </summary>
    public class GenerateStoreCertificationPDF : IHttpHandler
    {

        IUnitOfWork _UnitOfWork = null;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_UnitOfWork == null)
                    _UnitOfWork = UnitOfWorkFactory.Create(); ;
                return _UnitOfWork;
            }
            set
            {
                _UnitOfWork = value;
            }
        }

        protected HttpContext Context { get; set; }
        protected Store CurrentStore { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Context = context;
            context.Response.Clear();
            LoadData();
            context.Response.ContentType = "application/octet-stream";
            Context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Store-{0}-Certification.pdf", CurrentStore.Reporter.VerificationNumber));
            CreatePDF();
        }

        private void LoadData()
        {
            CurrentStore = new StoreRepository(UnitOfWork).FindByUsername(Context.User.Identity.Name, x => x.Reporter);
        }

        private void CreatePDF()
        {
            using (LocalReport lr = new LocalReport())
            {
                ConfigureReport(lr);
                //Proposal Label Has to Be LandScape
                string deviceInfo = @"<DeviceInfo>
            <OutputFormat>PDF</OutputFormat>
            <PageWidth>21cm</PageWidth>
            <PageHeight>29.7cm</PageHeight>
            <MarginTop>0.5in</MarginTop>
            <MarginLeft>0.0in</MarginLeft>
            <MarginRight>0.0in</MarginRight>
            <MarginBottom>0.5in</MarginBottom>
            </DeviceInfo>";

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                //var reportBytes = lr.Render("PDF", deviceInfo);
                Context.Response.BinaryWrite(renderedBytes);
            }
        }

        private void ConfigureReport(LocalReport localReport)
        {
            localReport.ReportPath = HttpContext.Current.Server.MapPath("~/_rdlc/StoreCertification.rdlc");
            List<ReportParameter> parameters = new List<ReportParameter>();

            var reporter = CurrentStore.Reporter;

            parameters.Add(new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", reporter.VerificationNumber, reporter.VerificationDate)));


            parameters.Add(new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, τον Προμηθευτή εκπροσωπεί ο/η <{0}> με email <{1}> και τηλέφωνο <{2}>.", reporter.ContactName, reporter.ContactEmail, reporter.ContactPhone)));


            localReport.DataSources.Add(new ReportDataSource() { Name = "Dummy", Value = new List<Address>() { new Address() } });
            localReport.SetParameters(parameters);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}