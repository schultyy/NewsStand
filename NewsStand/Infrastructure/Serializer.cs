using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NewsStand.Infrastructure
{
    public static class Serializer
    {
        public static void SaveBinary(string filename, object instance)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");

            if (instance == null)
                throw new ArgumentNullException("instance");

            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, instance);
            }
        }

        public static T LoadBinary<T>(string filename)
            where T : class
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");

            if (File.Exists(filename))
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    var binaryFormatter = new BinaryFormatter();
                    return binaryFormatter.Deserialize(stream) as T;
                }
            }
            return null;
        }
    }
}
