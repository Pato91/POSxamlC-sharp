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
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        bool isRetail;
       Label userIDlbl;
       Label itemIDLbl;
        public EditPage(string userID, string itemID, bool isRetail)
        {
            InitializeComponent();
            userIDlbl = new Label();
            userIDlbl.Content = userID;
            userIDlbl.Visibility = System.Windows.Visibility.Hidden;

            itemIDLbl = new Label();
            itemIDLbl.Content = itemID;
            itemIDLbl.Visibility = System.Windows.Visibility.Hidden;

            productNameComboBox.IsEditable = true;
            typeComboBox.IsEditable = true;

            this.isRetail = isRetail;

            Loaded += EditPage_Loaded;


        }

        void EditPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (isRetail == false)
            {
                SqlConnection connec0 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True");
                {
                    try
                    {
                        SqlCommand itemAttributes = new SqlCommand("SELECT * FROM [Inventory] WHERE ProductId = @ProductId", connec0);
                        itemAttributes.Parameters.AddWithValue("@ProductId", SqlDbType.Int).Value = Convert.ToInt32(itemIDLbl.Content.ToString());

                        connec0.Open();
                        itemAttributes.ExecuteNonQuery();

                        SqlDataAdapter tempStockTable = new SqlDataAdapter(itemAttributes);

                        DataSet tempStockHoldingSet = new DataSet("MerakiBusinessDB");

                        tempStockTable.FillSchema(tempStockHoldingSet, SchemaType.Mapped, "Employee");
                        tempStockTable.Fill(tempStockHoldingSet, "Employee");


                        DataTable userTable = tempStockHoldingSet.Tables["Employee"];

                        var rowR = userTable.Rows[0].ItemArray;

                        barcodeTextBox.Text = Convert.ToString(rowR.GetValue(1));
                        productNameComboBox.Text = Convert.ToString(rowR.GetValue(2));
                        descriptionTextBox.Text = Convert.ToString(rowR.GetValue(3));
                        typeComboBox.Text = Convert.ToString(rowR.GetValue(4));
                        quantityTextBox.Text = Convert.ToString(rowR.GetValue(5));
                        packetQuantityTextBox.Text = Convert.ToString(rowR.GetValue(6));
                        purchasePriceTextBox.Text = Convert.ToString(rowR.GetValue(7));
                        unitPriceTextBox.Text = Convert.ToString(rowR.GetValue(8));
                    }
                    catch (System.Data.SqlClient.SqlException sqlException)
                    {
                        // Try to close the connection
                        if (connec0 != null)
                            connec0.Dispose();

                        //
                        //can't connect
                        //
                        System.Windows.Forms.MessageBox.Show(sqlException.Message);

                        // Stop here
                        System.Windows.MessageBox.Show("Employee wasn't saved into the system");
                    }
                }
            }
            else if (isRetail == true)
            {
                unitPriceTextBox.Visibility = System.Windows.Visibility.Hidden;
                purchasePriceLbl.Content = "Unit Price";
                qty.Content = "PacketQty";
                remainingQty.Content = "Remaining Qty";
                sellingLbl.Visibility = System.Windows.Visibility.Hidden;
                SqlConnection connec0 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True");
                {
                    try
                    {
                        SqlCommand itemAttributes = new SqlCommand("SELECT * FROM [RetailStock] WHERE RetailId = @ProductId", connec0);
                        itemAttributes.Parameters.AddWithValue("@ProductId", SqlDbType.Int).Value = Convert.ToInt32(itemIDLbl.Content.ToString());

                        connec0.Open();
                        itemAttributes.ExecuteNonQuery();

                        SqlDataAdapter tempStockTable = new SqlDataAdapter(itemAttributes);

                        DataSet tempStockHoldingSet = new DataSet("MerakiBusinessDB");

                        tempStockTable.FillSchema(tempStockHoldingSet, SchemaType.Mapped, "Employee");
                        tempStockTable.Fill(tempStockHoldingSet, "Employee");


                        DataTable userTable = tempStockHoldingSet.Tables["Employee"];

                        var rowR = userTable.Rows[0].ItemArray;

                        barcodeTextBox.Text = Convert.ToString(rowR.GetValue(1));
                        productNameComboBox.Text = Convert.ToString(rowR.GetValue(2));
                        descriptionTextBox.Text = Convert.ToString(rowR.GetValue(3));
                        typeComboBox.Text = Convert.ToString(rowR.GetValue(4));
                        quantityTextBox.Text = Convert.ToString(rowR.GetValue(5));
                        packetQuantityTextBox.Text = Convert.ToString(rowR.GetValue(6));
                        purchasePriceTextBox.Text = Convert.ToString(rowR.GetValue(7));
                    }
                    catch (System.Data.SqlClient.SqlException sqlException)
                    {
                        // Try to close the connection
                        if (connec0 != null)
                            connec0.Dispose();

                        //
                        //can't connect
                        //
                        System.Windows.Forms.MessageBox.Show(sqlException.Message);

                        // Stop here
                        System.Windows.MessageBox.Show("");
                    }
                }
            }

           
        }
    

    
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
            _editInventory.NavigationService.Navigate(new ViewsStockPage(userIDlbl.Content.ToString()));
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
            if (isRetail == false)
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
                                singleInsertCmd.CommandText = "UPDATE [Inventory] SET Barcode = @Barcode, ProductName = @ProductName, ProductDescription = @ProductDescription, Type = @Type, Quantity = @Quantity, PacketQuantity = @PacketQuantity, BoughtAt = @BoughtAt, UnitPrice = @UnitPrice, TotalValue = @TotalValue WHERE ProductId = @ProductId";
                                singleInsertCmd.Parameters.AddWithValue("@Barcode", SqlDbType.NVarChar).Value = barcodeTextBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = productNameComboBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@ProductDescription", SqlDbType.NVarChar).Value = descriptionTextBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = typeComboBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = Convert.ToInt32(quantityTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@PacketQuantity", SqlDbType.Int).Value = Convert.ToInt32(packetQuantityTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@BoughtAt", SqlDbType.Decimal).Value = Convert.ToDecimal(purchasePriceTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@UnitPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(unitPriceTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(Convert.ToDecimal(unitPriceTextBox.Text) * Convert.ToInt32(quantityTextBox.Text));
                                singleInsertCmd.Parameters.AddWithValue("@ProductId", SqlDbType.Int).Value = Convert.ToInt32(Convert.ToDecimal(itemIDLbl.Content.ToString()));

                                singleInsertInventorytable.Open();
                                singleInsertCmd.ExecuteNonQuery();
                                MessageBox.Show("Inventory Products Updated!!");
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
            else if (isRetail == true)
            {
                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

                if (purchasePriceTextBox.Text!= "")
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
                                singleInsertCmd.CommandText = "UPDATE [RetailStock] SET Barcode = @Barcode, ProductName = @ProductName, ProductDescription = @ProductDescription, Type = @Type, PacketQuantity = @PacketQuantity, RemainingQuantity = @RemainingQuantity, UnitPrice = @UnitPrice, TotalValue = @TotalValue WHERE RetailId = @ProductId";
                                singleInsertCmd.Parameters.AddWithValue("@ProductId", SqlDbType.Int).Value = Convert.ToInt32(itemIDLbl.Content.ToString());
                                singleInsertCmd.Parameters.AddWithValue("@Barcode", SqlDbType.NVarChar).Value = barcodeTextBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = productNameComboBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@ProductDescription", SqlDbType.NVarChar).Value = descriptionTextBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = typeComboBox.Text;
                                singleInsertCmd.Parameters.AddWithValue("@PacketQuantity", SqlDbType.Int).Value = Convert.ToInt32(quantityTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@RemainingQuantity", SqlDbType.Int).Value = Convert.ToInt32(packetQuantityTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@UnitPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(purchasePriceTextBox.Text);
                                singleInsertCmd.Parameters.AddWithValue("@TotalValue", SqlDbType.Decimal).Value = Convert.ToDecimal(Convert.ToDecimal(purchasePriceTextBox.Text) * Convert.ToInt32(packetQuantityTextBox.Text));

                                singleInsertInventorytable.Open();
                                singleInsertCmd.ExecuteNonQuery();
                                MessageBox.Show("Retail Inventory Products Updated!!");
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
                else MessageBox.Show("Please Check Unit Price!");

            }
            
        }


    }


}
