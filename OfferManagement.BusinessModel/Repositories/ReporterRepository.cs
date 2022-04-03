using Imis.Domain.EF;
using Imis.Domain.EF.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace OfferManagement.BusinessModel
{
    public class ReporterRepository : DomainRepository<DBEntities, Reporter, int>
    {
        #region [ Base .ctors ]

        public ReporterRepository() : base() { }

        public ReporterRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion        

        public Reporter FindByID<T>(int id)
            where T : Reporter
        {
            return BaseQuery
                    .OfType<T>()
                    .FirstOrDefault(x => x.ID == id);
        }

        public enReporterType FindReporterTypeByUsername(string username)
        {
            return (enReporterType)BaseQuery
                    .Where(x => x.UserName == username)
                    .Select(x => x.ReporterTypeInt)
                    .FirstOrDefault();
        }

        public Reporter FindByUsername<T>(string username)
            where T : Reporter
        {
            return BaseQuery
                    .OfType<T>()
                    .FirstOrDefault(x => x.UserName == username);
        }

        public Reporter FindByEmail(string email)
        {
            return BaseQuery
                    .Where(x => x.Email == email)
                    .FirstOrDefault();
        }

        public Reporter FindByUsername(string username, params Expression<Func<Reporter, object>>[] includeExpressions)
        {
            var query = BaseQuery;

            if (includeExpressions.Length > 0)
            {
                foreach (var item in includeExpressions)
                    query = query.Include(item);
            }

            return query
                    .Where(x => x.UserName == username)
                    .FirstOrDefault();
        }

        public Reporter FindByEmailVerificationCode(string emailVerificationCode)
        {
            return BaseQuery
                    .Where(x => x.EmailVerificationCode == emailVerificationCode)
                    .FirstOrDefault();
        }

        public bool MobilePhoneExists(int reporterID, string mobilePhone)
        {
            return BaseQuery
                    .Where(x => x.ContactMobilePhone == mobilePhone)
                    .Where(x => x.IsMobilePhoneVerified == true)
                    .Any(x => x.ID != reporterID);
        }

        public bool MobilePhoneExists(string username, string mobilePhone)
        {
            return BaseQuery
                    .Where(x => x.ContactMobilePhone == mobilePhone)
                    .Where(x => x.IsMobilePhoneVerified == true)
                    .Any(x => x.UserName != username);
        }
    }
}