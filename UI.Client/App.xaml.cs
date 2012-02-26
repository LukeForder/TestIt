using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Logging;

namespace UI.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += AppStarted;
            this.DispatcherUnhandledException += UnhandledException;
        }

        void UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ILogger logger = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<ILogger>();
            if (logger != null)
            {
                logger.Log(Level.Error, e.Exception.Message);
            }
        }

        void AppStarted(object sender, StartupEventArgs e)
        {
            this.Startup -= AppStarted;

            MainWindow = new Shell();
            MainWindow.Show();
        }
    }
}
