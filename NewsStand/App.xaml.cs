using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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

            DispatcherUnhandledException += App_DispatcherUnhandledException;

            var boot = new AppBootstrapper();

            ConfigurationSerializer.CheckFolder();

            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowDialog(new ShellViewModel());
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exc = e.Exception is TargetInvocationException ? e.Exception.InnerException : e.Exception;

            MessageBox.Show(exc.Message);

            e.Handled = true;
        }
    }
}
