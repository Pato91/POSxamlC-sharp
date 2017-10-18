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
    /// Interaction logic for ExpirryDateEdit.xaml
    /// </summary>
    public partial class ExpirryDateEdit : Page
    {
        public ExpirryDateEdit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";
            using (SqlConnection newsConnec = new SqlConnection(connec0))
            {
                SqlCommand getNewsCmd = newsConnec.CreateCommand();
                getNewsCmd.Connection = newsConnec;

                getNewsCmd.CommandText = "INSERT INTO [Expiry_Date] (Batch_Number, Barcode, ProductName, Expiry_Date) VALUES (@Batch_Number, @Barcode, @ProductName, @Expiry_Date) ";
                getNewsCmd.Parameters.AddWithValue("@Batch_Number", SqlDbType.BigInt).Value = Convert.ToInt64(batchNumberTextBox.Text);
                getNewsCmd.Parameters.AddWithValue("@Barcode", SqlDbType.BigInt).Value = Convert.ToInt64(BarcodeText.Text);
                getNewsCmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = NameText.Text;
                getNewsCmd.Parameters.AddWithValue("@Expiry_Date", SqlDbType.Date).Value = Convert.ToDateTime(ExpDateText.Text);

                newsConnec.Open();

                getNewsCmd.ExecuteNonQuery();

                MessageBox.Show("Saved Successfuly", "DONE!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _expiryEditPageFrame.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }
    }
}