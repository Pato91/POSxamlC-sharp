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
    /// Interaction logic for ReportGenerator.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public SalesPage(string userID)
        {
            InitializeComponent();


            // limiting the filter comboBox functionalities
            userNameLabel.Content = userID;
            List<String> filterList = new List<string>();
            filterList.Add("Barcode");
            filterList.Add("Product Name");
            filterList.Add("Date Of Sale");
            filterList.Add("Sold By");

            filterByComboBox.ItemsSource = filterList;


        }
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateFilter != null)
            {
                salesDataGrid.IsReadOnly = true;
                salesDataGrid.ItemsSource = null;
                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

                using (SqlConnection loadSalesConnec = new SqlConnection(connec0))
                {
                    SqlCommand getSalesCmd = loadSalesConnec.CreateCommand();
                    getSalesCmd.Connection = loadSalesConnec;

                    getSalesCmd.CommandText = "SELECT * FROM  [Sales] WHERE DateOfSale = @DateOfSale";
                    getSalesCmd.Parameters.AddWithValue("@DateOfSale", SqlDbType.Date).Value = Convert.ToDateTime(dateFilter.SelectedDate.Value.ToString("MM-dd-yyyy"));
                    loadSalesConnec.Open();
                    SqlDataAdapter tempSalesTable = new SqlDataAdapter(getSalesCmd);

                    DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                    tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Inventory");
                    tempSalesTable.Fill(tempUserHoldingSet, "Inventory");


                    DataTable salesTable = tempUserHoldingSet.Tables["Inventory"];

                    salesDataGrid.ItemsSource = salesTable.AsDataView();

                    salesDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;




                }
            }
        }
        private void SalesPage_Load(object sender, RoutedEventArgs e)
        {
            salesDataGrid.IsReadOnly = true;
            salesDataGrid.ItemsSource = null;
            //Deactivates all filters at start and sets the first element by default to Barcode
            filterByComboBox.SelectedItem = filterByComboBox.Items[0].ToString();
            dateFilter.IsHitTestVisible = false;
            salesSearchControl.IsHitTestVisible = true;
            soldByNameComboBox.IsHitTestVisible = false;

            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadSalesConnec = new SqlConnection(connec0))
            {
                SqlCommand getSalesCmd = loadSalesConnec.CreateCommand();
                getSalesCmd.Connection = loadSalesConnec;

                getSalesCmd.CommandText = "SELECT * FROM  [Sales]";
                loadSalesConnec.Open();
                SqlDataAdapter tempSalesTable = new SqlDataAdapter(getSalesCmd);

                DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Inventory");
                tempSalesTable.Fill(tempUserHoldingSet, "Inventory");


                DataTable salesTable = tempUserHoldingSet.Tables["Inventory"];

                salesDataGrid.ItemsSource = salesTable.AsDataView();

                salesDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;



            }


            using (SqlConnection loadEmployeeSalesConnec = new SqlConnection(connec0))
            {
                SqlCommand getEmployeeCmd = loadEmployeeSalesConnec.CreateCommand();
                getEmployeeCmd.Connection = loadEmployeeSalesConnec;

                getEmployeeCmd.CommandText = "SELECT * FROM  [Employee]";
                loadEmployeeSalesConnec.Open();
                SqlDataAdapter tempRetailSalesTable = new SqlDataAdapter(getEmployeeCmd);


                DataSet tempEmployeeHoldingSet = new DataSet("MerakiBusinessDB");

                tempRetailSalesTable.FillSchema(tempEmployeeHoldingSet, SchemaType.Mapped, "Employee");
                tempRetailSalesTable.Fill(tempEmployeeHoldingSet, "Employee");


                DataTable employeeTable = tempEmployeeHoldingSet.Tables["Employee"];

                List<String> filterListEmployee = new List<string>();
                var itemSource = employeeTable as IEnumerable;

                if (itemSource != null)
                {
                    foreach (var item in itemSource)
                    {
                        filterListEmployee.Add(((DataRowView)item).Row[3].ToString());
                    }
                    MessageBox.Show(filterListEmployee.ToArray().ToString());
                    soldByNameComboBox.ItemsSource = filterListEmployee;
                }


            }
        }
        private void SalesType_Checked(object sender, RoutedEventArgs e)
        {
            salesDataGrid.IsReadOnly = true;
            salesDataGrid.ItemsSource = null;
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadRetailSalesConnec = new SqlConnection(connec0))
            {
                SqlCommand getRetailSalesCmd = loadRetailSalesConnec.CreateCommand();
                getRetailSalesCmd.Connection = loadRetailSalesConnec;

                getRetailSalesCmd.CommandText = "SELECT * FROM  [RetailSale]";
                loadRetailSalesConnec.Open();
                SqlDataAdapter tempRetailSalesTable = new SqlDataAdapter(getRetailSalesCmd);

                DataSet tempRetailSalesHoldingSet = new DataSet("MerakiBusinessDB");

                tempRetailSalesTable.FillSchema(tempRetailSalesHoldingSet, SchemaType.Mapped, "Inventory");
                tempRetailSalesTable.Fill(tempRetailSalesHoldingSet, "Inventory");


                DataTable retailSalesTable = tempRetailSalesHoldingSet.Tables["Inventory"];

                salesDataGrid.ItemsSource = retailSalesTable.AsDataView();

                salesDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;


            }
        }

        private void SalesType_Unchecked(object sender, RoutedEventArgs e)
        {
            salesDataGrid.IsReadOnly = true;
            salesDataGrid.ItemsSource = null;
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadSalesConnec = new SqlConnection(connec0))
            {
                SqlCommand getSalesCmd = loadSalesConnec.CreateCommand();
                getSalesCmd.Connection = loadSalesConnec;

                getSalesCmd.CommandText = "SELECT * FROM  [Sales]";
                loadSalesConnec.Open();
                SqlDataAdapter tempSalesTable = new SqlDataAdapter(getSalesCmd);

                DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Inventory");
                tempSalesTable.Fill(tempUserHoldingSet, "Inventory");


                DataTable salesTable = tempUserHoldingSet.Tables["Inventory"];

                salesDataGrid.ItemsSource = salesTable.AsDataView();

                salesDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;


            }
        }

        private void backToInventoryPage3_Click(object sender, RoutedEventArgs e)
        {
            _salesPageFrame.NavigationService.Navigate(new HomePage(userNameLabel.Content.ToString()));
        }

        private void filterComboBox_SelectedItem(object sender, RoutedEventArgs e)
        {
            //Limiting 2: Limiting the functionalities of the filtering by disabling some and enabling others not at all once
            if (filterByComboBox.SelectedItem.ToString().Equals("Date Of Sale"))
            {
                dateFilter.IsHitTestVisible = true;
                salesSearchControl.IsHitTestVisible = false;
                salesSearchControl.SearchText = null;
                soldByNameComboBox.Text = null;
                soldByNameComboBox.IsHitTestVisible = false;
            }
            else if (filterByComboBox.SelectedItem.ToString().Equals("Barcode") || filterByComboBox.SelectedItem.ToString().Equals("Product Name"))
            {
                salesSearchControl.IsHitTestVisible = true;
                dateFilter.IsHitTestVisible = false;
                dateFilter.SelectedDate = null;
                soldByNameComboBox.IsHitTestVisible = false;
                soldByNameComboBox.Text = null;
            }
            else if (filterByComboBox.SelectedItem.ToString().Equals("Sold By"))
            {
                soldByNameComboBox.IsHitTestVisible = true;
                dateFilter.IsHitTestVisible = false;
                dateFilter.SelectedDate = null;
                salesSearchControl.IsHitTestVisible = false;
                salesSearchControl.SearchText = null;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void printSalesReportBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)salesDataGrid.ItemsSource).ToTable();
            string htmlTable = toHTML_Table(table);
            System.IO.File.WriteAllText(@"e:\Folder\abc.HTML", htmlTable);

            TheWebBrowser myBrowser = new TheWebBrowser();
            myBrowser.Url = @"e:\Folder\abc.HTML";
            myBrowser.Show();
        }

        public string toHTML_Table(DataTable dt)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Collapsed;
            var viewR = salesDataGrid.ItemsSource as IEnumerable;
            decimal total = 0;
            foreach (var item in viewR)
            {
                total = total + Convert.ToDecimal(((DataRowView)item).Row[salesDataGrid.Columns.Count - 3].ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            if (salesDataGrid.Columns.Count == 9)
            {
                builder.Append("PACKET SALES REPORT FOR " + DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString());

            }
            else if (salesDataGrid.Columns.Count == 11)
            {
                builder.Append("RETAIL SALES REPORT FOR " + DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString());

            }
            builder.Append("<br></br>");
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("<title>");
            builder.Append("Page-");
            builder.Append(Guid.NewGuid());
            builder.Append("</title>");
            builder.Append(" <style type=\"text/css\">body{overflow-x: scroll; } table{ background:aqua; } #headd{color:black; font: 14px arial;}</style>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td id='headd' align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            foreach (DataRow r in dt.Rows)
            {
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(r[c.ColumnName]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            builder.Append("<br></br>");
            builder.Append("Total Sales Amount To(Ug.Shs): " + String.Format("{0:0,0.00}", Convert.ToDecimal(totalsLbl.Content)));
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }
    }
}