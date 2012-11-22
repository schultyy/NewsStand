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

        public HomeViewModel()
        {
            this.loader = ServiceLocator.Current.GetInstance<DataLoader>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.settings = Serializer.Load<Settings>();
        }
    }
}
