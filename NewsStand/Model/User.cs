using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsStand.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Fullname { get; set; }

        public string Username { get; set; }

        public string Bio { get; set; }

        public string AvatarUrl { get; set; }

        public string Twitter { get; set; }

        public string Location { get; set; }

        public string PlatformUrl { get; set; }
    }
}
