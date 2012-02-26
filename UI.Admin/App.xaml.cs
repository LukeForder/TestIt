using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using UI.Admin.Infrastructure;
using UI.Admin.Views;

namespace UI.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            App.Current.MainWindow = new Shell();
            App.Current.MainWindow.Show();
            
        }

    }
}
