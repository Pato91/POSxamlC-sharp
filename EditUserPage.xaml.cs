using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        Label testLabel = new Label();

        public EditUserPage(string userID, string adminName)
        {
            InitializeComponent();
            userNameLabel1.Content = adminName;
            testLabel.Visibility = System.Windows.Visibility.Hidden;
            testLabel.Content = userID;
            Loaded += EditUserPage_Loaded;
        }


        private void EditUserPage_Loaded(object sender, RoutedEventArgs e)
        {


            SqlConnection connec0 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True");
            {
                try
                {
                    SqlCommand userAttributes = new SqlCommand("SELECT * FROM [Employee] WHERE UserId = @UserId", connec0);
                    userAttributes.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = Convert.ToInt32(testLabel.Content.ToString());

                    connec0.Open();
                    userAttributes.ExecuteNonQuery();

                    SqlDataAdapter tempSalesTable = new SqlDataAdapter(userAttributes);

                    DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                    tempSalesTable.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Employee");
                    tempSalesTable.Fill(tempUserHoldingSet, "Employee");


                    DataTable userTable = tempUserHoldingSet.Tables["Employee"];

                    var rowR = userTable.Rows[0].ItemArray;

                    firstNameTextBox.Text = Convert.ToString(rowR.GetValue(1));
                    lastNameTextBox.Text = Convert.ToString(rowR.GetValue(2));
                    userNameTextBox.Text = Convert.ToString(rowR.GetValue(3));
                    userContact.Text = Convert.ToString(rowR.GetValue(4));
                    passcodeTextBox.Text = Decrypt(Convert.ToString(rowR.GetValue(6)), true);
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

        private Boolean TextBoxTextAllowed1(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c); });
        }
        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsLetter(c); });
        }

        private void firstNameTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void lastNameTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void userContact_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed1(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _adduserFrame.NavigationService.Navigate(new UsersPage(userNameLabel1.Content.ToString()));
        }

        private void createUserBtn_Click(object sender, RoutedEventArgs e)
        {

            if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "")
            {
                System.Windows.MessageBox.Show("Name and password", "Warning");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(passcodeTextBox.Text.Trim(), "^(?=.*?[a-z])(?=.*?[0-9])"))
            {
                System.Windows.MessageBox.Show("Check Password!!");
            }
            else if (firstNameTextBox.Text != "" && lastNameTextBox.Text != "" && userNameTextBox.Text != "" && passcodeTextBox.Text.Length > 5 && System.Text.RegularExpressions.Regex.IsMatch(passcodeTextBox.Text.Trim(), "^(?=.*?[a-z])(?=.*?[0-9])"))
            {
                SqlConnection connec0 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True");
                {

                    try
                    {
                        SqlCommand userAttributes = new SqlCommand("UPDATE [Employee] SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Contact = @Contact, Status = @Status, Password = @Password WHERE UserId = @UserId", connec0);
                        userAttributes.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = Convert.ToInt32(testLabel.Content.ToString());
                        userAttributes.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = firstNameTextBox.Text.ToUpper();
                        userAttributes.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = lastNameTextBox.Text.ToUpper();
                        userAttributes.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = userNameTextBox.Text.ToLower();
                        userAttributes.Parameters.AddWithValue("@Contact", SqlDbType.NVarChar).Value = userContact.Text;
                        userAttributes.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = 1;
                        userAttributes.Parameters.AddWithValue("@Password", Encrypt(passcodeTextBox.Text, true));

                        connec0.Open();
                        userAttributes.ExecuteNonQuery();

                        System.Windows.MessageBox.Show("Saved Successfuly", "DONE!");
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

                    firstNameTextBox.Text = string.Empty;
                    lastNameTextBox.Text = string.Empty;
                    userNameTextBox.Text = string.Empty;
                    passcodeTextBox.Text = string.Empty;
                    userContact.Text = string.Empty;
                }


            }

        }
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey",
                                                         typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
