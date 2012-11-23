using NewsStand.Model;

namespace NewsStand.Services
{
    public interface ITimelineService
    {
        Recommendation[] GetTimeLineForToday(string username);
    }
}