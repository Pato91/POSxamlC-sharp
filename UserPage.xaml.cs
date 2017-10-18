using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Meraki101
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    /// 


    public partial class UsersPage : Page
    {
        System.Windows.Controls.Label lbl34;
        public UsersPage(string userID)
        {
            InitializeComponent();
            userNameLabel3.Content = userID;
            Loaded += UserPage_Load;
            searchUserControl.ManipulationStarted += searchUserControl_searching;
            employeeDataGrid.MouseDoubleClick += employeeDataGridRow_DoubleClicked;

            lbl34 = new System.Windows.Controls.Label();

            lbl34.Visibility = System.Windows.Visibility.Hidden;
        }


        private void UserPage_Load(object sender, RoutedEventArgs e)
        {
            deleteUserBtn.IsHitTestVisible = false;
            editUserBtn.IsHitTestVisible = false;
            deleteUserBtn.Background = new SolidColorBrush(Colors.OrangeRed);
            editUserBtn.Background = new SolidColorBrush(Colors.OrangeRed);


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

                employeeDataGrid.ItemsSource = userTable.AsDataView();
                employeeDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[7].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[8].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.IsReadOnly = true;
            }
        }

        public void searchUser_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            //string[] keyWords = searchUserControl.SearchText.Split(' ');
            //string brokenSearchWord = searchUserControl.SearchText;

            string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

            using (SqlConnection searchUserConnec = new SqlConnection(connec0))
            {
                SqlCommand searchUserCmd = searchUserConnec.CreateCommand();
                searchUserCmd.Connection = searchUserConnec;

                searchUserCmd.CommandText = "SELECT * FROM Employee";
                searchUserConnec.Open();
                SqlDataAdapter tempSearchedUserTable = new SqlDataAdapter(searchUserCmd);

                DataSet tempSearchedUserHoldingSet = new DataSet("MerakiBusinessDB");

                tempSearchedUserTable.FillSchema(tempSearchedUserHoldingSet, SchemaType.Mapped, "Expiry_Date");
                tempSearchedUserTable.Fill(tempSearchedUserHoldingSet, "Employee");

                DataTable userTable = tempSearchedUserHoldingSet.Tables["Employee"];



                employeeDataGrid.ItemsSource = null;
                employeeDataGrid.Items.Clear();

                employeeDataGrid.ItemsSource = userTable.AsDataView();

                //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(employeeDataGrid.ItemsSource);
                //view.Filter = userFilter;

                // sorter.Filter = "(UserName LIKE '%" + searchUserControl.SearchText + "%')";

                /*employeeDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[4].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
                employeeDataGrid.Columns[1].IsReadOnly = true;
                employeeDataGrid.Columns[2].IsReadOnly = true;
                employeeDataGrid.Columns[3].IsReadOnly = true;
                employeeDataGrid.Columns[7].IsReadOnly = true;*/

                //}


                ///not yet there
                char b = Convert.ToChar(e.Key.ToString());
                if (Char.IsLetterOrDigit(b))
                {
                    var itemsSource = employeeDataGrid.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {
                        foreach (var item in itemsSource)
                        {
                            var row = ((DataRowView)employeeDataGrid.SelectedItem).Row["UserName"];
                            if (row != null)
                            {
                                if (row.ToString().StartsWith(searchUserControl.SearchText))
                                {
                                    employeeDataGrid.SelectedItem = row;
                                }
                            }
                        }
                    }

                }

            }


        }





        private bool userFilter(object sender, FilterEventArgs e)
        {
            DataRow t = e.Item as DataRow;
            if (t != null)
            // If filter is turned on, filter completed items.
            {
                if (t.ItemArray.Contains(searchUserControl.SearchText))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }

            }
            return true;
        }

        private void employeeDataGridRow_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            deleteUserBtn.Background = new SolidColorBrush(Colors.GreenYellow);
            editUserBtn.Background = new SolidColorBrush(Colors.GreenYellow);
            deleteUserBtn.IsHitTestVisible = true;
            editUserBtn.IsHitTestVisible = true;
        }
        private void searchUserControl_searching(object sender, RoutedEventArgs e)
        {
            /* string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

             using (SqlConnection newsConnec = new SqlConnection(connec0))
             {
                 SqlCommand getNewsCmd = newsConnec.CreateCommand();
                 getNewsCmd.Connection = newsConnec;

                 getNewsCmd.CommandText = "SELECT * FROM Employee";
                 newsConnec.Open();
                 SqlDataAdapter tempUserTable = new SqlDataAdapter(getNewsCmd);

                 DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                 tempUserTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Expiry_Date");
                 tempUserTable.Fill(tempUserHoldingSet, "Employee");


                 DataTable userTable = tempUserHoldingSet.Tables["Employee"];

                 employeeDataGrid.DataContext = userTable.DefaultView;


             }*/
        }
        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewUserFrame.NavigationService.Navigate(new AddUserPage(userNameLabel3.Content.ToString()));
        }

        private void backToUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewUserFrame.NavigationService.Navigate(new HomePage(userNameLabel3.Content.ToString()));
        }

        private void deleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do You Want To Delete This User?", "Confirmation!", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";
                using (SqlConnection deleteUserConnec = new SqlConnection(connec0))
                {
                    SqlCommand deleteUserCmd = deleteUserConnec.CreateCommand();
                    deleteUserCmd.Connection = deleteUserConnec;

                    deleteUserCmd.CommandText = "UPDATE [Employee] SET Status = 0 WHERE UserId = @UserId";
                    deleteUserCmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = (employeeDataGrid.SelectedItem as DataRowView).Row[0];
                    deleteUserConnec.Open();

                    deleteUserCmd.ExecuteNonQuery();
                    deleteUserConnec.Close();

                    System.Windows.MessageBox.Show("Employee Account Successfully Deleted");

                    employeeDataGrid.ItemsSource = null;
                    employeeDataGrid.Items.Clear();

                    using (SqlConnection loadNewUserConnec = new SqlConnection(connec0))
                    {
                        SqlCommand getNewUserCmd = loadNewUserConnec.CreateCommand();
                        getNewUserCmd.Connection = loadNewUserConnec;

                        getNewUserCmd.CommandText = "SELECT * FROM Employee WHERE Status = 1";
                        loadNewUserConnec.Open();
                        SqlDataAdapter tempNewUserTable = new SqlDataAdapter(getNewUserCmd);

                        DataSet tempNewUserHoldingSet = new DataSet("MerakiBusinessDB");

                        tempNewUserTable.FillSchema(tempNewUserHoldingSet, SchemaType.Mapped, "Expiry_Date");
                        tempNewUserTable.Fill(tempNewUserHoldingSet, "Employee");


                        DataTable newUserTable = tempNewUserHoldingSet.Tables["Employee"];

                        employeeDataGrid.ItemsSource = newUserTable.AsDataView();
                        employeeDataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
                        employeeDataGrid.Columns[4].Visibility = System.Windows.Visibility.Hidden;
                        employeeDataGrid.Columns[5].Visibility = System.Windows.Visibility.Hidden;
                        employeeDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
                        employeeDataGrid.Columns[7].Visibility = System.Windows.Visibility.Hidden;
                        employeeDataGrid.Columns[1].IsReadOnly = true;
                        employeeDataGrid.Columns[2].IsReadOnly = true;
                        employeeDataGrid.Columns[3].IsReadOnly = true;
                        employeeDataGrid.Columns[7].IsReadOnly = true;

                        deleteUserBtn.IsHitTestVisible = false;
                        editUserBtn.IsHitTestVisible = false;
                        deleteUserBtn.Background = new SolidColorBrush(Colors.OrangeRed);
                        editUserBtn.Background = new SolidColorBrush(Colors.OrangeRed);
                    }

                }
            }
            else if (result == MessageBoxResult.No)
            {
                System.Windows.MessageBox.Show("User Has Not Been Deleted!");
            }


        }

        private void editUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string userID = ((DataRowView)employeeDataGrid.SelectedItem).Row[0].ToString();
            _viewUserFrame.NavigationService.Navigate(new EditUserPage(userID, userNameLabel3.Content.ToString()));
            //_viewUserFrame.NavigationService.Navigate(new Uri("EditUserPage.xaml", UriKind.Relative));
        }


    }
}