using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Meraki101
{
    /// <summary>
    /// Interaction logic for WebBrowser.xaml
    /// </summary>
    public partial class TheWebBrowser : Window
    {
        Label urlLbl = new Label();
        public TheWebBrowser()
        {
            InitializeComponent();

            urlLbl.Visibility = System.Windows.Visibility.Hidden;
        }

        private void webBrowser_LoadCompleted(object sender, RoutedEventArgs e)
        {
            // ... Load this site.
            this.webBrowser.Navigate(urlLbl.Content.ToString());
        }
        public string Url
        {
            set { this.urlLbl.Content = value; }
        }

        private void webBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}