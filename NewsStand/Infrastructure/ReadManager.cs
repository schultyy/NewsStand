using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using NewsStand.Configuration;

namespace NewsStand.Infrastructure
{
    public class ReadManager : IReadManager
    {
        private const string readFilename = "ReadRecommendations.bin";

        private readonly List<int> readRecommendations;

        public ReadManager()
        {
            readRecommendations = GetReadRecommendations() ?? new List<int>();
        }

        ~ReadManager()
        {
            Serializer.SaveBinary(Path.Combine(ConfigurationSerializer.GetRootDirectory(), readFilename),
                                  readRecommendations);
        }

        public void MarkAsRead(int recommendationId)
        {
            if (readRecommendations.Contains(recommendationId))
                return;
            readRecommendations.Add(recommendationId);
        }

        public void MarkAsUnread(int recommendationId)
        {
            if (readRecommendations.Contains(recommendationId))
                readRecommendations.Remove(recommendationId);
        }

        public List<int> GetReadRecommendations()
        {
            return readRecommendations;
        }

        public bool IsRead(int recommendationId)
        {
            return readRecommendations.Contains(recommendationId);
        }
    }
}
