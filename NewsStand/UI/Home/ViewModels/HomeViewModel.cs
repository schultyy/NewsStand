using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Caliburn.Micro;
using Microsoft.Practices.ServiceLocation;
using NewsStand.Configuration;
using NewsStand.Infrastructure;
using NewsStand.Model;
using NewsStand.Services;
using Xceed.Wpf.DataGrid.Converters;

namespace NewsStand.UI.Home.ViewModels
{
    public class HomeViewModel : Screen
    {
        private Settings settings;

        private readonly IDataLoader loader;

        private readonly IAvatarService avatarService;

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

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public HomeViewModel()
        {
            this.loader = ServiceLocator.Current.GetInstance<IDataLoader>();
            this.avatarService = ServiceLocator.Current.GetInstance<IAvatarService>();
            Recommendations = new BindableCollection<RecommendationViewModel>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            settings = Serializer.Load<Settings>();

            Username = settings.Username;

            User = loader.LoadFollowings(settings.Username);

            IsBusy = true;

            var context = TaskScheduler.FromCurrentSynchronizationContext();

            var task = Task.Factory.StartNew(() =>
                                                  {
                                                      var timeLine = ServiceLocator.Current.GetInstance
                                                          <ITimelineService>()
                                                          .GetTimeLineForToday(User.Username);

                                                      var token = Task.Factory.CancellationToken;

                                                      Task.Factory.StartNew(() =>
                                                                                {
                                                                                    foreach (
                                                                                        var recommendation in
                                                                                            timeLine.Where(
                                                                                                c =>
                                                                                                c.Created.Date ==
                                                                                                DateTime.Today))
                                                                                    {
                                                                                        var local = recommendation;

                                                                                        AddModel(local);
                                                                                    }
                                                                                }, token, TaskCreationOptions.None,
                                                                            context).ContinueWith(_ => IsBusy = false, context);



                                                      Task.Factory.StartNew(() =>
                                                                                {
                                                                                    foreach (
                                                                                        var recommendation in
                                                                                            timeLine.Where(
                                                                                                c =>
                                                                                                c.Created.Date !=
                                                                                                DateTime.Today))
                                                                                    {
                                                                                        var local = recommendation;

                                                                                        AddModel(local);
                                                                                    }
                                                                                }, token, TaskCreationOptions.None,
                                                                            context);
                                                  });

        }

        private void AddModel(Recommendation recommendation)
        {
            var user = User.Followings.Single(c => c.Id == recommendation.UserId);
            var avatar = avatarService.LoadAvatar(user.AvatarUrl);
            recommendations.Add(new RecommendationViewModel
                                         {
                                             Created = recommendation.Created,
                                             Quote = recommendation.Quote,
                                             Url = recommendation.Url,
                                             User = user,
                                             ArticleTitle = recommendation.ArticleTitle,
                                             WebsiteTitle = recommendation.WebsiteTitle,
                                             WebsiteUrl = recommendation.WebsiteUrl,
                                             Avatar = avatar
                                         });
        }
    }
}
