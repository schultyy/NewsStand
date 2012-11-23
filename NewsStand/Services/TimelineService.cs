using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using NewsStand.Infrastructure;
using NewsStand.Model;

namespace NewsStand.Services
{
    public class TimelineService : ITimelineService
    {

        public Recommendation[] GetTimeLineForToday(string username)
        {
            var loader = ServiceLocator.Current.GetInstance<IDataLoader>();

            var followingsForCurrentUser = loader.LoadFollowings(username);

            var allRecommendations = new List<Recommendation>();

            foreach (var userWithFollowing in followingsForCurrentUser.Followings)
                allRecommendations.AddRange(loader.GetRecommendationsForUser(userWithFollowing.Username));

            return allRecommendations
                //.Where(c => c.Created.Date == DateTime.Today)
                .OrderBy(c => c.Created)
                .ToArray();
        }
    }
}
