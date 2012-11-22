using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NewsStand.Configuration
{
    public class Serializer
    {
        private const string configFilename = "configuration.xml";

        private static string GetRootDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Newsstand");
        }

        public static void Save<T>(T instance)
            where T : class
        {
            using (var streamWriter = new StreamWriter(Path.Combine(GetRootDirectory(), configFilename)))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(streamWriter, instance);
            }
        }

        public static T Load<T>()
            where T : class
        {
            if (File.Exists(Path.Combine(GetRootDirectory(), configFilename)))
            {
                using (var streamReader = new StreamReader(Path.Combine(GetRootDirectory(), configFilename)))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(streamReader) as T;
                }
            }
            return null;
        }

        public static void CheckFolder()
        {
            if (Directory.Exists(GetRootDirectory()))
                return;
            Directory.CreateDirectory(GetRootDirectory());
        }
    }
}
