using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NewsStand.Model;
using Newtonsoft.Json.Linq;

namespace NewsStand.Infrastructure
{
    public static class Converter
    {
        public static class FromJson
        {
            public static User ToUser(JToken obj)
            {
                return new User
                           {
                               Id = obj["id"].ToObject<int>(),
                               Username = obj["username"].ToObject<string>(),
                               Fullname = obj["fullname"].ToObject<string>(),
                               AvatarUrl = obj["avatar"].ToObject<string>()
                           };
            }

            public static UserWithFollowings ToUserWithFollowings(JObject json)
            {
                return new UserWithFollowings
                           {
                               Id = json["user"]["id"].ToObject<int>(),
                               Username = json["user"]["username"].ToObject<string>(),
                               Fullname = json["user"]["fullname"].ToObject<string>(),
                               AvatarUrl = json["user"]["avatar"].ToObject<string>(),
                               Followings = new ObservableCollection<User>(json["entities"].Select(ToUser))
                           };
            }

            public static Recommendation[] ToRecommendations(JObject json)
            {
                return json["entities"].Select(ToRecommendation)
                                        .ToArray();
            }

            public static Recommendation ToRecommendation(JToken json)
            {
                var uri = new Uri(json["article"]["url"].ToObject<string>());

                var websiteName = uri.GetLeftPart(UriPartial.Authority);

                return new Recommendation
                           {
                               Id = json["id"].ToObject<int>(),
                               Created = json["created"].ToObject<DateTime>(),
                               Quote = json["quote"].ToObject<string>(),
                               PlatformUrl = json["platform_url"].ToObject<string>(),
                               UserId = json["user_id"].ToObject<int>(),
                               Url = json["article"]["url"].ToObject<string>(),
                               ArticleTitle = json["article"]["title"].ToObject<string>(),
                               WebsiteTitle = websiteName,
                               WebsiteUrl = uri.Host
                           };
            }
        }
    }
}
