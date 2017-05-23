using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Clonable
{
    public static class Clonable 
    {
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable) throw new ArgumentException("The class must have serializable attribute.", "source");
            if (object.ReferenceEquals(source, null)) return default(T);
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
