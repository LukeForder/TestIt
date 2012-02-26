using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace UI.Admin.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            
            Loaded += new RoutedEventHandler(NavigateToHome);

            Messenger.Default.Register<Messages.NavigateTo>(this, msg => NavigateTo(msg));

            Messenger.Default.Register<Messages.ChooseFileMessage>(this, msg => ShowFileDialog(msg));

            Messenger.Default.Register<DialogMessage>(this,
                msg =>
                {
                     var result = MessageBox.Show(
                        msg.Content,
                        msg.Caption,
                        msg.Button);

                    // Send callback
                    msg.ProcessCallback(result);
                }
            );
        }

        private void ShowFileDialog(Messages.ChooseFileMessage msg)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = msg.Extensions
            };

            if (dialog.ShowDialog() == true)
            {
                msg.ProcessResult(dialog.FileName);
            }
        }

        private void NavigateTo(Messages.NavigateTo msg)
        {
            StringBuilder navigationPage = new StringBuilder("/Views/");
            navigationPage.Append(msg.Page);
            navigationPage.Append(".xaml");

            MainContent.Navigate(new Uri(navigationPage.ToString(), UriKind.Relative));
        }

        void NavigateToHome(object sender, RoutedEventArgs e)
        {
            NavigateTo(new Messages.NavigateTo("HomeView"));
        }
        

    }
}
