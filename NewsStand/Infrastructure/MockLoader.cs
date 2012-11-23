using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NewsStand.Model;

namespace NewsStand.Infrastructure
{
    public class MockLoader : IDataLoader
    {
        public UserWithFollowings LoadFollowings(string username)
        {
            return new UserWithFollowings
                       {
                           Username = "pjohnson",
                           Fullname = "Peter Johnson",
                           Bio = "Blah blah blah",
                           Followings = new ObservableCollection<User>
                                            {
                                                new User()
                                                    {
                                                        Username = "jdoe",
                                                        Fullname = "John Doe"
                                                    }
                                            }
                       };
        }

        public Recommendation[] GetRecommendationsForUser(string username, int pagesize = 5, int page = 0)
        {
            return new Recommendation[]
                       {
                           new Recommendation
                               {
                                   Created = DateTime.Today,
                                   Url = "http://example.org/",
                                   Id = 34,
                                   Quote = "You just have to do something",
                                   UserId = 3452
                               },
                               new Recommendation
                               {
                                   Created = DateTime.Today.AddDays(-1),
                                   Url = "http://example.org/Foo",
                                   Id = 34432,
                                   Quote = "You just have to do something - blah blah blah",
                                   UserId = 3867
                               }
                       };
        }
    }
}
