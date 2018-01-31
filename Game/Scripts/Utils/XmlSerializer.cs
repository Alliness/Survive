using System.IO;

namespace Game.Scripts.Utils
{
    public class XmlSerializer
    {

        public static T Deserialize<T>(FileStream stream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var container = serializer.Deserialize(stream);
            stream.Close();
            return (T) container;
        }

        public static string Serialize<T>(T Object)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, Object);
                return textWriter.ToString();
            }
        }
    }
}