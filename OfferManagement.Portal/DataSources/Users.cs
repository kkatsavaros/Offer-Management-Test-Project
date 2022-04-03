using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Security;
using OfferManagement.BusinessModel;
using Imis.Domain;
using Imis.Domain.EF;

namespace OfferManagement.Portal.DataSources
{
    [DataObject(true)]
    public class Users
    {
        private int _RecordCount = 0;

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<string> FindUsersInRoles(string[] roleNames)
        {
            List<string> result = new List<string>();

            foreach (string roleName in roleNames)
            {
                result.AddRange(Roles.GetUsersInRole(roleName));
            }

            return result.Distinct().OrderBy(x => x).ToList();
        }
    }
}