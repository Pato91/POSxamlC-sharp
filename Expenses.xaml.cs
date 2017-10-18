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
    /// Interaction logic for Expenses.xaml
    /// </summary>
    public partial class Expenses : Page
    {
        public Expenses(string userID)
        {
            InitializeComponent();
            userNameLabel.Content = userID;

            List<string> priorityLists = new List<string>();
            priorityLists.Add("VERY HIGH");
            priorityLists.Add("HIGH");
            priorityLists.Add("MEDIUM");
            priorityLists.Add("LOW");
            priorityLists.Add("VERY LOW");

            priorityCombobox.ItemsSource = priorityLists;
            priorityCombobox.IsReadOnly = true;

            List<string> rateByList = new List<string>();
            rateByList.Add("HOURLY");
            rateByList.Add("DAILY");
            rateByList.Add("WEEKLY");
            rateByList.Add("MONTHLY");
            rateByList.Add("ANNUALLY");

            rateByCombobox.ItemsSource = rateByList;
            rateByCombobox.IsReadOnly = true;


        }

        private void expensesPage_Loaded(object sender, EventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadPaymentsConnec = new SqlConnection(connec0))
            {
                SqlCommand getUserCmd = loadPaymentsConnec.CreateCommand();
                getUserCmd.Connection = loadPaymentsConnec;

                getUserCmd.CommandText = "SELECT * FROM [Payments]";
                loadPaymentsConnec.Open();
                SqlDataAdapter tempPaymentsTable = new SqlDataAdapter(getUserCmd);

                DataSet tempPaymentsHoldingSet = new DataSet("MerakiBusinessDB");

                tempPaymentsTable.FillSchema(tempPaymentsHoldingSet, SchemaType.Mapped, "Payments");
                tempPaymentsTable.Fill(tempPaymentsHoldingSet, "Payments");


                DataTable userTable = tempPaymentsHoldingSet.Tables["Payments"];

                expensesDataGrid.ItemsSource = userTable.AsDataView();
            }
        }
        private void payButton_Click(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection commitPaymentConnec = new SqlConnection(connec0))
            {
                commitPaymentConnec.Open();
                SqlCommand commitPaymentCommand = commitPaymentConnec.CreateCommand();
                SqlTransaction commitTransaction = commitPaymentConnec.BeginTransaction();

                commitPaymentCommand.Connection = commitPaymentConnec;
                commitPaymentCommand.Transaction = commitTransaction;
                try
                {


                    commitPaymentCommand.CommandText = "INSERT INTO [Payments] (Consumables, Rate, Quantifier, DueDate, Priority, PaymentTo, AmountPaid) VALUES (@Consumables, @Rate, @Quantifier, @DueDate, @Priority, @PaymentTo, @AmountPaid)";
                    commitPaymentCommand.Parameters.Add("@Consumables", SqlDbType.NVarChar).Value = consumableName.Text.ToUpper(); ;
                    commitPaymentCommand.Parameters.Add("@Rate", SqlDbType.NVarChar).Value = rateByCombobox.Text; ;
                    commitPaymentCommand.Parameters.Add("@Quantifier", SqlDbType.Int).Value = Convert.ToInt32(serviceHoursTextBox.Text) ;
                    commitPaymentCommand.Parameters.Add("@DueDate", SqlDbType.NVarChar).Value = datePicker.DisplayDate;
                    commitPaymentCommand.Parameters.Add("@Priority", SqlDbType.NVarChar).Value = priorityCombobox.Text;
                    commitPaymentCommand.Parameters.Add("@PaymentTo", SqlDbType.NVarChar).Value = paidToTextBox.Text.ToUpper();
                    commitPaymentCommand.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = Convert.ToInt32(serviceHoursTextBox.Text) * Convert.ToDecimal(unitAmountPaid.Text);

                    commitPaymentCommand.ExecuteNonQuery();

                    commitTransaction.Commit();

                    using (SqlConnection loadPaymentsConnec = new SqlConnection(connec0))
                    {
                        SqlCommand getPaymentsCmd = loadPaymentsConnec.CreateCommand();
                        getPaymentsCmd.Connection = loadPaymentsConnec;

                        getPaymentsCmd.CommandText = "SELECT * FROM [Payments]";
                        loadPaymentsConnec.Open();
                        SqlDataAdapter tempPaymnetsTable = new SqlDataAdapter(getPaymentsCmd);

                        DataSet tempPaymentsHoldingSet = new DataSet("MerakiBusinessDB");

                        tempPaymnetsTable.FillSchema(tempPaymentsHoldingSet, SchemaType.Mapped, "Payments");
                        tempPaymnetsTable.Fill(tempPaymentsHoldingSet, "Payments");


                        DataTable paymentsTable = tempPaymentsHoldingSet.Tables["Payments"];
                        expensesDataGrid.ItemsSource = null;

                        expensesDataGrid.ItemsSource = paymentsTable.AsDataView();

                        rateByCombobox.ItemsSource = null;
                        priorityCombobox.ItemsSource = null;
                        consumableName.Text = String.Empty;
                        serviceHoursTextBox.Text = String.Empty;
                        paidToTextBox.Text = String.Empty;
                        unitAmountPaid.Text = String.Empty;

                        List<string> priorityLists = new List<string>();
                        priorityLists.Add("VERY HIGH");
                        priorityLists.Add("HIGH");
                        priorityLists.Add("MEDIUM");
                        priorityLists.Add("LOW");
                        priorityLists.Add("VERY LOW");

                        priorityCombobox.ItemsSource = priorityLists;
                        priorityCombobox.IsReadOnly = true;

                        List<string> rateByList = new List<string>();
                        rateByList.Add("HOURLY");
                        rateByList.Add("DAILY");
                        rateByList.Add("WEEKLY");
                        rateByList.Add("MONTHLY");
                        rateByList.Add("ANNUALLY");

                        rateByCombobox.ItemsSource = rateByList;
                        rateByCombobox.IsReadOnly = true;
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            _expensesFrame.NavigationService.Navigate(new HomePage(userNameLabel.Content.ToString()));
        }

        private void consumables_textPreiview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed2(e.Text);
        }

        private void quantifier_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void paymentTo_PreivewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed1(e.Text);
        }

        private void unitAmountPaid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c) && !Char.IsWhiteSpace(c) && !Char.IsSymbol(c) && !Char.IsSeparator(c) &&!Char.IsLetter(c); });
        }

        private Boolean TextBoxTextAllowed1(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsLetter(c) && !Char.IsWhiteSpace(c) && !Char.IsSymbol(c) && !Char.IsSeparator(c)&&!Char.IsDigit(c); });
        }

        private Boolean TextBoxTextAllowed2(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsLetterOrDigit(c) && !Char.IsWhiteSpace(c) && !Char.IsSeparator(c); });
        }
    }
}
