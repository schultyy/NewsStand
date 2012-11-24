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
            using (Stream stream = File.OpenWrite(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, readRecommendations);
            }
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
            if (File.Exists(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
            {
                using (Stream stream = File.OpenRead(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
                {
                    var formatter = new BinaryFormatter();
                    return (List<int>)formatter.Deserialize(stream);
                }
            }
            return null;
        }

        public bool IsRead(int recommendationId)
        {
            return readRecommendations.Contains(recommendationId);
        }
    }

    public interface IReadManager
    {
        void MarkAsRead(int recommendationId);
        List<int> GetReadRecommendations();
        void MarkAsUnread(int recommendationId);
    }
}
