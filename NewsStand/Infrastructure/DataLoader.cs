using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NewsStand.Model;
using Newtonsoft.Json.Linq;

namespace NewsStand.Infrastructure
{
    public class DataLoader : IDataLoader
    {
        public UserWithFollowings LoadFollowings(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            var request = WebRequest.Create(
                string.Format("https://quote.fm/api/user/listFollowings?username={0}",
                              username));

            string content = null;

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
                content = reader.ReadToEnd();

            var json = JObject.Parse(content);

            return Converter.FromJson.ToUserWithFollowings(json);
        }

        public Recommendation[] GetRecommendationsForUser(string username, int pagesize = 5, int page = 0)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            var request =
                WebRequest.Create(string.Format("https://quote.fm/api/recommendation/listByUser?username={0}&scope=time&pageSize={1}&page={2}",
                                                    username, pagesize, page));

            string content = null;

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
                content = reader.ReadToEnd();

            var json = JObject.Parse(content);

            return Converter.FromJson.ToRecommendations(json);
        }

        public Recommendation GetRecommendationById(int id)
        {
            var request = WebRequest.Create(string.Format("https://quote.fm/api/recommendation/get?id={0}", id));

            string content = null;

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
                content = reader.ReadToEnd();

            var json = JObject.Parse(content);

            return Converter.FromJson.ToRecommendation(json);
        }
    }
}
