using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public partial class Tag
    {
        public enTagType TagType
        {
            get { return (enTagType)TagTypeInt; }
            set
            {
                int intValue = (int)value;
                if (TagTypeInt != intValue)
                    TagTypeInt = intValue;
            }
        }
    }
}
