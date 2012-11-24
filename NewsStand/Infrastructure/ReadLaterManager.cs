using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NewsStand.Configuration;

namespace NewsStand.Infrastructure
{
    public class ReadLaterManager : IReadLaterManager
    {
        private readonly List<int> readLater;

        const string readLaterFilename = "RecommendationsForLaterReading.bin";

        public ReadLaterManager()
        {
            readLater = Load();
        }

        ~ReadLaterManager()
        {
            Serializer.SaveBinary(Path.Combine(ConfigurationSerializer.GetRootDirectory(), readLaterFilename), readLater);
        }

        private List<int> Load()
        {
            return
                Serializer.LoadBinary<List<int>>(Path.Combine(ConfigurationSerializer.GetRootDirectory(),
                                                              readLaterFilename)) ?? new List<int>();
        }

        public void MarkForLaterReading(int recommendationId)
        {
            if (readLater.Contains(recommendationId))
                return;
            readLater.Add(recommendationId);
        }

        public List<int> GetReadLaterRecommendations()
        {
            return readLater;
        }

        public void DeleteReadLaterEntry(int recommendationId)
        {
            if (readLater.Contains(recommendationId))
                readLater.Remove(recommendationId);
        }
    }
}
