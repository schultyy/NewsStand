using System.Collections.Generic;

namespace NewsStand.Infrastructure
{
    public interface IReadLaterManager
    {
        void MarkForLaterReading(int recommendationId);
        List<int> GetReadLaterRecommendations();
        void DeleteReadLaterEntry(int recommendationId);
    }
}