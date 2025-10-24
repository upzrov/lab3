using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace lab_3.DAL
{
    public class XmlDataProvider<T> : IDataProvider<T>
    {
        public IEnumerable<T> Read(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();

            var serializer = new XmlSerializer(typeof(List<T>));
            using var fs = new FileStream(filePath, FileMode.Open);
            return (IEnumerable<T>?)serializer.Deserialize(fs) ?? [];
        }

        public void Write(IEnumerable<T> data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using var fs = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(fs, data);
        }
    }
}
