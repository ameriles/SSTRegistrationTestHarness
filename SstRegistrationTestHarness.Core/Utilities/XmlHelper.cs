using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SstRegistrationTestHarness.Core.Utilities
{
    public static class XmlHelper
    {
        public static string Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            using (var output = new Utf8StringWriter())
            {
                using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(writer, obj);
                    return output.ToString();
                }
            }
        }

        public static T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
