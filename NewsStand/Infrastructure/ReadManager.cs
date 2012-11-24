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

        public void MarkAsRead(int recommendationId)
        {
            var items = GetReadRecommendations() ?? new List<int>();
            if (items.Contains(recommendationId))
                return;
            items.Add(recommendationId);
            using (Stream stream = File.OpenWrite(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, items);
            }
        }

        public void MarkAsUnread(int recommendationId)
        {
            var items = GetReadRecommendations() ?? new List<int>();
            if (items.Contains(recommendationId))
            {
                items.Remove(recommendationId);
                using (Stream stream = File.OpenWrite(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, items);
                }
            }
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
    }

    public interface IReadManager
    {
        void MarkAsRead(int recommendationId);
        List<int> GetReadRecommendations();
        void MarkAsUnread(int recommendationId);
    }
}
