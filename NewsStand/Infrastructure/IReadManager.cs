using System.Collections.Generic;

namespace NewsStand.Infrastructure
{
    public interface IReadManager
    {
        void MarkAsRead(int recommendationId);
        List<int> GetReadRecommendations();
        void MarkAsUnread(int recommendationId);
    }
}