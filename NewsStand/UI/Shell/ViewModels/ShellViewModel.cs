using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using NewsStand.Configuration;
using NewsStand.UI.Home.ViewModels;

namespace NewsStand.UI.Shell.ViewModels
{
    public sealed class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public override string DisplayName
        {
            get
            {
                return "Newsstand";
            }
            set
            {
                base.DisplayName = value;
            }
        }

        public ShellViewModel()
        {
            if (ConfigurationSerializer.Load<Settings>() == null)
            {
                var usernameModel = new UsernameViewModel();
                usernameModel.Closed += (o, e) =>
                                            {
                                                ConfigurationSerializer.Save(new Settings() { Username = e.Username });
                                                ActivateItem(new HomeViewModel());
                                            };
                ActivateItem(usernameModel);
            }
            else
                ActivateItem(new HomeViewModel());
        }

        public override void CanClose(Action<bool> callback)
        {
            base.CanClose(callback);
            System.Diagnostics.Debug.WriteLine("Can close");
        }
    }
}
