using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for SellPage.xaml
    /// </summary>
    public partial class SellPage : Page
    {
        /// <summary>
        /// this also is for the packet sales
        /// </summary>
        ArrayList checkOutPdts;
        int index = 0;
        Dictionary<int, ArrayList> dictionary;
        Timer timer = new Timer();

        DataTable tbTest1 = new DataTable();

        /// <summary>
        /// for retail
        /// </summary>
        /// 
        int index1 = 0;
        ArrayList checkOutPdtsRetail;

        public SellPage(string userName)
        {
            InitializeComponent();

            if (userName.EndsWith("0"))
            {
                backToInventory.Visibility = System.Windows.Visibility.Hidden;
                backToInventory.IsHitTestVisible = false;

                userNameLabel.Content = userName.TrimEnd('0');

                /// <summary>
                /// this also is for the packet sales
                /// </summary>
                retailOutBtn.IsHitTestVisible = false;
                retailOutBtn.Background = Brushes.Red;
                Loaded += SellPage_Loaded;
                checkOutPdts = new ArrayList();


                dictionary = new Dictionary<int, ArrayList>();

                /// <summary>
                /// this also is for the packet sales
                /// </summary>
                checkOutPdtsRetail = new ArrayList();
            }
            else if (!userName.EndsWith("0"))
            {
                userNameLabel.Content = userName;
                /// <summary>
                /// this also is for the packet sales
                /// </summary>
                retailOutBtn.IsHitTestVisible = false;
                retailOutBtn.Background = Brushes.Red;
                Loaded += SellPage_Loaded;
                checkOutPdts = new ArrayList();


                dictionary = new Dictionary<int, ArrayList>();

                /// <summary>
                /// this also is for the packet sales
                /// </summary>
                checkOutPdtsRetail = new ArrayList();
            }

        }

        //search Algo
        public void itemsSearchControl_KeyDown(object sender, KeyEventArgs e)
        {
            listProductGrid.ItemsSource = null;

            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";


            using (SqlConnection loadInventoryConnec = new SqlConnection(connec0))
            {
                SqlCommand getInventoryrCmd = loadInventoryConnec.CreateCommand();
                getInventoryrCmd.Connection = loadInventoryConnec;

                getInventoryrCmd.CommandText = "SELECT * FROM Inventory WHERE (Quantity > 0) ORDER BY ProductName ASC";
                loadInventoryConnec.Open();
                SqlDataAdapter tempInventoryTable = new SqlDataAdapter(getInventoryrCmd);

                DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                tempInventoryTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                tempInventoryTable.Fill(tempInventoryHoldingSet, "Inventory");


                DataTable inventoryTable = tempInventoryHoldingSet.Tables["Inventory"];


                DataTable tempInventoryTable1 = inventoryTable.Copy();
                tempInventoryTable1.Clear();
                if (itemsSearchControl.SearchText != "") // Note: txt_Search is the TextBox..
                {
                    if (Keyboard.IsKeyUp(Key.Back))
                    {
                        foreach (DataRow dr in inventoryTable.Rows)
                        {
                            if (dr[1].ToString().StartsWith(itemsSearchControl.SearchText + ""))
                            {
                                tempInventoryTable1.ImportRow(dr);
                            }

                        }
                        listProductGrid.ItemsSource = tempInventoryTable1.AsDataView();
                        listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                        listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                        listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                        listProductGrid.Columns[7].Visibility = System.Windows.Visibility.Collapsed;

                        listProductGrid.IsReadOnly = true;
                    }
                    else if (Keyboard.IsKeyUp(Key.Back))
                    {
                        if (itemsSearchControl.SearchText != "")
                        {
                            foreach (DataRow dr in inventoryTable.Rows)
                            {
                                if (dr[1].ToString().StartsWith(itemsSearchControl.SearchText + ""))
                                {
                                    tempInventoryTable1.ImportRow(dr);
                                }
                            }
                            listProductGrid.ItemsSource = tempInventoryTable1.AsDataView();
                            listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                            listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                            listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                            listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                            listProductGrid.Columns[7].Visibility = System.Windows.Visibility.Collapsed;

                            listProductGrid.IsReadOnly = true;
                        }

                    }
                    
                }
                
                else
                {
                    listProductGrid.ItemsSource = inventoryTable.AsDataView();
                    listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                    listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                    listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                    listProductGrid.Columns[7].Visibility = System.Windows.Visibility.Collapsed;

                    listProductGrid.IsReadOnly = true;
                }


            }

            
        }

       


        public void retailSearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            listRetailProductsGrid.ItemsSource = null;

            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

          

            using (SqlConnection loadRetailStockConnec = new SqlConnection(connec0))
            {
                SqlCommand getRetailStockCmd = loadRetailStockConnec.CreateCommand();
                getRetailStockCmd.Connection = loadRetailStockConnec;

                getRetailStockCmd.CommandText = "SELECT * FROM RetailStock WHERE (RemainingQuantity > 0) ORDER BY ProductName ASC";
                loadRetailStockConnec.Open();

                SqlDataAdapter tempRetailStockTable = new SqlDataAdapter(getRetailStockCmd);

                DataSet tempRetailStockHoldingSet = new DataSet("MerakiBusinessDB");

                tempRetailStockTable.FillSchema(tempRetailStockHoldingSet, SchemaType.Mapped, "RetailStock");
                tempRetailStockTable.Fill(tempRetailStockHoldingSet, "RetailStock");

                DataTable retailStockTable = tempRetailStockHoldingSet.Tables["RetailStock"];

                DataTable tempRetailInventoryTable1 = retailStockTable.Copy();
                tempRetailInventoryTable1.Clear();
                if (retailSearchControl.SearchText != "") // Note: txt_Search is the TextBox..
                {
                    if (!Keyboard.IsKeyUp(Key.Back))
                    {
                        foreach (DataRow dr in retailStockTable.Rows)
                        {
                            if (dr[1].ToString().StartsWith(retailSearchControl.SearchText + ""))
                            {
                                tempRetailInventoryTable1.ImportRow(dr);
                            }

                        }
                        listRetailProductsGrid.ItemsSource = tempRetailInventoryTable1.AsDataView();
                        listRetailProductsGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        listRetailProductsGrid.SelectionMode = DataGridSelectionMode.Single;
                        listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;

                        //listRetailProductsGrid.Columns[9].Visibility = System.Windows.Visibility.Hidden;
                        listRetailProductsGrid.IsReadOnly = true;
                    }
                    else if (Keyboard.IsKeyUp(Key.Back))
                    {
                        if (retailSearchControl.SearchText != "")
                        {
                            foreach (DataRow dr in retailStockTable.Rows)
                            {
                                if (dr[1].ToString().StartsWith(retailSearchControl.SearchText + ""))
                                {
                                    tempRetailInventoryTable1.ImportRow(dr);
                                }
                            }
                            listRetailProductsGrid.ItemsSource = tempRetailInventoryTable1.AsDataView();
                            listRetailProductsGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                            listRetailProductsGrid.SelectionMode = DataGridSelectionMode.Single;

                            listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                            //listRetailProductsGrid.Columns[9].Visibility = System.Windows.Visibility.Hidden;
                            listRetailProductsGrid.IsReadOnly = true;
                        }
                    }
  
                }
                else
                {
                    listRetailProductsGrid.ItemsSource = retailStockTable.AsDataView();

                    listRetailProductsGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    listRetailProductsGrid.SelectionMode = DataGridSelectionMode.Single;
                    listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                    //listRetailProductsGrid.Columns[9].Visibility = System.Windows.Visibility.Hidden;
                    listRetailProductsGrid.IsReadOnly = true;
                }

            }
        }


        //reformats my string back to a double
        public static decimal parser(string input)
        {
            return decimal.Parse(Regex.Match(input, @"-?\d{1,3}(,\d{3})*(\.\d+)?").Value);
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c)&& !Char.IsWhiteSpace(c)&&!Char.IsSymbol(c)&&!Char.IsSeparator(c); });
        }

        private void cashPaymentTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void retailPaymentTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }


        private void packetTab_Loaded(object sender, RoutedEventArgs e)
        {
            checkOutPdts.Clear();
        }

        private void retailTab_Loaded(object sender, RoutedEventArgs e)
        {
            checkOutPdtsRetail.Clear();
        }
        //private void listDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    timer.Elapsed += timer_Elapsed;
        //}

        //private void timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    timer.Start();
        //    Stopwatch watchDog = new Stopwatch();
        //    watchDog.Start();
        //    if (watchDog.ElapsedMilliseconds == 6000)
        //    {
        //        timer.Stop();
        //        retailOutBtn.Focus();
        //        retailOutBtn.IsHitTestVisible = false;
        //        retailOutBtn.Background = Brushes.Red;
        //    }
        //}

        /// <summary>
        /// these methods bellow until the next big comment are for the first tab.. ie the packet sales tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelected_Selected(object sender, SelectionChangedEventArgs e)
        {
            retailOutBtn.Background = Brushes.GreenYellow;
            retailOutBtn.IsHitTestVisible = true;

        }


        private void transactionGrid_MouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {

        }

        #region listProducts_MouseDoubleClick
        private void listProducts_MouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (listProductGrid.SelectedItem != null && transactionGrid.Items.Count == 0)
            {
                changeLabel1.Content = "";
                dictionary = new Dictionary<int, ArrayList>();

                MyDialog dialog = new MyDialog();
                dialog.ShowDialog();
                if (dialog.ResponseText != "")
                {
                    transactionGrid.ItemsSource = null;
                    //magic code... its like heaven sent!!! it saves you a lot of time wasting and presents a cleaner code!!!
                    string id = ((DataRowView)listProductGrid.SelectedItem).Row[0].ToString();
                    string pdtBarcode = ((DataRowView)listProductGrid.SelectedItem).Row[1].ToString();
                    string pdtName = ((DataRowView)listProductGrid.SelectedItem).Row[2].ToString();
                    string pdtDescription = ((DataRowView)listProductGrid.SelectedItem).Row[3].ToString();
                    string pdtType = ((DataRowView)listProductGrid.SelectedItem).Row[4].ToString();
                    string pdtQuantity = dialog.ResponseText;
                    string inventoryQuantity = Convert.ToString(Convert.ToInt32(((DataRowView)listProductGrid.SelectedItem).Row[5].ToString()) - Convert.ToInt32(pdtQuantity));
                    string unitPrice = ((DataRowView)listProductGrid.SelectedItem).Row[8].ToString();
                    string totalAmount = Convert.ToString(Convert.ToDecimal(unitPrice) * Convert.ToInt32(pdtQuantity));

                    string finalTotal = Convert.ToString(Convert.ToDecimal(String.Format("{0:0.00}", Convert.ToDecimal(totalsLabel1.Content.ToString()))) + Convert.ToDecimal(totalAmount));
                    totalsLabel1.Content = String.Format("{0:0,0.00}", Convert.ToDecimal(finalTotal));
                    if (dictionary.Count > 0)
                    {
                        int z = dictionary.Keys.First();
                        dictionary.Remove(z);

                        checkOutPdts.Add(new TransDataRow(id, pdtBarcode, pdtName, pdtDescription, pdtType, inventoryQuantity, pdtQuantity, unitPrice, totalAmount));
                        dictionary.Add(index, checkOutPdts);
                        //MessageBox.Show(dictionary.Count + " "+index);

                        //for (int x = index; x < dictionary.Count; x++)
                        //{
                        transactionGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        transactionGrid.SelectionMode = DataGridSelectionMode.Single;
                        int y = dictionary.Keys.Max();
                        transactionGrid.ItemsSource = dictionary[y];
                        transactionGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                        //transactionGrid.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                        //transactionGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;
                        //}

                        index = index + 1;

                    }
                    else if (dictionary.Count == 0)
                    {
                        checkOutPdts.Add(new TransDataRow(id, pdtBarcode, pdtName, pdtDescription, pdtType, inventoryQuantity, pdtQuantity, unitPrice, totalAmount));
                        dictionary.Add(index, checkOutPdts);
                        //MessageBox.Show(dictionary.Count + " "+index);

                        //for (int x = index; x < dictionary.Count; x++)
                        //{
                        transactionGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        transactionGrid.SelectionMode = DataGridSelectionMode.Single;
                        int y = dictionary.Keys.Max();
                        transactionGrid.ItemsSource = dictionary[y];
                        transactionGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                        //transactionGrid.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                        //transactionGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;
                        //}

                        index = index + 1;
                    }


                }
                else if (dialog.ResponseText == "")
                {
                    MessageBox.Show("Please Input Quantity To Make A Valid Selection");
                }

            }
            else if (transactionGrid.Items.Count !=0)
            {
                MessageBox.Show("First Sell Existing Item");
            }
            else MessageBox.Show("Double Click The Item For Valid Selection");
           
        }

        #endregion



        private void listProductGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            listProductGrid.ItemContainerGenerator.StatusChanged += (s, x) =>
            {
                if (listProductGrid.ItemContainerGenerator.Status ==
                                   GeneratorStatus.ContainersGenerated)
                {
                    listProductGrid.IsSynchronizedWithCurrentItem = true;
                    var row = (DataGridRow)listProductGrid.ItemContainerGenerator.ContainerFromIndex(listProductGrid.SelectedIndex);
                    row.Background = Brushes.Gold;
                }
            };
        }



        private void listProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        void SellPage_Loaded(object sender, RoutedEventArgs e)
        {


            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadInventoryConnec = new SqlConnection(connec0))
            {
                SqlCommand getInventoryrCmd = loadInventoryConnec.CreateCommand();
                getInventoryrCmd.Connection = loadInventoryConnec;

                getInventoryrCmd.CommandText = "SELECT * FROM Inventory WHERE (Quantity > 0) ORDER BY ProductName ASC";
                loadInventoryConnec.Open();
                SqlDataAdapter tempInventoryTable = new SqlDataAdapter(getInventoryrCmd);

                DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                tempInventoryTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                tempInventoryTable.Fill(tempInventoryHoldingSet, "Inventory");


                DataTable inventoryTable = tempInventoryHoldingSet.Tables["Inventory"];

                listProductGrid.ItemsSource = inventoryTable.AsDataView();

                listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                listProductGrid.Columns[7].Visibility = System.Windows.Visibility.Collapsed;

                listProductGrid.IsReadOnly = true;

                using (SqlConnection loadRetailStockConnec = new SqlConnection(connec0))
                {
                    SqlCommand getRetailStockCmd = loadRetailStockConnec.CreateCommand();
                    getRetailStockCmd.Connection = loadRetailStockConnec;

                    getRetailStockCmd.CommandText = "SELECT * FROM RetailStock WHERE (RemainingQuantity > 0) ORDER BY ProductName ASC";
                    loadRetailStockConnec.Open();

                    SqlDataAdapter tempRetailStockTable = new SqlDataAdapter(getRetailStockCmd);

                    DataSet tempRetailStockHoldingSet = new DataSet("MerakiBusinessDB");

                    tempRetailStockTable.FillSchema(tempRetailStockHoldingSet, SchemaType.Mapped, "RetailStock");
                    tempRetailStockTable.Fill(tempRetailStockHoldingSet, "RetailStock");

                    DataTable retailStockTable = tempRetailStockHoldingSet.Tables["RetailStock"];

                    listRetailProductsGrid.ItemsSource = retailStockTable.AsDataView();
                    //listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                    //listRetailProductsGrid.Columns[9].Visibility = System.Windows.Visibility.Hidden;
                    listRetailProductsGrid.IsReadOnly = true;
                }

            }


        }


        #region commitTransactionButton

        private void commitTransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";
            if (totalsLabel1.Content == "0.00" && cashPaymentTextBox.Text == "")
            {
                MessageBox.Show("Please Fill In The Payment!");
            }
            else if (cashPaymentTextBox.Text == "")
            {
                MessageBox.Show("Please Fill In The Payment!");
            }
            else if (transactionGrid.ItemsSource == null)
            {
                MessageBox.Show("Please Select An Item To Sell!!");
                cashPaymentTextBox.Clear();
            }
            else
            {
                for (int x = 0; x < transactionGrid.Items.Count; x++)
                {
                    transactionGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    transactionGrid.SelectedIndex = x;

                    string id = ((TransDataRow)transactionGrid.SelectedItem).Id.ToString();
                    string pdtBarcode = ((TransDataRow)transactionGrid.SelectedItem).Barcode.ToString();
                    string pdtName = ((TransDataRow)transactionGrid.SelectedItem).ProductName.ToString();
                    string pdtDescription = ((TransDataRow)transactionGrid.SelectedItem).ProductDescription.ToString();
                    string pdtType = ((TransDataRow)transactionGrid.SelectedItem).Type.ToString();
                    string pdtQuantity = ((TransDataRow)transactionGrid.SelectedItem).Quantity.ToString();
                    string inventoryQuantity = ((TransDataRow)transactionGrid.SelectedItem).InventoryQuantity.ToString();
                    Decimal unitPrice = Convert.ToDecimal(((TransDataRow)transactionGrid.SelectedItem).UnitPrice.ToString());
                    string totalAmount = ((TransDataRow)transactionGrid.SelectedItem).TotalAmount.ToString();


                    int remainingQty234 = Convert.ToInt32(inventoryQuantity) - Convert.ToInt32(pdtQuantity);

                    if (Convert.ToDecimal(cashPaymentTextBox.Text) >= Convert.ToDecimal(totalAmount))
                    {

                        using (SqlConnection commitSaleConnec = new SqlConnection(connec0))
                        {
                            commitSaleConnec.Open();
                            SqlCommand commitSaleCommand = commitSaleConnec.CreateCommand();
                            SqlTransaction commitTransaction = commitSaleConnec.BeginTransaction();

                            commitSaleCommand.Connection = commitSaleConnec;
                            commitSaleCommand.Transaction = commitTransaction;
                            try
                            {


                                commitSaleCommand.CommandText = "INSERT INTO [Sales] (Barcode, ProductName, ProductDescription, Type, Quantity, Amount, SoldBy) VALUES (@Barcode, @ProductName, @ProductDescription, @Type, @Quantity, @Amount, @SoldBy)";
                                commitSaleCommand.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = pdtBarcode;
                                commitSaleCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = pdtName;
                                commitSaleCommand.Parameters.Add("@ProductDescription", SqlDbType.NVarChar).Value = pdtDescription;
                                commitSaleCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pdtType;
                                commitSaleCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = Convert.ToInt32(pdtQuantity);
                                commitSaleCommand.Parameters.Add("@Amount", SqlDbType.Int).Value = Convert.ToDecimal(totalAmount);
                                commitSaleCommand.Parameters.Add("@SoldBy", SqlDbType.NVarChar).Value = userNameLabel.Content.ToString();

                                commitSaleCommand.ExecuteNonQuery();

                                commitTransaction.Commit();


                                using (SqlConnection updateInventoryConnec = new SqlConnection(connec0))
                                {
                                    updateInventoryConnec.Open();
                                    SqlCommand updateInventoryCommand = updateInventoryConnec.CreateCommand();
                                    SqlTransaction updateInventoryTransaction = updateInventoryConnec.BeginTransaction();

                                    updateInventoryCommand.Connection = updateInventoryConnec;
                                    updateInventoryCommand.Transaction = updateInventoryTransaction;
                                    try
                                    {


                                        updateInventoryCommand.CommandText = "UPDATE [Inventory] SET Quantity = @RemainingQunatity, TotalValue = @TotalValue WHERE ProductId = @id";
                                        updateInventoryCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(id);
                                        updateInventoryCommand.Parameters.Add("@RemainingQunatity", SqlDbType.Int).Value = inventoryQuantity;
                                        updateInventoryCommand.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPrice * Convert.ToInt32(inventoryQuantity));

                                        updateInventoryCommand.ExecuteNonQuery();

                                        updateInventoryTransaction.Commit();

                                        ///alot of issues here
                                        string cashPaidMent = cashPaymentTextBox.Text;
                                        decimal totalTotal = (decimal)parser(totalsLabel1.Content.ToString());

                                        decimal change =  Convert.ToDecimal(cashPaidMent)-totalTotal;
                                        changeLabel1.Content = String.Format("{0:0,0.00}", change);

                                        transactionGrid.ItemsSource = null;
                                        listProductGrid.ItemsSource = null;
                                        dictionary.Clear();
                                        totalsLabel1.Content = "0.00";
                                        cashPaymentTextBox.Text = "";


                                        using (SqlConnection loadInventoryConnec = new SqlConnection(connec0))
                                        {
                                            SqlCommand getInventoryrCmd = loadInventoryConnec.CreateCommand();
                                            getInventoryrCmd.Connection = loadInventoryConnec;

                                            getInventoryrCmd.CommandText = "SELECT * FROM Inventory WHERE (Quantity > 0) ORDER BY ProductName ASC";
                                            loadInventoryConnec.Open();
                                            SqlDataAdapter tempInventoryTable = new SqlDataAdapter(getInventoryrCmd);

                                            DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                                            tempInventoryTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                                            tempInventoryTable.Fill(tempInventoryHoldingSet, "Inventory");


                                            DataTable inventoryTable = tempInventoryHoldingSet.Tables["Inventory"];

                                            listProductGrid.ItemsSource = inventoryTable.AsDataView();

                                            listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                                            listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                                            listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                                            listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                                            listProductGrid.Columns[8].Visibility = System.Windows.Visibility.Collapsed;
                                            listProductGrid.IsReadOnly = true;
                                        }

                                        checkOutPdts.Clear();
                                        MessageBox.Show("Transaction Has Been Successfully Committed!");

                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Commit Exception Type: " + ex.GetType());
                                        MessageBox.Show("  Message: " + ex.Message);

                                        // Attempt to roll back the transaction.
                                        try
                                        {
                                            updateInventoryTransaction.Rollback();
                                        }
                                        catch (Exception ex2)
                                        {
                                            // This catch block will handle any errors that may have occurred
                                            // on the server that would cause the rollback to fail, such as
                                            // a closed connection.
                                            MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                            MessageBox.Show("  Message: " + ex2.Message);
                                        }
                                    }
                                }


                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show("Commit Exception Type: " + ex.GetType());
                                MessageBox.Show("  Message: " + ex.Message);

                                // Attempt to roll back the transaction.
                                try
                                {
                                    commitTransaction.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    // This catch block will handle any errors that may have occurred
                                    // on the server that would cause the rollback to fail, such as
                                    // a closed connection.
                                    MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                    MessageBox.Show("  Message: " + ex2.Message);
                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Insufficient Funds!!!");
                    }

                }


            }
        }
        #endregion
        private void backToInventory_Click(object sender, RoutedEventArgs e)
        {
            _sellFrame.NavigationService.Navigate(new InventoryManager(userNameLabel.Content.ToString()));
        }

        #region retailOutBtn_click
        private void retailOutBtn_Click(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            string id = ((DataRowView)listProductGrid.SelectedItem).Row[0].ToString();
            string pdtBarcode = ((DataRowView)listProductGrid.SelectedItem).Row[1].ToString();
            string pdtName = ((DataRowView)listProductGrid.SelectedItem).Row[2].ToString();
            string pdtDescription = ((DataRowView)listProductGrid.SelectedItem).Row[3].ToString();
            string pdtType = ((DataRowView)listProductGrid.SelectedItem).Row[4].ToString();
            string inventoryQuantity = Convert.ToString(1);
            string pdtQuantity = ((DataRowView)listProductGrid.SelectedItem).Row[6].ToString();
            string unitPrice = ((DataRowView)listProductGrid.SelectedItem).Row[8].ToString();
            string totalValue = Convert.ToString(Convert.ToDecimal(unitPrice) * Convert.ToInt16(pdtQuantity));

            var itemSource = listRetailProductsGrid.ItemsSource as IEnumerable;

            if (itemSource != null)
            {

                Label itsTrue = new Label();
                itsTrue.Visibility = System.Windows.Visibility.Hidden;

                foreach (var item in itemSource)
                {
                    if (pdtBarcode != ((DataRowView)item).Row[1].ToString())
                    {
                        Label just = new Label();
                        just.Visibility = System.Windows.Visibility.Collapsed;
                        just.Content = ((DataRowView)item).Row[1].ToString();
                    }
                    else if (pdtBarcode.Equals((((DataRowView)item).Row[1].ToString())))
                    {
                        itsTrue.Content = "true";
                        break;
                    }
                }


                if (itsTrue.Content != "true")
                {
                    using (SqlConnection retailOutConnec = new SqlConnection(connec0))
                    {
                        retailOutConnec.Open();
                        SqlCommand retailOutCommand = retailOutConnec.CreateCommand();
                        SqlTransaction commitTransaction = retailOutConnec.BeginTransaction();

                        retailOutCommand.Connection = retailOutConnec;
                        retailOutCommand.Transaction = commitTransaction;
                        try
                        {


                            retailOutCommand.CommandText = "INSERT INTO [RetailStock] (Barcode, ProductName, ProductDescription, Type, PacketQuantity, RemainingQuantity, UnitPrice, TotalValue, RetailedBy) VALUES (@Barcode, @ProductName, @ProductDescription, @Type, @PacketQuantity, @RemainingQuantity, @UnitPrice, @TotalValue, @RetailedBy) ";
                            retailOutCommand.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = pdtBarcode;
                            retailOutCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = pdtName;
                            retailOutCommand.Parameters.Add("@ProductDescription", SqlDbType.NVarChar).Value = pdtDescription;
                            retailOutCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pdtType;
                            retailOutCommand.Parameters.Add("@PacketQuantity", SqlDbType.Int).Value = Convert.ToInt32(pdtQuantity);
                            retailOutCommand.Parameters.Add("@RemainingQuantity", SqlDbType.Int).Value = Convert.ToInt32(pdtQuantity);
                            retailOutCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPrice) / Convert.ToInt32(pdtQuantity);
                            retailOutCommand.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPrice);
                            retailOutCommand.Parameters.Add("@RetailedBy", SqlDbType.NVarChar).Value = userNameLabel.Content.ToString();

                            retailOutCommand.ExecuteNonQuery();

                            commitTransaction.Commit();

                            listRetailProductsGrid.ItemsSource = null;
                            using (SqlConnection updateInventoryConnec = new SqlConnection(connec0))
                            {
                                updateInventoryConnec.Open();
                                SqlCommand updateInventoryCommand = updateInventoryConnec.CreateCommand();
                                SqlTransaction updateInventoryTransaction = updateInventoryConnec.BeginTransaction();

                                updateInventoryCommand.Connection = updateInventoryConnec;
                                updateInventoryCommand.Transaction = updateInventoryTransaction;
                                try
                                {

                                    updateInventoryCommand.CommandText = "UPDATE [Inventory] SET Quantity = @RemainingQunatity, TotalValue = @TotalValue WHERE ProductId = @id";
                                    updateInventoryCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(id);
                                    updateInventoryCommand.Parameters.Add("@RemainingQunatity", SqlDbType.Int).Value = Convert.ToInt32(((DataRowView)listProductGrid.SelectedItem).Row[5].ToString()) - 1;
                                    updateInventoryCommand.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(totalValue) - Convert.ToDecimal(unitPrice);

                                    updateInventoryCommand.ExecuteNonQuery();

                                    updateInventoryTransaction.Commit();


                                    listRetailProductsGrid.ItemsSource = null;

                                    using (SqlConnection loadInventoryConnec = new SqlConnection(connec0))
                                    {
                                        SqlCommand getInventoryrCmd = loadInventoryConnec.CreateCommand();
                                        getInventoryrCmd.Connection = loadInventoryConnec;

                                        getInventoryrCmd.CommandText = "SELECT * FROM Inventory WHERE (Quantity > 0) ORDER BY ProductName ASC";
                                        loadInventoryConnec.Open();
                                        SqlDataAdapter tempInventoryTable = new SqlDataAdapter(getInventoryrCmd);

                                        DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                                        tempInventoryTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                                        tempInventoryTable.Fill(tempInventoryHoldingSet, "Inventory");


                                        DataTable inventoryTable = tempInventoryHoldingSet.Tables["Inventory"];

                                        listProductGrid.ItemsSource = inventoryTable.AsDataView();


                                        listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                                        listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                                        listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                                        listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                                        listProductGrid.Columns[7].Visibility = System.Windows.Visibility.Collapsed;
                                        listProductGrid.IsReadOnly = true;
                                    }

                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show("Commit Exception Type: " + ex.GetType());
                                    MessageBox.Show("  Message: " + ex.Message);

                                    // Attempt to roll back the transaction.
                                    try
                                    {
                                        updateInventoryTransaction.Rollback();
                                    }
                                    catch (Exception ex2)
                                    {
                                        // This catch block will handle any errors that may have occurred
                                        // on the server that would cause the rollback to fail, such as
                                        // a closed connection.
                                        MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                        MessageBox.Show("  Message: " + ex2.Message);
                                    }
                                }

                                using (SqlConnection loadRetailStockConnec = new SqlConnection(connec0))
                                {
                                    SqlCommand getRetailStockCmd = loadRetailStockConnec.CreateCommand();
                                    getRetailStockCmd.Connection = loadRetailStockConnec;

                                    getRetailStockCmd.CommandText = "SELECT * FROM RetailStock WHERE (PacketQuantity > 0) ORDER BY ProductName ASC";
                                    loadRetailStockConnec.Open();

                                    SqlDataAdapter tempRetailStockTable = new SqlDataAdapter(getRetailStockCmd);

                                    DataSet tempRetailStockHoldingSet = new DataSet("MerakiBusinessDB");

                                    tempRetailStockTable.FillSchema(tempRetailStockHoldingSet, SchemaType.Mapped, "RetailStock");
                                    tempRetailStockTable.Fill(tempRetailStockHoldingSet, "RetailStock");

                                    DataTable retailStockTable = tempRetailStockHoldingSet.Tables["RetailStock"];

                                    listRetailProductsGrid.ItemsSource = retailStockTable.AsDataView();

                                    //listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                                    //listRetailProductsGrid.Columns[9].Visibility = System.Windows.Visibility.Hidden;
                                    listRetailProductsGrid.IsReadOnly = true;
                                }

                                MessageBox.Show("Item Can Now Be Sold In Retail!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Commit Exception Type: " + ex.GetType());
                            MessageBox.Show("am here 1a");
                            MessageBox.Show("  Message: " + ex.Message);
                            MessageBox.Show("am here 2a");
                            // Attempt to roll back the transaction.
                            try
                            {
                                commitTransaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                // This catch block will handle any errors that may have occurred
                                // on the server that would cause the rollback to fail, such as
                                // a closed connection.
                                MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                MessageBox.Show("am here 3a");
                                MessageBox.Show("  Message: " + ex2.Message);
                                MessageBox.Show("am here 4a");
                            }
                        }

                    }
                    itsTrue.Content = "";
                }
                else
                {
                    MessageBox.Show("Item Already Exists In Retails!\n" + "Please, First Sell It Out Fully\n" + "To Retail Another Similar Item!");
                }
            }

            else if (listRetailProductsGrid.HasItems == false)
            {
                using (SqlConnection retailOutConnec = new SqlConnection(connec0))
                {
                    retailOutConnec.Open();
                    SqlCommand retailOutCommand = retailOutConnec.CreateCommand();
                    SqlTransaction commitTransaction = retailOutConnec.BeginTransaction();

                    retailOutCommand.Connection = retailOutConnec;
                    retailOutCommand.Transaction = commitTransaction;
                    try
                    {


                        retailOutCommand.CommandText = "INSERT INTO [RetailStock] (Barcode, ProductName, ProductDescription, Type, PacketQuantity, RemainingQuantity, UnitPrice, TotalValue, RetailedBy) VALUES (@Barcode, @ProductName, @ProductDescription, @Type, @PacketQuantity, @RemainingQuantity, @UnitPrice, @TotalValue, @RetailedBy) ";
                        retailOutCommand.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = pdtBarcode;
                        retailOutCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = pdtName;
                        retailOutCommand.Parameters.Add("@ProductDescription", SqlDbType.NVarChar).Value = pdtDescription;
                        retailOutCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pdtType;
                        retailOutCommand.Parameters.Add("@PacketQuantity", SqlDbType.Int).Value = Convert.ToInt32(pdtQuantity);
                        retailOutCommand.Parameters.Add("@RemainingQuantity", SqlDbType.Int).Value = Convert.ToInt32(pdtQuantity);
                        retailOutCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPrice) / Convert.ToInt32(pdtQuantity);
                        retailOutCommand.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPrice);
                        retailOutCommand.Parameters.Add("@RetailedBy", SqlDbType.NVarChar).Value = userNameLabel.Content.ToString();

                        retailOutCommand.ExecuteNonQuery();

                        commitTransaction.Commit();

                        listRetailProductsGrid.ItemsSource = null;
                        using (SqlConnection updateInventoryConnec = new SqlConnection(connec0))
                        {
                            updateInventoryConnec.Open();
                            SqlCommand updateInventoryCommand = updateInventoryConnec.CreateCommand();
                            SqlTransaction updateInventoryTransaction = updateInventoryConnec.BeginTransaction();

                            updateInventoryCommand.Connection = updateInventoryConnec;
                            updateInventoryCommand.Transaction = updateInventoryTransaction;
                            try
                            {

                                updateInventoryCommand.CommandText = "UPDATE [Inventory] SET Quantity = @RemainingQunatity, TotalValue = @TotalValue WHERE ProductId = @id";
                                updateInventoryCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(id);
                                updateInventoryCommand.Parameters.Add("@RemainingQunatity", SqlDbType.Int).Value = Convert.ToInt32(((DataRowView)listProductGrid.SelectedItem).Row[5].ToString()) - 1;
                                updateInventoryCommand.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(totalValue) - Convert.ToDecimal(unitPrice);

                                updateInventoryCommand.ExecuteNonQuery();

                                updateInventoryTransaction.Commit();

                                listRetailProductsGrid.ItemsSource = null;

                                using (SqlConnection loadInventoryConnec = new SqlConnection(connec0))
                                {
                                    SqlCommand getInventoryrCmd = loadInventoryConnec.CreateCommand();
                                    getInventoryrCmd.Connection = loadInventoryConnec;

                                    getInventoryrCmd.CommandText = "SELECT * FROM Inventory WHERE (Quantity > 0) ORDER BY ProductName ASC";
                                    loadInventoryConnec.Open();
                                    SqlDataAdapter tempInventoryTable = new SqlDataAdapter(getInventoryrCmd);

                                    DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                                    tempInventoryTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                                    tempInventoryTable.Fill(tempInventoryHoldingSet, "Inventory");


                                    DataTable inventoryTable = tempInventoryHoldingSet.Tables["Inventory"];

                                    listProductGrid.ItemsSource = inventoryTable.AsDataView();

                                    listProductGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                                    listProductGrid.SelectionMode = DataGridSelectionMode.Single;

                                    listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                                    listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                                    listProductGrid.Columns[7].Visibility = System.Windows.Visibility.Collapsed;
                                    listProductGrid.IsReadOnly = true;
                                }

                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show("Commit Exception Type: " + ex.GetType());
                                MessageBox.Show("  Message: " + ex.Message);

                                // Attempt to roll back the transaction.
                                try
                                {
                                    updateInventoryTransaction.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    // This catch block will handle any errors that may have occurred
                                    // on the server that would cause the rollback to fail, such as
                                    // a closed connection.
                                    MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                    MessageBox.Show("  Message: " + ex2.Message);
                                }
                            }

                            using (SqlConnection loadRetailStockConnec = new SqlConnection(connec0))
                            {
                                SqlCommand getRetailStockCmd = loadRetailStockConnec.CreateCommand();
                                getRetailStockCmd.Connection = loadRetailStockConnec;

                                getRetailStockCmd.CommandText = "SELECT * FROM RetailStock WHERE (PacketQuantity > 0) ORDER BY ProductName ASC";
                                loadRetailStockConnec.Open();

                                SqlDataAdapter tempRetailStockTable = new SqlDataAdapter(getRetailStockCmd);

                                DataSet tempRetailStockHoldingSet = new DataSet("MerakiBusinessDB");

                                tempRetailStockTable.FillSchema(tempRetailStockHoldingSet, SchemaType.Mapped, "RetailStock");
                                tempRetailStockTable.Fill(tempRetailStockHoldingSet, "RetailStock");

                                DataTable retailStockTable = tempRetailStockHoldingSet.Tables["RetailStock"];

                                listRetailProductsGrid.ItemsSource = retailStockTable.AsDataView();
                                //listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                                //listRetailProductsGrid.Columns[9].Visibility = System.Windows.Visibility.Hidden;
                                listRetailProductsGrid.IsReadOnly = true;
                            }

                            MessageBox.Show("Item Can Now Be Sold In Retail!");
                        }

                        MessageBox.Show("am here1");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Commit Exception Type: " + ex.GetType());
                        MessageBox.Show("am here2");
                        MessageBox.Show("  Message: " + ex.Message);
                        MessageBox.Show("am here3");

                        // Attempt to roll back the transaction.
                        try
                        {
                            commitTransaction.Rollback();

                        }
                        catch (Exception ex2)
                        {
                            // This catch block will handle any errors that may have occurred
                            // on the server that would cause the rollback to fail, such as
                            // a closed connection.
                            MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                            MessageBox.Show("am here4");
                            MessageBox.Show("  Message: " + ex2.Message);
                            MessageBox.Show("am here 5");
                        }
                    }

                }
            }

        }

        #endregion






        /// <summary>
        /// Now... for the retail Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listRetailProductsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listRetailProductsGrid.SelectedItem != null && retailTransactionGrid.Items.Count == 0)
            {
                changeRetailLabel.Content = "";
                MyDialog dialog = new MyDialog();
                dialog.ShowDialog();
                if (dialog.ResponseText != "")
                {
                    checkOutPdtsRetail = new ArrayList();
                    //transactionGrid.ItemsSource = null;
                    //magic code... its like heaven sent!!! it saves you a lot of time wasting and presents a cleaner code!!!
                    string idRetail = ((DataRowView)listRetailProductsGrid.SelectedItem).Row[0].ToString();
                    string pdtBarcodeRetail = ((DataRowView)listRetailProductsGrid.SelectedItem).Row[1].ToString();
                    string pdtNameRetail = ((DataRowView)listRetailProductsGrid.SelectedItem).Row[2].ToString();
                    string pdtDescriptionRetail = ((DataRowView)listRetailProductsGrid.SelectedItem).Row[3].ToString();
                    string pdtTypeRetail = ((DataRowView)listRetailProductsGrid.SelectedItem).Row[4].ToString();
                    string packetQuantityRetail = ((DataRowView)listRetailProductsGrid.SelectedItem).Row[5].ToString();
                    string pdtQuantityRetail = dialog.ResponseText;
                    string remainingQuantityRetail = Convert.ToString(Convert.ToInt32(((DataRowView)listRetailProductsGrid.SelectedItem).Row[6].ToString()) - Convert.ToInt32(pdtQuantityRetail));
                    string unitPriceRetail = Convert.ToString((Convert.ToDecimal(((DataRowView)listRetailProductsGrid.SelectedItem).Row[7].ToString())));
                    string totalAmountRetail = Convert.ToString(Convert.ToDecimal(unitPriceRetail) * Convert.ToInt32(Convert.ToInt32(pdtQuantityRetail)));

                    string finalTotalRetail = Convert.ToString(Convert.ToDecimal(String.Format("{0:0.00}", Convert.ToDecimal(retailTotalAmount.Content.ToString()))) + Convert.ToDecimal(totalAmountRetail));
                    retailTotalAmount.Content = String.Format("{0:0,0.00}", Convert.ToDecimal(finalTotalRetail));


                    if (dictionary.Count > 0)
                    {
                        int z = dictionary.Keys.First();
                        dictionary.Remove(z);

                        checkOutPdtsRetail.Add(new TransDataRowRetail(idRetail, pdtBarcodeRetail, pdtNameRetail, pdtDescriptionRetail, pdtTypeRetail, packetQuantityRetail, remainingQuantityRetail, pdtQuantityRetail, unitPriceRetail, totalAmountRetail));

                        dictionary.Add(index1, checkOutPdtsRetail);

                        retailTransactionGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        retailTransactionGrid.SelectionMode = DataGridSelectionMode.Single;

                        int y = dictionary.Keys.Max();
                        retailTransactionGrid.ItemsSource = dictionary[y];

                        //retailTransactionGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                        //retailTransactionGrid.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                        //retailTransactionGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;

                    }
                    else if (dictionary.Count == 0)
                    {
                        checkOutPdtsRetail.Add(new TransDataRowRetail(idRetail, pdtBarcodeRetail, pdtNameRetail, pdtDescriptionRetail, pdtTypeRetail, packetQuantityRetail, remainingQuantityRetail, pdtQuantityRetail, unitPriceRetail, totalAmountRetail));

                        dictionary.Add(index1, checkOutPdtsRetail);

                        retailTransactionGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                        retailTransactionGrid.SelectionMode = DataGridSelectionMode.Single;
                        int y = dictionary.Keys.Max();
                        retailTransactionGrid.ItemsSource = dictionary[y];

                        //retailTransactionGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                        //retailTransactionGrid.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                        //retailTransactionGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;
                    }



                }
                else if (dialog.ResponseText == "")
                {
                    MessageBox.Show("Please Input Quantity To Make A Valid Selection");
                }
            }
            else if (retailTransactionGrid.Items.Count !=0)
            {
                MessageBox.Show("First Sell Existing Item");
            }
            else MessageBox.Show("Double Click The Item To Make A Valid Selection");
           
        }

        #region sellRetailBtn_Click

        private void sellRetailBtn_Click(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";
            if (retailTotalAmount.Content == "0.00" && retailPaymentTextBox.Text == "")
            {
                MessageBox.Show("Please Fill In The Payment!");
            }
            else if (retailPaymentTextBox.Text == "")
            {
                MessageBox.Show("Please Fill In The Payment!");
            }
            else if (retailTransactionGrid.ItemsSource == null)
            {
                MessageBox.Show("Please Select An Item To Sell!!");
                retailPaymentTextBox.Clear();
            }
            else
            {
                for (int x = 0; x < retailTransactionGrid.Items.Count; x++)
                {
                    retailTransactionGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    retailTransactionGrid.SelectedIndex = x;

                    string id = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).Id.ToString();
                    string pdtBarcode = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).Barcode.ToString();
                    string pdtName = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).ProductName.ToString();
                    string pdtDescription = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).ProductDescription.ToString();
                    string pdtType = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).Type.ToString();
                    string packetQuantity = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).PacketQuantity.ToString();
                    string retailQuantity = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).Quantity.ToString();
                    Decimal unitPrice = Convert.ToDecimal(((TransDataRowRetail)retailTransactionGrid.SelectedItem).UnitPrice.ToString());
                    string totalAmount = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).TotalAmount.ToString();

                    string remainingQuantityRetail = ((TransDataRowRetail)retailTransactionGrid.SelectedItem).RemainingQuantity.ToString();

                    //String.Format("{0:0.00}", Convert.ToDecimal(totalsLabel1.Content.ToString()))

                    if (Convert.ToDecimal(retailPaymentTextBox.Text) >= Convert.ToDecimal(totalAmount))
                    {

                        using (SqlConnection commitRetailSaleConnec = new SqlConnection(connec0))
                        {
                            commitRetailSaleConnec.Open();
                            SqlCommand commitRetailSaleCommand = commitRetailSaleConnec.CreateCommand();
                            SqlTransaction commitRetailTransaction = commitRetailSaleConnec.BeginTransaction();

                            commitRetailSaleCommand.Connection = commitRetailSaleConnec;
                            commitRetailSaleCommand.Transaction = commitRetailTransaction;
                            try
                            {


                                commitRetailSaleCommand.CommandText = "INSERT INTO [RetailSale] (Barcode, ProductName, ProductDescription, Type, PacketQuantity, SoldQuantity, UnitPrice, TotalAmount, SoldBy) VALUES (@Barcode, @ProductName, @ProductDescription, @Type, @PacketQuantity, @SoldQuantity, @UnitPrice, @TotalAmount, @SoldBy)";
                                commitRetailSaleCommand.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = pdtBarcode;
                                commitRetailSaleCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = pdtName;
                                commitRetailSaleCommand.Parameters.Add("@ProductDescription", SqlDbType.NVarChar).Value = pdtDescription;
                                commitRetailSaleCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pdtType;
                                commitRetailSaleCommand.Parameters.Add("@PacketQuantity", SqlDbType.Int).Value = Convert.ToInt32(packetQuantity);
                                commitRetailSaleCommand.Parameters.Add("@SoldQuantity", SqlDbType.Int).Value = Convert.ToInt32(retailQuantity);
                                commitRetailSaleCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = unitPrice;
                                commitRetailSaleCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(totalAmount);
                                commitRetailSaleCommand.Parameters.Add("@SoldBy", SqlDbType.NVarChar).Value = userNameLabel.Content.ToString();

                                commitRetailSaleCommand.ExecuteNonQuery();

                                commitRetailTransaction.Commit();


                                using (SqlConnection updateRetailStockConnec = new SqlConnection(connec0))
                                {
                                    updateRetailStockConnec.Open();
                                    SqlCommand updateRetailStockCommand = updateRetailStockConnec.CreateCommand();
                                    SqlTransaction updateRetailStockTransaction = updateRetailStockConnec.BeginTransaction();

                                    updateRetailStockCommand.Connection = updateRetailStockConnec;
                                    updateRetailStockCommand.Transaction = updateRetailStockTransaction;
                                    try
                                    {


                                        updateRetailStockCommand.CommandText = "UPDATE [RetailStock] SET RemainingQuantity = @RemainingQuantity, TotalValue = @TotalValue WHERE RetailId = @id";
                                        updateRetailStockCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Convert.ToInt32(id);
                                        updateRetailStockCommand.Parameters.AddWithValue("@RemainingQuantity", SqlDbType.Int).Value = Convert.ToInt32(remainingQuantityRetail);
                                        updateRetailStockCommand.Parameters.AddWithValue("@TotalValue", SqlDbType.Int).Value = Convert.ToInt32(remainingQuantityRetail) * Convert.ToInt32(unitPrice);


                                        updateRetailStockCommand.ExecuteNonQuery();

                                        
                                        updateRetailStockTransaction.Commit();

                                        using (SqlConnection loadRetailInventoryConnec = new SqlConnection(connec0))
                                        {
                                            SqlCommand getRetailsInventoryrCmd = loadRetailInventoryConnec.CreateCommand();
                                            getRetailsInventoryrCmd.Connection = loadRetailInventoryConnec;

                                            getRetailsInventoryrCmd.CommandText = "SELECT * FROM RetailStock WHERE (RemainingQuantity > 0)";
                                            loadRetailInventoryConnec.Open();
                                            SqlDataAdapter tempRetailInventoryTable = new SqlDataAdapter(getRetailsInventoryrCmd);

                                            DataSet tempRetailInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                                            tempRetailInventoryTable.FillSchema(tempRetailInventoryHoldingSet, SchemaType.Mapped, "Inventory");
                                            tempRetailInventoryTable.Fill(tempRetailInventoryHoldingSet, "Inventory");


                                            DataTable RetailInventoryTable = tempRetailInventoryHoldingSet.Tables["Inventory"];

                                            listRetailProductsGrid.ItemsSource = RetailInventoryTable.AsDataView();

                                            listRetailProductsGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                                            listRetailProductsGrid.SelectionMode = DataGridSelectionMode.Single;

                                            listRetailProductsGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                                            
                                            listProductGrid.IsReadOnly = true;
                                        }


                                        ///alot of issues here
                                        string cashPaidMent = retailPaymentTextBox.Text;
                                        decimal totalTotal = (decimal)parser(retailTotalAmount.Content.ToString());

                                        decimal change = Convert.ToDecimal(cashPaidMent) - totalTotal;
                                        changeRetailLabel.Content = String.Format("{0:0,0.00}", change);

                                        retailTransactionGrid.ItemsSource = null;
                                        listRetailProductsGrid.ItemsSource = null;
                                        dictionary.Clear();
                                        retailTotalAmount.Content = "0.00";
                                        retailPaymentTextBox.Text = "";

                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Commit Exception Type: " + ex.GetType());
                                        MessageBox.Show("  Message: " + ex.Message);

                                        // Attempt to roll back the transaction.
                                        try
                                        {
                                            updateRetailStockTransaction.Rollback();
                                        }
                                        catch (Exception ex2)
                                        {
                                            // This catch block will handle any errors that may have occurred
                                            // on the server that would cause the rollback to fail, such as
                                            // a closed connection.
                                            MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                            MessageBox.Show("  Message: " + ex2.Message);
                                        }
                                    }
                                }


                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show("Commit Exception Type: " + ex.GetType());
                                MessageBox.Show("  Message: " + ex.Message);

                                // Attempt to roll back the transaction.
                                try
                                {
                                    commitRetailTransaction.Rollback();
                                }
                                catch (Exception ex2)
                                {
                                    // This catch block will handle any errors that may have occurred
                                    // on the server that would cause the rollback to fail, such as
                                    // a closed connection.
                                    MessageBox.Show("Rollback Exception Type: " + ex2.GetType());
                                    MessageBox.Show("  Message: " + ex2.Message);
                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Insufficient Funds!!!");
                    }

                }
                


                using (SqlConnection loadRetailStockConnec = new SqlConnection(connec0))
                {
                    SqlCommand getRetailStockCmd = loadRetailStockConnec.CreateCommand();
                    getRetailStockCmd.Connection = loadRetailStockConnec;

                    getRetailStockCmd.CommandText = "SELECT * FROM [RetailStock] WHERE (RemainingQuantity > 0)";
                    loadRetailStockConnec.Open();
                    SqlDataAdapter tempRetailStockTable = new SqlDataAdapter(getRetailStockCmd);

                    DataSet tempInventoryHoldingSet = new DataSet("MerakiBusinessDB");

                    tempRetailStockTable.FillSchema(tempInventoryHoldingSet, SchemaType.Mapped, "RetailStock");
                    tempRetailStockTable.Fill(tempInventoryHoldingSet, "RetailStock");


                    DataTable retailTable = tempInventoryHoldingSet.Tables["RetailStock"];

                    listRetailProductsGrid.ItemsSource = retailTable.AsDataView();

                    listRetailProductsGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
                    listRetailProductsGrid.SelectionMode = DataGridSelectionMode.Single;

                    //listProductGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                    //listProductGrid.Columns[6].Visibility = System.Windows.Visibility.Collapsed;
                    //listProductGrid.Columns[8].Visibility = System.Windows.Visibility.Collapsed;
                    listRetailProductsGrid.IsReadOnly = true;
                }
                checkOutPdtsRetail.Clear();
                MessageBox.Show("Transaction Has Been Successfully Committed!");

            }
        }
        #endregion
        private void logOutLabel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void clearBtn1_Click(object sender, RoutedEventArgs e)
        {
            checkOutPdts.Clear();
            dictionary.Clear();
            transactionGrid.ItemsSource = null;
            totalsLabel1.Content = "0.00";
        }

        private void clearBtn2_Click(object sender, RoutedEventArgs e)
        {
            checkOutPdtsRetail.Clear();
            dictionary.Clear();
            retailTransactionGrid.ItemsSource = null;
            retailTotalAmount.Content = "0.00";
        }


    }

}