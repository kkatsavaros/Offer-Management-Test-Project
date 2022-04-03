using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class OfferManagementIdentity : GenericIdentity
    {
        public OfferManagementIdentity(GenericIdentity identity)
            : base(identity) { }

        public OfferManagementIdentity(string name)
            : base(name) { }

        public OfferManagementIdentity(string name, string type)
            : base(name, type) { }

        public int ReporterID { get; set; }
        public string ContactName { get; set; }
    }

    public class OfferManagementPrincipal : GenericPrincipal
    {
        public OfferManagementPrincipal(IIdentity identity, string[] roles)
            : base(identity, roles) { }

        public OfferManagementIdentity Identity { get; set; }
    }
}
