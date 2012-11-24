using NewsStand.Model;

namespace NewsStand.Infrastructure
{
    public interface IDataLoader
    {
        UserWithFollowings LoadFollowings(string username);
        Recommendation[] GetRecommendationsForUser(string username, int pagesize = 5, int page = 0);
        Recommendation GetRecommendationById(int id);
    }
}