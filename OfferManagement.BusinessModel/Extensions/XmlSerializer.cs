using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OfferManagement.BusinessModel
{
    public static class Serializer
    {
        internal static void SetMetadata<T>(this ITrackable entity, T metadata)
        {
            if (entity == null)
                return;

            StringBuilder sb = new StringBuilder();
            XmlSerializer xs = new XmlSerializer(typeof(T));

            xs.Serialize(XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), metadata);
            string serializedData = sb.ToString();

            if (entity.ValueXML != serializedData)
            {
                entity.ValueXML = serializedData;
            }
        }

        internal static T GetMetadata<T>(this ITrackable xmlColumn) where T : class,new()
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            T pm = null;

            if (xmlColumn != null && !string.IsNullOrWhiteSpace(xmlColumn.ValueXML))
            {
                pm = xs.Deserialize(new StringReader(xmlColumn.ValueXML)) as T;
            }

            return pm ?? (new T());
        }

    }
}
