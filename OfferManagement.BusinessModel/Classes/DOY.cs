using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace OfferManagement.BusinessModel
{
    public class DOY
    {
        private static XElement _xDoc = null;
        public static XElement DOYsXml
        {
            get
            {
                if (_xDoc == null)
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OfferManagement.BusinessModel.Resources.DOYs.xml");
                    _xDoc = XDocument.Load(XmlReader.Create(stream)).Element("DOYs");
                }
                return _xDoc;
            }
        }
    }
}
