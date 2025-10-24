using System.Collections.Generic;

namespace lab_3.DAL
{
    public interface IDataProvider<T>
    {
        void Write(IEnumerable<T> data, string filePath);
        IEnumerable<T> Read(string filePath);
    }
}
