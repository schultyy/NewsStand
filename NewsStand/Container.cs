using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using NewsStand.Infrastructure;
using NewsStand.Services;

namespace NewsStand
{
    public class Container
    {
        public IServiceLocator Build()
        {
            var container = new UnityContainer();

            container.RegisterInstance<IWindowManager>(new WindowManager());
            container.RegisterInstance<IEventAggregator>(new EventAggregator());
            container.RegisterType<IDataLoader, DataLoader>();
            container.RegisterType<ITimelineService, TimelineService>();

            foreach (Type t in typeof(Screen).Assembly.GetTypes().Where(t => typeof(Screen).IsAssignableFrom(t)))
                container.RegisterType(t);

            return new UnityServiceLocator(container);
        }
    }
}
