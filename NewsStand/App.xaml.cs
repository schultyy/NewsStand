﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using NewsStand.Configuration;
using NewsStand.UI.Shell.ViewModels;

namespace NewsStand
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var boot = new AppBootstrapper();

            Serializer.CheckFolder();

            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowDialog(new ShellViewModel());
        }
    }
}
