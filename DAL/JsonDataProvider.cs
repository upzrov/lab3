using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace lab_3.DAL
{
    public class JsonDataProvider<T> : IDataProvider<T>
    {
        readonly JsonSerializerOptions options = new() { WriteIndented = true };

        public IEnumerable<T> Read(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void Write(IEnumerable<T> data, string filePath)
        {
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }
    }
}
