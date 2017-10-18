using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage(string userName)
        {
            InitializeComponent();
            userNameLabel.Content = userName;
            Loaded += HomePage_Load;
        }

        private void HomePage_Load(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection newsConnec = new SqlConnection(connec0))
            {
            //    SqlCommand getNewsCmd = newsConnec.CreateCommand();
            //    getNewsCmd.Connection = newsConnec;

            //    getNewsCmd.CommandText = "SELECT * FROM Expiry_Date";
            //    newsConnec.Open();
            //    SqlDataAdapter tempTable = new SqlDataAdapter(getNewsCmd);

            //    DataSet tempHoldingSet = new DataSet("MerakiBusinessDB");

            //    tempTable.FillSchema(tempHoldingSet, SchemaType.Mapped, "Expiry_Date");
            //    tempTable.Fill(tempHoldingSet, "Expiry_Date");


            //    DataTable newsTable = tempHoldingSet.Tables["Expiry_Date"];
            //    //newsTextBlock.Inlines.Add(string.Format("    !!!PRODUCTS ABOUT TO EXPIRE!!!\n\n"));
            //    foreach (DataRow row in newsTable.Rows)
            //    {
            //        if (row != null)
            //        {
            //            TimeSpan dateDiff = Convert.ToDateTime(row.ItemArray[4]) - DateTime.Today;
            //            if (dateDiff.Days < 30 && dateDiff.Days >= 15)
            //            {
            //                newsTextBlock.Inlines.Add(string.Format("-{0}", row.ItemArray.GetValue(3)) + string.Format(" With Batch Number {0}", row.ItemArray.GetValue(1)) + string.Format(" Has {0}", dateDiff.Days) + string.Format(" Days Left To Expire\n\n"));
            //                newsTextBlock.Foreground = new SolidColorBrush(Colors.Green);
            //            }
            //            if (dateDiff.Days < 15)
            //            {
            //                newsTextBlock.Inlines.Add(string.Format("-{0}", row.ItemArray.GetValue(3)) + string.Format(" Has {0}", dateDiff.Days) + string.Format(" Days Left To Expire!!!\n\n"));
            //                newsTextBlock.Foreground = new SolidColorBrush(Colors.OrangeRed);
            //            }
            //        }

            //    }



            }


        }

        private void salesTile_Click(object sender, EventArgs e)
        {
            _homeFrame.NavigationService.Navigate(new SalesPage(userNameLabel.Content.ToString()));
        }

        private void usersTile_Click(object sender, EventArgs e)
        {
            _homeFrame.NavigationService.Navigate(new UsersPage(userNameLabel.Content.ToString()));
        }

        private void reportsTile_Click(object sender, EventArgs e)
        {
            _homeFrame.NavigationService.Navigate(new Reports(userNameLabel.Content.ToString()));
        }

        private void InventoryTile_Click(object sender, EventArgs e)
        {
            _homeFrame.NavigationService.Navigate(new InventoryManager(userNameLabel.Content.ToString()));
        }

        private void expensesTile_Click(object sender, EventArgs e)
        {
            _homeFrame.NavigationService.Navigate(new Expenses(userNameLabel.Content.ToString()));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
