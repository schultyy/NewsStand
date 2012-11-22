using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Practices.ServiceLocation;
using NewsStand.Configuration;
using NewsStand.Infrastructure;
using NewsStand.Model;

namespace NewsStand.UI.Home.ViewModels
{
    public class HomeViewModel : Screen
    {
        private Settings settings;

        private DataLoader loader;

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value)
                    return;
                username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }

        public HomeViewModel()
        {
            this.loader = ServiceLocator.Current.GetInstance<DataLoader>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.settings = Serializer.Load<Settings>();

            this.Username = settings.Username;

            var followings = loader.LoadFollowings(settings.Username);
            var recommendations = new List<Recommendation>();
            foreach (var following in followings.Followings)
            {
                recommendations.AddRange(loader.GetRecommendationsForUser(following.Username));
            }
        }
    }
}
