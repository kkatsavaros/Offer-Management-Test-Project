using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public partial class OfferLog
    {
        public enOfferStatus NewStatus
        {
            get { return (enOfferStatus)NewStatusInt; }
            set
            {
                if (NewStatusInt != (int)value)
                    NewStatusInt = (int)value;
            }
        }

        public enOfferStatus OldStatus
        {
            get { return (enOfferStatus)OldStatusInt; }
            set
            {
                if (OldStatusInt != (int)value)
                    OldStatusInt = (int)value;
            }
        }

        public OfferChangeValues GetOldValues()
        {
            return new Serializer<OfferChangeValues>().Deserialize(OldValuesXml);
        }

        public void SetOldValues(OfferChangeValues values)
        {
            OldValuesXml = new Serializer<OfferChangeValues>().Serialize(values);
        }

        public OfferChangeValues GetNewValues()
        {
            return new Serializer<OfferChangeValues>().Deserialize(NewValuesXml);
        }

        public void SetNewValues(OfferChangeValues values)
        {
            NewValuesXml = new Serializer<OfferChangeValues>().Serialize(values);
        }
    }
}
