using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OfferManagement.BusinessModel
{
    public class Serializer<T>
    {
        private static XmlSerializer xs;

        static Serializer()
        {
            xs = new XmlSerializer(typeof(T));
        }

        public string Serialize(T value)
        {
            if (value == null)
                return string.Empty;

            var sb = new StringBuilder();
            var xmlWriterSettings = new XmlWriterSettings() { OmitXmlDeclaration = true };
            using (var xw = XmlTextWriter.Create(sb, xmlWriterSettings))
            {
                xs.Serialize(xw, value);
            }

            return sb.ToString();
        }

        public T Deserialize(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return default(T);

            return (T)xs.Deserialize(new StringReader(xml));
        }
    }
}
