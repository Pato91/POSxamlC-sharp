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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports(string userID)
        {
            InitializeComponent();
            usernameLabel.Content = userID;
        }

        private void reports_Loaded(object sender, RoutedEventArgs e)
        {
            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection loadUserConnec = new SqlConnection(connec0))
            {
                SqlCommand getUserCmd = loadUserConnec.CreateCommand();
                getUserCmd.Connection = loadUserConnec;

                getUserCmd.CommandText = "SELECT * FROM [Employee] WHERE Status = 1";
                loadUserConnec.Open();
                SqlDataAdapter tempUserTable = new SqlDataAdapter(getUserCmd);

                DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                tempUserTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Expiry_Date");
                tempUserTable.Fill(tempUserHoldingSet, "Employee");


                DataTable userTable = tempUserHoldingSet.Tables["Employee"];

                List<string> staffUserName = new List<string>();
                staffUserName.Add("");

                var usernameRow = userTable.Rows as IEnumerable;

                foreach (var row in usernameRow)
                {
                    var userQualites = ((DataRow)row).ItemArray;
                    staffUserName.Add(userQualites.GetValue(3).ToString());
                }

                byStaffCombobox.ItemsSource = staffUserName;

                List<string> byPdtsList = new List<string>();
                byPdtsList.Add("");

                byProductCombobox.ItemsSource = byPdtsList;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _reportsFrame.NavigationService.Navigate(new HomePage(usernameLabel.Content.ToString()));
        }

        private void printSalesRptBtn_Click(object sender, RoutedEventArgs e)
        {
            
            
            if ( byStaffCombobox.Text != "")
            {
                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

                using (SqlConnection loadSalesConnec = new SqlConnection(connec0))
                {
                    SqlCommand getSalesCmd = loadSalesConnec.CreateCommand();
                    getSalesCmd.Connection = loadSalesConnec;


                    getSalesCmd.CommandText = "SELECT * FROM  [Sales] WHERE SoldBy = @SoldBy ORDER BY dateOfSale";
                    getSalesCmd.Parameters.AddWithValue("@SoldBy", SqlDbType.NVarChar).Value = byStaffCombobox.Text;
                    loadSalesConnec.Open();
                    SqlDataAdapter tempSalesTable = new SqlDataAdapter(getSalesCmd);

                    DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                    tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Sales");
                    tempSalesTable.Fill(tempUserHoldingSet, "Sales");


                    DataTable salesTable = tempUserHoldingSet.Tables["Sales"];


                    using (SqlConnection loadRetailSalesConnec = new SqlConnection(connec0))
                    {
                        SqlCommand getRetailSalesCmd = loadRetailSalesConnec.CreateCommand();
                        getRetailSalesCmd.Connection = loadRetailSalesConnec;

                        getRetailSalesCmd.CommandText = "SELECT * FROM  [RetailSale] WHERE SoldBy = @SoldBy ORDER BY SaleDate ASC";
                        getRetailSalesCmd.Parameters.AddWithValue("@SoldBy", SqlDbType.NVarChar).Value = byStaffCombobox.Text;
                        loadRetailSalesConnec.Open();
                        SqlDataAdapter tempRetailSalesTable = new SqlDataAdapter(getRetailSalesCmd);

                        DataSet tempRetailSalesHoldingSet = new DataSet("MerakiBusinessDB");

                        tempRetailSalesTable.FillSchema(tempRetailSalesHoldingSet, SchemaType.Mapped, "RetailSale");
                        tempRetailSalesTable.Fill(tempRetailSalesHoldingSet, "RetailSale");


                        DataTable retailSalesTable = tempRetailSalesHoldingSet.Tables["RetailSale"];


                        DateTime today = DateTime.Today;
                        string htmlPacketSalesTable = toHTML_Table_PacketSales2(salesTable, " "+today.Day+"/"+today.Month+"/"+today.Year);
                        string htmlRetailSalesTables = toHTML_Table_RetailSales2(retailSalesTable, byStaffCombobox.Text);
                        System.IO.File.WriteAllText(@"e:\Folder\ELiza Report For-" + DateTime.Now.Day.ToString() + ".HTML", htmlPacketSalesTable);
                        System.IO.File.AppendAllText(@"e:\Folder\abc.HTML", htmlRetailSalesTables);




                        TheWebBrowser myBrowser = new TheWebBrowser();
                        myBrowser.Url = @"e:\Folder\ELiza Report For-" + DateTime.Now.Day.ToString()+ ".HTML";
                        myBrowser.Show();

                    }
                }
            }
           
            else if (startDatePicker.SelectedDate == null && endDatePicker.SelectedDate !=null && byStaffCombobox.Text == "")
            {
                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

                using (SqlConnection loadSalesConnec = new SqlConnection(connec0))
                {
                    SqlCommand getSalesCmd = loadSalesConnec.CreateCommand();
                    getSalesCmd.Connection = loadSalesConnec;

                    getSalesCmd.CommandText = "SELECT * FROM  [Sales] WHERE DateOfSale <= @DateOfSale2 ORDER BY DateOfSale ASC";
                    getSalesCmd.Parameters.AddWithValue("@DateOfSale2", SqlDbType.Date).Value = Convert.ToDateTime(endDatePicker.SelectedDate.Value.ToString("MM-dd-yyyy"));
                    loadSalesConnec.Open();
                    SqlDataAdapter tempSalesTable = new SqlDataAdapter(getSalesCmd);

                    DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                    tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Sales");
                    tempSalesTable.Fill(tempUserHoldingSet, "Sales");


                    DataTable salesTable = tempUserHoldingSet.Tables["Sales"];


                    using (SqlConnection loadRetailSalesConnec = new SqlConnection(connec0))
                    {
                        SqlCommand getRetailSalesCmd = loadRetailSalesConnec.CreateCommand();
                        getRetailSalesCmd.Connection = loadRetailSalesConnec;

                        getRetailSalesCmd.CommandText = "SELECT * FROM  [RetailSale] WHERE SaleDate <= @DateOfSale2 ORDER BY SaleDate ASC";
                        getRetailSalesCmd.Parameters.AddWithValue("@DateOfSale2", SqlDbType.Date).Value = Convert.ToDateTime(endDatePicker.SelectedDate.Value.ToString("MM-dd-yyyy"));
                        loadRetailSalesConnec.Open();
                        SqlDataAdapter tempRetailSalesTable = new SqlDataAdapter(getRetailSalesCmd);

                        DataSet tempRetailSalesHoldingSet = new DataSet("MerakiBusinessDB");

                        tempRetailSalesTable.FillSchema(tempRetailSalesHoldingSet, SchemaType.Mapped, "ReatilSales");
                        tempRetailSalesTable.Fill(tempRetailSalesHoldingSet, "ReatilSales");


                        DataTable retailSalesTable = tempRetailSalesHoldingSet.Tables["ReatilSales"];



                        string htmlPacketSalesTable1 = toHTML_Table_PacketSales1(salesTable, endDatePicker.SelectedDate.Value);
                        string htmlRetailSalesTables1 = toHTML_Table_RetailSales1(retailSalesTable, endDatePicker.SelectedDate.Value);
                        System.IO.File.WriteAllText(@"e:\Folder\" + DateTime.Now.Day.ToString() + ".HTML", htmlPacketSalesTable1);
                        System.IO.File.AppendAllText(@"e:\Folder\" + DateTime.Now.Day.ToString() + ".HTML", htmlRetailSalesTables1);




                        TheWebBrowser myBrowser = new TheWebBrowser();
                        myBrowser.Url = @"e:\Folder\" + DateTime.Now.Day.ToString() + ".HTML";
                        myBrowser.Show();

                    }
                }
            }

            else if (startDatePicker.SelectedDate != null && endDatePicker.SelectedDate == null && byStaffCombobox.Text == "")
            {
                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

                using (SqlConnection loadSalesConnec = new SqlConnection(connec0))
                {
                    SqlCommand getSalesCmd = loadSalesConnec.CreateCommand();
                    getSalesCmd.Connection = loadSalesConnec;

                    getSalesCmd.CommandText = "SELECT * FROM  [Sales] WHERE DateOfSale >= @DateOfSale1 ORDER BY DateOfSale ASC";
                    getSalesCmd.Parameters.AddWithValue("@DateOfSale1", SqlDbType.Date).Value = Convert.ToDateTime(startDatePicker.SelectedDate.Value.ToString("MM-dd-yyyy"));
                    loadSalesConnec.Open();
                    SqlDataAdapter tempSalesTable = new SqlDataAdapter(getSalesCmd);

                    DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                    tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Sales");
                    tempSalesTable.Fill(tempUserHoldingSet, "Sales");


                    DataTable salesTable = tempUserHoldingSet.Tables["Sales"];


                    using (SqlConnection loadRetailSalesConnec = new SqlConnection(connec0))
                    {
                        SqlCommand getRetailSalesCmd = loadRetailSalesConnec.CreateCommand();
                        getRetailSalesCmd.Connection = loadRetailSalesConnec;

                        getRetailSalesCmd.CommandText = "SELECT * FROM  [RetailSale] WHERE SaleDate >= @DateOfSale1 ORDER BY SaleDate ASC";
                        getRetailSalesCmd.Parameters.AddWithValue("@DateOfSale1", SqlDbType.Date).Value = Convert.ToDateTime(startDatePicker.SelectedDate.Value.ToString("MM-dd-yyyy"));
                        loadRetailSalesConnec.Open();
                        SqlDataAdapter tempRetailSalesTable = new SqlDataAdapter(getRetailSalesCmd);

                        DataSet tempRetailSalesHoldingSet = new DataSet("MerakiBusinessDB");

                        tempRetailSalesTable.FillSchema(tempRetailSalesHoldingSet, SchemaType.Mapped, "ReatilSales");
                        tempRetailSalesTable.Fill(tempRetailSalesHoldingSet, "ReatilSales");


                        DataTable retailSalesTable = tempRetailSalesHoldingSet.Tables["ReatilSales"];



                        string htmlPacketSalesTable1 = toHTML_Table_PacketSales1(salesTable, startDatePicker.SelectedDate.Value);
                        string htmlRetailSalesTables1 = toHTML_Table_RetailSales1(retailSalesTable, startDatePicker.SelectedDate.Value);
                        System.IO.File.WriteAllText(@"e:\Folder\" + DateTime.Now.Day.ToString() + ".HTML", htmlPacketSalesTable1);
                        System.IO.File.AppendAllText(@"e:\Folder\" + DateTime.Now.Day.ToString() + ".HTML", htmlRetailSalesTables1);




                        TheWebBrowser myBrowser = new TheWebBrowser();
                        myBrowser.Url = @"e:\Folder\" + DateTime.Now.Day.ToString() + ".HTML";
                        myBrowser.Show();

                    }
                }
            }


            #region everything is null
            else if (startDatePicker.SelectedDate == null && endDatePicker.SelectedDate == null && byStaffCombobox.Text == null)
            {
                MessageBox.Show("Please First Make A selection!");
            }
            #endregion
        }

        #region packetsSales generator for two dates
        public string toHTML_Table_PacketSales(DataTable dt, DateTime startDate, DateTime endDate)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Hidden;
            decimal total = 0;
            var viewR = dt.Rows as IEnumerable;
            foreach (var item in viewR)
            {
                var moneyColumn = ((DataRow)item).ItemArray;
                total = total + Convert.ToDecimal(moneyColumn.GetValue(6).ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return "User Has Not Made Requested Packet Sales For The Specified Time Period!"; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("PACKET SALES REPORT FOR THE PERIOD OF" + startDate.ToString() + "-" + endDate.ToString());


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
            builder.Append("<br></br>");
            builder.Append("<br></br>");
            builder.Append("<br></br>");
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }
        #endregion



        public string toHTML_Table_RetailSales(DataTable dt, DateTime startDate, DateTime endDate)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Hidden;
            decimal total = 0;
            var viewR = dt.Rows as IEnumerable;
            foreach (var item in viewR)
            {
                var moneyColumn = ((DataRow)item).ItemArray;
                total = total + Convert.ToDecimal(moneyColumn.GetValue(8).ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return "User Has Not Made Requested Retail Sales For The Specified Time Period!"; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("RETAIL SALES REPORT FOR THE PERIOD OF" + startDate.ToString() + "-" + endDate.ToString());


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



        //for the same start and end dates
        #region packets sales table drawer for one date
        public string toHTML_Table_PacketSales1(DataTable dt, DateTime startDate)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Hidden;
            decimal total = 0;
            var viewR = dt.Rows as IEnumerable;
            foreach (var item in viewR)
            {
                var moneyColumn = ((DataRow)item).ItemArray;
                total = total + Convert.ToDecimal(moneyColumn.GetValue(6).ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return "User Has Not Made Requested Packet Sales For The Specified Date!"; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("PACKET SALES REPORT FOR THE DATE OF" + startDate.ToString());


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
            builder.Append("<br></br>");
            builder.Append("<br></br>");
            builder.Append("<br></br>");
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }
        #endregion

        #region retails sales for one date table drawer
        public string toHTML_Table_RetailSales1(DataTable dt, DateTime startDate)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Hidden;
            decimal total = 0;
            var viewR = dt.Rows as IEnumerable;
            foreach (var item in viewR)
            {
                var moneyColumn = ((DataRow)item).ItemArray;
                total = total + Convert.ToDecimal(moneyColumn.GetValue(8).ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return "User Has Not Made Requuest Packet Sales For The Specified Date!"; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("RETAIL SALES REPORT FOR THE DATE OF" + startDate.ToString());


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
        #endregion


        #region packet sales gnrator for on the staff name
        public string toHTML_Table_PacketSales2(DataTable dt, string staffName)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Hidden;
            decimal total = 0;
            var viewR = dt.Rows as IEnumerable;
            foreach (var item in viewR)
            {
                var moneyColumn = ((DataRow)item).ItemArray;
                total = total + Convert.ToDecimal(moneyColumn.GetValue(6).ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return "User Has Not Made Requuest Packet Sales For The Specified Date!"; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("PACKET SALES REPORT FOR THE DATE OF" + staffName);


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
            builder.Append("<br></br>");
            builder.Append("<br></br>");
            builder.Append("<br></br>");
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }
        #endregion


        #region retails sales for one staff table drawer
        public string toHTML_Table_RetailSales2(DataTable dt, string staffName)
        {
            Label totalsLbl = new Label();
            totalsLbl.Visibility = System.Windows.Visibility.Hidden;
            decimal total = 0;
            var viewR = dt.Rows as IEnumerable;
            foreach (var item in viewR)
            {
                var moneyColumn = ((DataRow)item).ItemArray;
                total = total + Convert.ToDecimal(moneyColumn.GetValue(8).ToString());
                totalsLbl.Content = total;
            }

            if (dt.Rows.Count == 0) return "User Has Not Made Requuest Packet Sales For The Specified Date!"; // enter code here

            StringBuilder builder = new StringBuilder();

            builder.Append("RETAIL SALES REPORT FOR THE DATE OF" + staffName);


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
        #endregion
    }
}

