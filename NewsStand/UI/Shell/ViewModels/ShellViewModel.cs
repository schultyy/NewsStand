using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using NewsStand.Configuration;
using NewsStand.UI.Configuration.ViewModels;
using NewsStand.UI.Home.ViewModels;
using NewsStand.UI.ReadLater.ViewModels;

namespace NewsStand.UI.Shell.ViewModels
{
    public sealed class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public ShellViewModel()
        {
            if (ConfigurationSerializer.Load() == null)
            {
                DisplayName = "NewsStand";
                var usernameModel = new UsernameViewModel();
                usernameModel.Closed += (o, e) =>
                                            {
                                                ConfigurationSerializer.Save(new Settings() { Username = e.Username });
                                                usernameModel.TryClose();
                                                LoadViewModels();
                                            };
                ActivateItem(usernameModel);
            }
            else
                LoadViewModels();
        }

        private void LoadViewModels()
        {
            Items.Add(new HomeViewModel());
            Items.Add(new ReadLaterListViewModel());
            Items.Add(new ConfigurationViewModel());
            ActivateItem(Items.First());
            DisplayName = string.Format("NewsStand - {0}", ConfigurationSerializer.Load().Username);
        }
    }
}
