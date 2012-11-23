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
                           Id = 534,
                           Username = "schultyy",
                           Fullname = "schultyy",
                           Bio = "Blah blah blah",
                           Followings = new ObservableCollection<User>
                                            {
                                                new User()
                                                    {
                                                        Id = 653,
                                                        Username = "jdoe",
                                                        Fullname = "John Doe"
                                                    },
                                                    new User()
                                                        {
                                                            Id = 342,
                                                            Username = "pjohnson",
                                                            Fullname = "Peter Johnson"
                                                        }
                                            }
                       };
        }

        public Recommendation[] GetRecommendationsForUser(string username, int pagesize = 5, int page = 0)
        {
            if (username == "pjohnson")
                return new[]
                           {
                               new Recommendation
                                   {
                                       Created = DateTime.Today,
                                       Url = "http://example.org/",
                                       Id = 34,
                                       Quote = "You just have to do something",
                                       UserId = 342,
                                       ArticleTitle = "Article title",
                                       WebsiteUrl = "http://example.org",
                                       WebsiteTitle = "example.org"
                                   }
                           };
            else
                return new[]
                       {
                           new Recommendation
                               {
                                   Created = DateTime.Today.AddDays(-1),
                                   Url = "http://example.org/Foo",
                                   Id = 34432,
                                   Quote = "You just have to do something - blah blah blah",
                                   UserId = 653,
                                   ArticleTitle = "Blah foo Bar",
                                   WebsiteUrl = "http://example.org",
                                       WebsiteTitle = "example.org"
                               }
                       };
        }
    }
}
