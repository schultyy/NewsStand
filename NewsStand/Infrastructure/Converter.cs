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
        public static User FromJsonToUser(JObject obj)
        {
            return new User
                       {
                           Id = Convert.ToInt32(obj["id"]),
                           Username = obj["username"].ToString(),
                           Fullname = obj["fullname"].ToString()
                       };
        }

        public static User FromJsonToUser(JToken obj)
        {
            return new User
            {
                Id = obj["id"].ToObject<int>(),
                Username = obj["username"].ToObject<string>(),
                Fullname = obj["fullname"].ToObject<string>()
            };
        }

        public static UserWithFollowings FromJsonToUserWithFollowings(JObject json)
        {
            var id = json["user"]["id"].ToObject<int>();
            return new UserWithFollowings
                       {
                           Id = json["user"]["id"].ToObject<int>(),
                           Username = json["user"]["username"].ToObject<string>(),
                           Fullname = json["user"]["fullname"].ToObject<string>(),
                           Followings = new ObservableCollection<User>(json["entities"].Select(FromJsonToUser))
                       };
        }
    }
}
