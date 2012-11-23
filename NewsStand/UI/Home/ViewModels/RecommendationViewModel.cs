using System;
using Caliburn.Micro;
using NewsStand.Model;

namespace NewsStand.UI.Home.ViewModels
{
    public class RecommendationViewModel : PropertyChangedBase
    {
        private string quote;

        public string Quote
        {
            get { return quote; }
            set
            {
                if (quote == value)
                    return;
                quote = value;
                NotifyOfPropertyChange(() => Quote);
            }
        }

        private DateTime created;

        public DateTime Created
        {
            get { return created; }
            set
            {
                if (created == value)
                    return;
                created = value;
                NotifyOfPropertyChange(() => Created);
            }
        }

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                if (user == value)
                    return;
                user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set
            {
                if (url == value)
                    return;
                url = value;
                NotifyOfPropertyChange(() => Url);
            }
        }
    }
}