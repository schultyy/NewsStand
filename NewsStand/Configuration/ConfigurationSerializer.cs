﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NewsStand.Configuration
{
    public class ConfigurationSerializer
    {
        private const string configFilename = "configuration.xml";

        public static string GetRootDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Newsstand");
        }

        public static void Save(Settings instance)
        {
            using (var streamWriter = new StreamWriter(Path.Combine(GetRootDirectory(), configFilename)))
            {
                var serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(streamWriter, instance);
            }
        }

        public static Settings Load()
        {
            if (File.Exists(Path.Combine(GetRootDirectory(), configFilename)))
            {
                using (var streamReader = new StreamReader(Path.Combine(GetRootDirectory(), configFilename)))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    return serializer.Deserialize(streamReader) as Settings;
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
