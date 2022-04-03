using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public partial class Store
    {
        public string PublicName
        {
            get { return Name; }
        }

        public enCompanyType? CompanyType
        {
            get
            {
                if (CompanyTypeInt.HasValue)
                {
                    return (enCompanyType)CompanyTypeInt;
                }

                return null;
            }
            set
            {
                if (CompanyTypeInt != (int)value)
                    CompanyTypeInt = (int)value;
            }
        }


        #region [ Helpers ]

        //public int GetMasterStoreID()
        //{
        //    return IsMasterStore()
        //            ? ID
        //            : MasterStoreID.Value;
        //}

        //public string GetMasterStoreName()
        //{
        //    return IsMasterStore()
        //            ? Name
        //            : MasterStore.Name;
        //}

        //public string GetMasterStoreTradeName()
        //{
        //    return IsMasterStore()
        //            ? TradeName ?? Name
        //            : MasterStore.TradeName ?? MasterStore.Name;
        //}

        //public enCompanyType GetCompanyType()
        //{
        //    return IsMasterStore()
        //            ? CompanyType.Value
        //            : MasterStore.CompanyType.Value;
        //}

        //public string GetDOY()
        //{
        //    return IsMasterStore()
        //            ? DOY
        //            : MasterStore.DOY;
        //}

        //public string GetLegalPersonName()
        //{
        //    return IsMasterStore()
        //            ? LegalPersonName
        //            : MasterStore.LegalPersonName;
        //}

        //public Address GetAddress()
        //{
        //    if (StoreType != enStoreType.StoreUser)
        //    {
        //        return Reporter.Address;
        //    }
        //    else
        //    {
        //        if (IsMasterStoreUser())
        //        {
        //            return MasterStore.Reporter.Address;
        //        }
        //        else
        //        {
        //            return ParentStore.Reporter.Address;
        //        }
        //    }
        //}        

        public bool IsVerified()
        {
            return Reporter.VerificationStatus == enVerificationStatus.SubmittedRequest;
        }

        public static Store CreateUser()
        {
            var store = new Store()
            {   
                Reporter = Reporter.CreateReporter(enReporterType.Store)
            };
            store.Reporter.Address = new Address();

            return store;
        }

        #endregion
    }
}
