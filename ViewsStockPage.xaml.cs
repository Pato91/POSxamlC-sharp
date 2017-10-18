using System;
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
    /// Interaction logic for ViewsStockPage.xaml
    /// </summary>
    public partial class ViewsStockPage : Page
    {
        bool optionCheck;
        Label userId;
        public ViewsStockPage(string userID)
        {
            InitializeComponent();
            userId = new Label();
            userId.Content = userID;
            userId.Visibility = System.Windows.Visibility.Hidden;
            Loaded += ViewStock_Load;

            editInventory.IsHitTestVisible = false;
            editInventory.Background = Brushes.Red;
        }

       
       
        private void ViewStock_Load(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadStockConnec = new SqlConnection(connec0))
            {
                SqlCommand getStockrCmd = loadStockConnec.CreateCommand();
                getStockrCmd.Connection = loadStockConnec;

                getStockrCmd.CommandText = "SELECT * FROM Inventory WHERE Quantity > 0";
                loadStockConnec.Open();
                SqlDataAdapter tempStockTable = new SqlDataAdapter(getStockrCmd);

                DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                tempStockTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Inventory");
                tempStockTable.Fill(tempUserHoldingSet, "Inventory");


                DataTable stockTable = tempUserHoldingSet.Tables["Inventory"];

                viewStockGrid.ItemsSource = stockTable.AsDataView();

                viewStockGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                viewStockGrid.IsReadOnly = true;


            }
        }

        private void retailInventoryChkBx_checked(object sender, RoutedEventArgs e)
        {
            optionCheck = true;
            viewStockGrid.IsReadOnly = true;
            viewStockGrid.ItemsSource = null;
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadRetailSalesConnec = new SqlConnection(connec0))
            {
                SqlCommand getRetailStockCmd = loadRetailSalesConnec.CreateCommand();
                getRetailStockCmd.Connection = loadRetailSalesConnec;

                getRetailStockCmd.CommandText = "SELECT * FROM  [RetailStock] WHERE (RemainingQuantity > 0) ORDER BY ProductName ASC";
                loadRetailSalesConnec.Open();
                SqlDataAdapter tempRetailStockTable = new SqlDataAdapter(getRetailStockCmd);

                DataSet tempRetailSalesHoldingSet = new DataSet("MerakiBusinessDB");

                tempRetailStockTable.FillSchema(tempRetailSalesHoldingSet, SchemaType.Mapped, "Inventory");
                tempRetailStockTable.Fill(tempRetailSalesHoldingSet, "Inventory");


                DataTable retailStockTable = tempRetailSalesHoldingSet.Tables["Inventory"];

                viewStockGrid.ItemsSource = retailStockTable.AsDataView();

                viewStockGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;


            }
        }


        private void retailInventoryChkBx_unchecked(object sender, RoutedEventArgs e)
        {
            optionCheck = false;
            viewStockGrid.IsReadOnly = true;
            viewStockGrid.ItemsSource = null;
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadRetailSalesConnec = new SqlConnection(connec0))
            {
                SqlCommand getStockCmd = loadRetailSalesConnec.CreateCommand();
                getStockCmd.Connection = loadRetailSalesConnec;

                getStockCmd.CommandText = "SELECT * FROM  [Inventory] WHERE (Quantity > 0) ORDER BY ProductName ASC";
                loadRetailSalesConnec.Open();
                SqlDataAdapter tempStockTable = new SqlDataAdapter(getStockCmd);

                DataSet tempRetailSalesHoldingSet = new DataSet("MerakiBusinessDB");

                tempStockTable.FillSchema(tempRetailSalesHoldingSet, SchemaType.Mapped, "Inventory");
                tempStockTable.Fill(tempRetailSalesHoldingSet, "Inventory");


                DataTable retailStockTable = tempRetailSalesHoldingSet.Tables["Inventory"];

                viewStockGrid.ItemsSource = retailStockTable.AsDataView();

                viewStockGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;


            }
        }

       

        private void backToInventoryManagerBtn1_Click(object sender, RoutedEventArgs e)
        {
            _viewStockPageFrame.NavigationService.Navigate(new InventoryManager(userId.Content.ToString()));
        }

        private void editInventoryBtn1_Click(object sender, RoutedEventArgs e)
        {
            string itemID = ((DataRowView)viewStockGrid.SelectedItem).Row[0].ToString();
            _viewStockPageFrame.NavigationService.Navigate(new EditPage(userId.Content.ToString(), itemID, optionCheck));
        }

        private void removeStockBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void viewStockGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            editInventory.IsHitTestVisible = true;
            editInventory.Background = Brushes.GreenYellow;
            viewStockGrid.IsReadOnly = true;
        }

        private void viewStockGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editInventory.IsHitTestVisible = false;
            editInventory.Background = Brushes.Red;
            viewStockGrid.IsReadOnly = true;
        }
        public void viewStockSearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            viewStockGrid.ItemsSource = null;

            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";


            using (SqlConnection loadInventoryConnec = new SqlConnection(connec0))
            {
                SqlCommand getInventoryrCmd = loadInventoryConnec.CreateCommand();
                getInventoryrCmd.Connection = loadInventoryConnec;

                getInventoryrCmd.CommandText = "SELECT * FROM Inventory WHERE (Quantity > 0)";
                loadInventoryConnec.Open();
                SqlDataAdapter tempInventoryTable = new SqlDataAdapter(getInventoryrCmd);

                DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                tempInventoryTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                tempInventoryTable.Fill(tempInventoryHoldingSet, "Inventory");


                DataTable inventoryTable = tempInventoryHoldingSet.Tables["Inventory"];


                DataTable tempInventoryTable1 = inventoryTable.Copy();
                tempInventoryTable1.Clear();
                if (viewStockSearchControl.SearchText != "") // Note: txt_Search is the TextBox..
                {
                    foreach (DataRow dr in inventoryTable.Rows)
                    {
                        if (dr[1].ToString().StartsWith(viewStockSearchControl.SearchText + "") || dr[2].ToString().Contains(viewStockSearchControl.SearchText + ""))
                        {
                            tempInventoryTable1.ImportRow(dr);
                        }
                    }
                    viewStockGrid.ItemsSource = tempInventoryTable1.AsDataView();
                    viewStockGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    viewStockGrid.SelectionMode = DataGridSelectionMode.Single;

                    viewStockGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;

                    viewStockGrid.IsReadOnly = true;
                }
                else if (Keyboard.IsKeyUp(Key.Back))
                {
                     foreach (DataRow dr in inventoryTable.Rows)
                    {
                        if (dr[1].ToString().StartsWith(viewStockSearchControl.SearchText + ""))
                        {
                            tempInventoryTable1.ImportRow(dr);
                        }
                    }
                    viewStockGrid.ItemsSource = tempInventoryTable1.AsDataView();
                    viewStockGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    viewStockGrid.SelectionMode = DataGridSelectionMode.Single;

                    viewStockGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;

                    viewStockGrid.IsReadOnly = true;
                }
                 else
                    {
                        viewStockGrid.ItemsSource = inventoryTable.AsDataView();
                        viewStockGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        viewStockGrid.SelectionMode = DataGridSelectionMode.Single;

                         viewStockGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;

                        viewStockGrid.IsReadOnly = true;
                    }
           
                    }
            
                   
                }

        }
    }

