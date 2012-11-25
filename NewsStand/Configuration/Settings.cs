using System.Xml.Serialization;

namespace NewsStand.Configuration
{
    [XmlRoot]
    public class Settings
    {
        public string Username { get; set; }

        public string Font { get; set; }
    }
}
