using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsStand.Model
{
    public class Recommendation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Quote { get; set; }

        public DateTime Created { get; set; }

        public string PlatformUrl { get; set; }

        public string Url { get; set; }

        public string ArticleTitle { get; set; }
    }
}
