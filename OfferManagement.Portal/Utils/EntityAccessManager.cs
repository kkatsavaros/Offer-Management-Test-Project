using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using OfferManagement.BusinessModel;
using Imis.Domain;

namespace OfferManagement.Portal.Utils
{
    public static class EntityAccessManager
    {
        public static Offer GetOffer(int offerID, int storeID, IUnitOfWork uow, params Expression<Func<Offer, object>>[] includeExpressions)
        {
            Offer offer = null;

            var oRep = new OfferRepository(uow);

            offer = oRep.FindByIDAndStoreID(offerID, storeID, includeExpressions);

            return offer;
        }
    }
}