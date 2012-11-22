﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NewsStand.Model;
using Newtonsoft.Json.Linq;

namespace NewsStand.Infrastructure
{
    public class DataLoader
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

            return Converter.FromJsonToUserWithFollowings(json);
        }
    }
}
