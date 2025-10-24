using MessagePack;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab_3.DAL
{
    public class MessagePackDataProvider<T> : IDataProvider<T>
    {
        private static readonly MessagePackSerializerOptions options =
            MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);

        public IEnumerable<T> Read(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();

            using var fs = new FileStream(filePath, FileMode.Open);
            return MessagePackSerializer.Deserialize<List<T>>(fs, options) ?? new List<T>();
        }

        public void Write(IEnumerable<T> data, string filePath)
        {
            var dataList = data.ToList();

            using var fs = new FileStream(filePath, FileMode.Create);
            MessagePackSerializer.Serialize(fs, dataList, options);
        }
    }
}

