using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Practices.ServiceLocation;
using NewsStand.Configuration;
using NewsStand.Infrastructure;
using NewsStand.Model;
using NewsStand.UI.Home.ViewModels;
using NewsStand.UI.Shared.ViewModels;

namespace NewsStand.UI.ReadLater.ViewModels
{
    public class ReadLaterListViewModel : Screen
    {
        private readonly IReadLaterManager readLaterManager;

        private readonly IDataLoader dataLoader;

        private readonly Settings settings;

        public override string DisplayName
        {
            get
            {
                return "RL";
            }
            set
            {
                base.DisplayName = value;
            }
        }

        private ObservableCollection<RecommendationViewModel> recommendations;

        public ObservableCollection<RecommendationViewModel> Recommendations
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

        public ReadLaterListViewModel()
        {
            readLaterManager = ServiceLocator.Current.GetInstance<IReadLaterManager>();
            dataLoader = ServiceLocator.Current.GetInstance<IDataLoader>();
            settings = ConfigurationSerializer.Load<Settings>();
            recommendations = new ObservableCollection<RecommendationViewModel>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            LoadRecommendations();
        }

        public void LoadRecommendations()
        {
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            IsBusy = true;

            Recommendations.Clear();

            Task.Factory.StartNew(() =>
                                      {
                                          var users = dataLoader.LoadFollowings(settings.Username);
                                          var token = Task.Factory.CancellationToken;
                                          foreach (
                                              var recommendation in
                                                  readLaterManager.GetReadLaterRecommendations().Select(
                                                      recommendationId =>
                                                      dataLoader.GetRecommendationById(recommendationId)))
                                          {
                                              var current = recommendation;
                                              Task.Factory.StartNew(() => Recommendations.Add(
                                                  RecommendationViewModel.FromModel(
                                                      current,
                                                      users.Followings.Single(
                                                          c => c.Id == current.UserId))), token,
                                                                    TaskCreationOptions.None, context);
                                          }
                                      }).ContinueWith(_ => IsBusy = false, context);
        }
    }
}
