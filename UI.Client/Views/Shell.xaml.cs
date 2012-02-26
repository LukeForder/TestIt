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
using System.Printing;

namespace UI.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();

            Messenger.Default.Register<Messages.NavigateTo>(this, NavigateTo);
            Messenger.Default.Register<Messages.SavePage>(this, SaveDocumentDialog);
            Messenger.Default.Register<Messages.PrintPage>(this, PrintPageDialog);

            Loaded += NavigateToWelcome;
        }

        void NavigateToWelcome(object sender, RoutedEventArgs e)
        {
            NavigateTo(new Messages.NavigateTo("WelcomeView"));
        }

        private void NavigateTo(Messages.NavigateTo navigateToMessage)
        {
            StringBuilder uri = new StringBuilder("Views/");
            uri.Append(navigateToMessage.Page);
            uri.Append(".xaml");

            MainContent.Navigate(new Uri(uri.ToString(), UriKind.Relative));
        }

        private void PrintPageDialog(Messages.PrintPage printMessage)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / this.ActualWidth, capabilities.PageImageableArea.ExtentHeight /  this.ActualHeight);

                //Transform the Visual to scale
                this.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                printMessage.Element.Measure(sz);
                printMessage.Element.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                printDialog.PrintVisual(printMessage.Element, printMessage.Description);
            }
            
        }

        private void SaveDocumentDialog(Messages.SavePage saveMessage)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XPS Documents (.xps)|*.xps";
            dialog.DefaultExt = ".xps";
            dialog.FileName = "Assessment Report";

            if (dialog.ShowDialog() == true)
            {
                saveMessage.Callback(dialog.FileName);
            }
        }
    }
}
