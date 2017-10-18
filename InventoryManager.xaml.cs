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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Meraki101
{
    /// <summary>
    /// Interaction logic for InventoryManager.xaml
    /// </summary>
    public partial class InventoryManager : Page
    {
        Label userIDLbl;
        public InventoryManager(string userID)
        {
            InitializeComponent();
            userIDLbl = new Label();
            userIDLbl.Content = userID;
            userIDLbl.Visibility = System.Windows.Visibility.Hidden;
            //  _inventoryFrame.NavigationService.Navigated += NavigationService_NavigateBack;
        }

        /* void NavigationService_NavigateBack(object sender, NavigationEventArgs e)
         {
             JournalEntry entry = base.NavigationService.RemoveBackEntry();
             CustomContentState state = (CustomContentState)entry.CustomContentState;
             if (_inventoryFrame.NavigationService.CanGoBack) { _inventoryFrame.NavigationService.AddBackEntry(state); }
         }*/

        private void _backToMain_Click(object sender, RoutedEventArgs e)
        {
            _inventoryFrame.NavigationService.Navigate(new HomePage(userIDLbl.Content.ToString())); ;

        }

        private void addInventoryTile_Click(object sender, EventArgs e)
        {
            _inventoryFrame.NavigationService.Navigate(new AddInventoryPage(userIDLbl.Content.ToString()));
        }

        private void sellStockTile_Click(object sender, EventArgs e)
        {
            _inventoryFrame.NavigationService.Navigate(new SellPage(userIDLbl.Content.ToString()));
        }

        private void viewStockTile_Click(object sender, EventArgs e)
        {
            _inventoryFrame.NavigationService.Navigate(new ViewsStockPage(userIDLbl.Content.ToString()));
        }

        private void removeExpiredStock_Click(object sender, EventArgs e)
        {
            // _inventoryFrame.NavigationService.Navigate(new Uri("RemoveExpiredPage.xaml",UriKind.Relative));
        }

        private void inventoryReport_Click(object sender, EventArgs e)
        {
            //_inventoryFrame.NavigationService.Navigate(new Uri("ReportGenerator.xaml", UriKind.Relative));
        }


    }
}
