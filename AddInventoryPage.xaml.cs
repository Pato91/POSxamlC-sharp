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
using Microsoft.PointOfService;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace Meraki101
{
    /// <summary>
    /// Interaction logic for AddInventoryPage.xaml
    /// </summary>
    public partial class AddInventoryPage : Page
    {
        Label userIDlbl;
        public AddInventoryPage(string userID)
        {
            InitializeComponent();
            userIDlbl = new Label();
            userIDlbl.Content = userID;
            userIDlbl.Visibility = System.Windows.Visibility.Hidden;
            productNameComboBox.IsEditable = true;
            typeComboBox.IsEditable = true;


            //bulkInsert.Checked += bulkInsert_Checked;
            //bulkInsert.Unchecked += bulkInsert_Unchecked;


        }

        //private void bulkInsert_Checked(object sender, RoutedEventArgs e)
        //{


        //    //Thread checkForBarcodeScan = new Thread(new ThreadStart(bulkListnerMtd));
        //    //checkForBarcodeScan.Start();
        //    //checkForBarcodeScan.IsBackground = true;

        //    productNameComboBox.IsReadOnly = true;
        //    productNameComboBox.IsHitTestVisible = false;
        //    descriptionTextBox.IsReadOnly = true;
        //    typeComboBox.IsReadOnly = true;
        //    typeComboBox.IsHitTestVisible = false;
        //    unitPriceTextBox.IsReadOnly = true;

        //    barcodeTextBox.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
        //    barcodeTextBox.Foreground = new SolidColorBrush(Colors.Black);
        //    barcodeTextBox.Background = new SolidColorBrush(Colors.Honeydew);

        //    //barcodeTextBox.TextChanged += barcodeTextBox_TextChanged;


        //}

        //private void barcodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    bulkInsert.Checked += bulkInsert_Checked;
        //    if (bulkInsert.IsChecked == true)
        //    {
        //        string[] controlVals = new string[] { barcodeTextBox.Text, productNameComboBox.Text, descriptionTextBox.Text, typeComboBox.Text, quantityTextBox.Text, unitPriceTextBox.Text };


        //        BackgroundWorker worker = new BackgroundWorker();
        //        worker.WorkerReportsProgress = true;
        //        worker.DoWork += bulkListnerMtd;
        //        worker.ProgressChanged += worker_ProgressChanged;
        //        worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        //        worker.RunWorkerAsync(controlVals);
        //    }


        //}







        //public void bulkListnerMtd(Object sender, DoWorkEventArgs e)
        //{

        //    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        //                (ThreadStart)delegate()
        //                {
        //                    //BarcodeScanner bulkScanner = new BarcodeScanner();
        //                    //bulkScanner.Event_Mehtod();
        //                    //string barcode = bulkScanner.barcodeLabel.Content.ToString();

        //                    //barcodeTextBox.Text = barcode;
        //                    string[] receivedControlVals = (string[])e.Argument;


        //                    //TextRange richTextBoxRange = new TextRange(descriptionTextBox.Document.ContentStart, descriptionTextBox.Document.ContentEnd);




        //                    if (receivedControlVals[0] != "" && receivedControlVals[1] != "" && receivedControlVals[3] != "" && receivedControlVals[5] != "")
        //                    {

        //                        Stopwatch stopwatch = new Stopwatch();
        //                        stopwatch.Start();

        //                        int f = Convert.ToInt32(receivedControlVals[4]);
        //                        do
        //                        {


        //                               f++;
        //                                stopwatch.Reset();



        //                        }
        //                        while (stopwatch.ElapsedMilliseconds < 5000);

        //                        (sender as BackgroundWorker).ReportProgress(f);

        //                        string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

        //                        using (SqlConnection addBulkConnec = new SqlConnection(connec0))
        //                        {
        //                            addBulkConnec.Open();
        //                            SqlCommand bulkInsertCmd = addBulkConnec.CreateCommand();
        //                            SqlTransaction bulkInsertTransac = addBulkConnec.BeginTransaction();

        //                            bulkInsertCmd.Connection = addBulkConnec;
        //                            bulkInsertCmd.Transaction = bulkInsertTransac;

        //                            try
        //                            {
        //                                using (SqlConnection readInventorytable = new SqlConnection(connec0))
        //                                {
        //                                    bulkInsertCmd.CommandText = "INSERT INTO [Inventory] Barcode, ProductName, ProductDescription, Type, Quantiy, UnitPrice VALUES @Barcode, @ProductName, @ProductDescription, @Type, @Quantity, @VAT(%), @UnitPrice";
        //                                    bulkInsertCmd.Parameters.AddWithValue("@Barcode", SqlDbType.Int).Value = Convert.ToInt32(receivedControlVals[0]);
        //                                    bulkInsertCmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = Convert.ToString(receivedControlVals[1]);
        //                                    bulkInsertCmd.Parameters.AddWithValue("@ProductDescription", SqlDbType.NVarChar).Value = Convert.ToString(receivedControlVals[2]);
        //                                    bulkInsertCmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = Convert.ToString(receivedControlVals[3]);
        //                                    bulkInsertCmd.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = f;
        //                                    bulkInsertCmd.Parameters.AddWithValue("UnitPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(receivedControlVals[5]);

        //                                    readInventorytable.Open();
        //                                    bulkInsertCmd.ExecuteNonQuery();
        //                                    readInventorytable.Close();

        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                MessageBox.Show("Commit Exception Type: {0} " + ex.GetType());
        //                                MessageBox.Show("  Message: {0} " + ex.Message);

        //                                // Attempt to roll back the transaction.
        //                                try
        //                                {
        //                                    bulkInsertTransac.Rollback();
        //                                }
        //                                catch (Exception ex2)
        //                                {
        //                                    // This catch block will handle any errors that may have occurred
        //                                    // on the server that would cause the rollback to fail, such as
        //                                    // a closed connection.
        //                                    MessageBox.Show("Rollback Exception Type: {0} " + ex2.GetType());
        //                                    MessageBox.Show("  Message: {0} " + ex2.Message);
        //                                }
        //                            }
        //                        }


        //                    }

        //                }
        //                  );


        //}
        //private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //    quantityTextBox.Text = Convert.ToString(e.ProgressPercentage);


        //}

        //private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    MessageBox.Show("Products Successfully Added");
        //}



        private void barcodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed3(e.Text);
        }
        private void quantityTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }


        private void packetQuantityTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void purchasePriceTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void unitPriceTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }


        private void productNameComboBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed3(e.Text);
        }

        private void descriptionTextBox_PreivewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed3(e.Text);
        }

        private void typeComboBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed1(e.Text);
        }


        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c); });
        }

        private Boolean TextBoxTextAllowed1(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsUpper(c); });
        }

        private Boolean TextBoxTextAllowed2(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c) || Char.IsUpper(c) ; });
        }

        private Boolean TextBoxTextAllowed3(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c) || Char.IsSymbol(c) || Char.IsPunctuation(c) || Char.IsUpper(c); });
        }

        private void backToIInventoryManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            _addInventory.NavigationService.Navigate(new InventoryManager(userIDlbl.Content.ToString()));
        }





        //private void bulkInsert_Unchecked(object sender, RoutedEventArgs e) 
        //{
        //    productNameComboBox.IsReadOnly = false;
        //    productNameComboBox.IsHitTestVisible = true;
        //    descriptionTextBox.IsReadOnly = false;
        //    typeComboBox.IsReadOnly = false;
        //    typeComboBox.IsHitTestVisible = true;
        //    unitPriceTextBox.IsReadOnly = false;
        //}


        private void commitBtn_Click(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            if (Convert.ToDecimal(unitPriceTextBox.Text) >= Convert.ToDecimal(purchasePriceTextBox.Text))
            {
                using (SqlConnection addSingleConnec = new SqlConnection(connec0))
                {
                    addSingleConnec.Open();
                    SqlCommand singleInsertCmd = addSingleConnec.CreateCommand();
                    SqlTransaction singleInsertTransac = addSingleConnec.BeginTransaction();

                    singleInsertCmd.Connection = addSingleConnec;
                    singleInsertCmd.Transaction = singleInsertTransac;

                    try
                    {
                        using (SqlConnection singleInsertInventorytable = new SqlConnection(connec0))
                        {
                            singleInsertCmd.CommandText = "INSERT INTO [Inventory] (Barcode, ProductName, ProductDescription, Type, Quantity, PacketQuantity, BoughtAt, UnitPrice, TotalValue) VALUES (@Barcode, @ProductName, @ProductDescription, @Type, @Quantity, @PacketQuantity, @BoughtAt, @UnitPrice, @TotalValue)";
                            singleInsertCmd.Parameters.AddWithValue("@Barcode", SqlDbType.NVarChar).Value = barcodeTextBox.Text;
                            singleInsertCmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = productNameComboBox.Text;
                            singleInsertCmd.Parameters.AddWithValue("@ProductDescription", SqlDbType.NVarChar).Value = descriptionTextBox.Text;
                            singleInsertCmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = typeComboBox.Text;
                            singleInsertCmd.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = Convert.ToInt32(quantityTextBox.Text);
                            singleInsertCmd.Parameters.AddWithValue("@PacketQuantity", SqlDbType.Int).Value = Convert.ToInt32(packetQuantityTextBox.Text);
                            singleInsertCmd.Parameters.AddWithValue("@BoughtAt", SqlDbType.Decimal).Value = Convert.ToDecimal(purchasePriceTextBox.Text);
                            singleInsertCmd.Parameters.AddWithValue("@UnitPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPriceTextBox.Text);
                            singleInsertCmd.Parameters.AddWithValue("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(Convert.ToDecimal(unitPriceTextBox.Text) * Convert.ToInt32(quantityTextBox.Text));

                            singleInsertInventorytable.Open();
                            singleInsertCmd.ExecuteNonQuery();
                            MessageBox.Show("Products Saved To Inventory");
                            //singleInsertInventorytable.Close();

                            singleInsertTransac.Commit();

                            barcodeTextBox.Clear();
                            productNameComboBox.Text = "";
                            descriptionTextBox.Clear();
                            typeComboBox.Text = "";
                            unitPriceTextBox.Clear();
                            quantityTextBox.Text = "";
                            packetQuantityTextBox.Text = "";
                            purchasePriceTextBox.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Commit Exception Type: {0} " + ex.GetType());
                        MessageBox.Show("  Message: {0} " + ex.Message);

                        // Attempt to roll back the transaction.
                        try
                        {
                            singleInsertTransac.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            // This catch block will handle any errors that may have occurred
                            // on the server that would cause the rollback to fail, such as
                            // a closed connection.
                            MessageBox.Show("Rollback Exception Type: {0} " + ex2.GetType());
                            MessageBox.Show("  Message: {0} " + ex2.Message);
                        }
                    }
                }

            }
            else MessageBox.Show("Please Check Unit Selling Price\n" + "(Should Be Atleast Equals To Unit Purchase Price)");

        }


    }


}
