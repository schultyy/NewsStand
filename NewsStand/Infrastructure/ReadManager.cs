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

        public void SetAsRead(int recommendationId)
        {
            var items = GetReadRecommendations();
            items.Add(recommendationId);
            using (Stream stream = File.OpenWrite(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, items);
            }
        }

        public List<int> GetReadRecommendations()
        {
            using (Stream stream = File.OpenRead(Path.Combine(Serializer.GetRootDirectory(), readFilename)))
            {
                var formatter = new BinaryFormatter();
                return (List<int>)formatter.Deserialize(stream);
            }
        }
    }

    public interface IReadManager
    {
        void SetAsRead(int recommendationId);
        List<int> GetReadRecommendations();
    }
}
