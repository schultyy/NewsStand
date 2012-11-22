using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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

        private UserWithFollowings user;

        public UserWithFollowings User
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

        private BindableCollection<RecommendationViewModel> recommendations;

        public BindableCollection<RecommendationViewModel> Recommendations
        {
            get { return recommendations; }
            set
            {
                if (recommendations == value)
                    return;
                recommendations = value;
                NotifyOfPropertyChange(() => Recommendations);
            }
        }

        public HomeViewModel()
        {
            this.loader = ServiceLocator.Current.GetInstance<DataLoader>();
            Recommendations = new BindableCollection<RecommendationViewModel>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.settings = Serializer.Load<Settings>();

            this.Username = settings.Username;

            User = loader.LoadFollowings(settings.Username);

            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += (o, e) =>
                                           {
                                               foreach (var item in User.Followings.Select(following => loader.GetRecommendationsForUser(following.Username)).SelectMany(recommendations1 => recommendations1))
                                               {
                                                   var item1 = item;
                                                   Dispatcher.CurrentDispatcher.Invoke(
                                                       new System.Action(() => AddModel(item1)),
                                                       DispatcherPriority.DataBind);
                                               }
                                           };
            backgroundWorker.RunWorkerAsync();
        }

        private void AddModel(Recommendation recommendation)
        {
            this.Recommendations.Add(new RecommendationViewModel
                                         {
                                             Created = recommendation.Created,
                                             Quote = recommendation.Quote,
                                             Url = recommendation.Url,
                                             User = User.Followings.Single(c => c.Id == recommendation.UserId)
                                         });
        }
    }

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
