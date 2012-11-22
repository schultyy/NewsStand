using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Practices.ServiceLocation;
using NewsStand.UI.Shell.ViewModels;

namespace NewsStand
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        IServiceLocator container;

        /// <summary>
        /// By default, we are configured to use MEF
        /// </summary>
        protected override void Configure()
        {
            this.container = new Container().Build();
            ServiceLocator.SetLocatorProvider(() => container);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            //container.SatisfyImportsOnce(instance);
        }
    }
}
