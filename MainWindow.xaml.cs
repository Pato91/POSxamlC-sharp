using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;




namespace Meraki101
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //_mainFrame.NavigationService.Navigated += NavigationService_Navigated;
        }




        /* void NavigationService_Navigated(object sender, NavigationEventArgs e)
         {
             //if (_mainFrame.NavigationService.CanGoBack) { _mainFrame.NavigationService.RemoveBackEntry(); }
         }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginPasswordBox.Password.Equals("") && userNameLoginTextBox.Text.Equals(""))
            {
                MessageBox.Show("Username And Password Are Required To login!");
            }

            else if (loginPasswordBox.Password != null && userNameLoginTextBox.Text.Equals(""))
            {
                MessageBox.Show("Username Is Required To login!");
            }

            else if (loginPasswordBox.Password.Equals("") && userNameLoginTextBox != null)
            {
                MessageBox.Show("Password Is Required To login!");
            }
            else if (userNameLoginTextBox.Text.Equals("merakiadmin") && loginPasswordBox.Password.Equals("quantumphysicistabj"))
            {
                _mainFrame.NavigationService.Navigate(new HomePage(userNameLoginTextBox.Text));

            }
            else if (loginPasswordBox.Password != "" && userNameLoginTextBox.Text != "")
            {

                string connec0 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MerakiBusinessDB.mdf;Integrated Security=True";

                using (SqlConnection loadUserConnec = new SqlConnection(connec0))
                {
                    SqlCommand getUserCmd = loadUserConnec.CreateCommand();
                    getUserCmd.Connection = loadUserConnec;

                    getUserCmd.CommandText = "SELECT * FROM [Employee]";
                    loadUserConnec.Open();
                    SqlDataAdapter tempUserTableAdapter = new SqlDataAdapter(getUserCmd);

                    DataSet tempUserHoldingSet = new DataSet("MerakiBusinessDB");

                    tempUserTableAdapter.FillSchema(tempUserHoldingSet, SchemaType.Mapped, "Employee");
                    tempUserTableAdapter.Fill(tempUserHoldingSet, "Employee");


                    DataTable userTable = tempUserHoldingSet.Tables["Employee"];

                    var users = userTable.Rows as IEnumerable;

                    foreach (var user in users)
                    {
                        var userQualities = ((DataRow)user).ItemArray;
                        if (userNameLoginTextBox.Text.Equals(userQualities.GetValue(3).ToString()) && loginPasswordBox.Password.Equals(Decrypt(userQualities.GetValue(6).ToString(), true)) && (Convert.ToInt32(userQualities.GetValue(5).ToString())) == 1 && (Convert.ToInt32(userQualities.GetValue(7).ToString())) == 1)
                        {
                            _mainFrame.NavigationService.Navigate(new HomePage(userNameLoginTextBox.Text));
                        }
                        else if (userNameLoginTextBox.Text.Equals(userQualities.GetValue(3).ToString()) && loginPasswordBox.Password.Equals(Decrypt(userQualities.GetValue(6).ToString(), true)) && (Convert.ToInt32(userQualities.GetValue(5).ToString())) == 1 && (Convert.ToInt32(userQualities.GetValue(7).ToString())) == 0)
                        {
                            _mainFrame.NavigationService.Navigate(new SellPage(userNameLoginTextBox.Text + "0"));
                        }
                        else if (userNameLoginTextBox.Text != userQualities.GetValue(3).ToString() && loginPasswordBox.Password != Decrypt(userQualities.GetValue(6).ToString(), true))
                        {
                            MessageBox.Show("Invalid Username Or Password!\n " + "Please Try Again!");
                        }
                        else if (userNameLoginTextBox.Text.Equals(userQualities.GetValue(3).ToString()) && loginPasswordBox.Password.Equals(Decrypt(userQualities.GetValue(6).ToString(), true)) && (Convert.ToInt32(userQualities.GetValue(5).ToString())) == 0 && (Convert.ToInt32(userQualities.GetValue(7).ToString())) != 0)
                        {
                            MessageBox.Show("Invalid Username Or Password!\n " + "Please Try Again!");
                        }

                    }


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

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
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


    //[SetUp]
    /// <summary>
    /// synchronization context to handle the exception ...The current SynchronizationContext may not be used as a TaskScheduler... 
    /// </summary>
    /* public void TestSetUp()
     {
         SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
     } */
    /*
      private void TileLayoutControl_TileClick(object sender, DevExpress.Xpf.LayoutControl.TileClickEventArgs e)
      {

      }

      void Inventory_TileClick(object sender, RoutedEventArgs e)
      {
          _mainFrame.NavigationService.Navigate(new Uri("InventoryManager.xaml", UriKind.Relative));
      }

      private void salesTile_Click(object sender, EventArgs e)
      {
          NavigationService nav = NavigationService.GetNavigationService(this.Parent);
          nav.Navigate(new Uri("InventoryManager.xaml", UriKind.Relative));
            

      }

      private void recordsTile_Click(object sender, EventArgs e)
      {

      }

      private void logOutBtn_Click(object sender, RoutedEventArgs e)
      {
          NavigationService nav = NavigationService.GetNavigationService(this);
          nav.Navigate(new Uri("InventoryManager.xaml", UriKind.Relative));
      }
     * 
     * 
     * 
  <DockPanel>
      <Frame x:Name="_mainFrame" />

      <Grid HorizontalAlignment="Left" Width="809" Margin="0,0,0,0" Background="#FF6EE2D2" Height="490" VerticalAlignment="Bottom">
          <Grid.RowDefinitions>
              <RowDefinition Height="29*"/>
              <RowDefinition Height="461*"/>
          </Grid.RowDefinitions>
          <Grid.ColumnDefinitions>
              <ColumnDefinition Width="0*"/>
              <ColumnDefinition Width="166*"/>
              <ColumnDefinition Width="68*"/>
              <ColumnDefinition Width="451*"/>
              <ColumnDefinition Width="124*"/>
          </Grid.ColumnDefinitions>

          <dxlc:TileLayoutControl x:Name="TileLayout" Margin="44,86,30,58" TileClick="TileLayoutControl_TileClick" Background="#FFB5E4F1" Grid.Column="3" Grid.ColumnSpan="2" Grid.Row="1">
              <dxlc:Tile x:Name="InventoryTile" Margin="-120,-110,105,105" Size="Small" Header="Inventory" MouseDown="Inventory_TileClick" ToolTip="Inventory">

                  <dxmvvm:Interaction.Behaviors>
                      <dxmvvm:EventToCommand EventName="Click" AllowChangingEventOwnerIsEnabled="True" MarkRoutedEventsAsHandled="True"/>
                  </dxmvvm:Interaction.Behaviors>
              </dxlc:Tile>
              <dxlc:Tile x:Name="salesTile" Margin="-115,-110,79,61" Size="Small" Header="Sales" />
              <dxlc:Tile x:Name="usersTile" Header="Users" Margin="-89,-110,89,110" Size="Small"/>
              <dxlc:Tile x:Name="recordsTile" Margin="-549,40,549,-40" Size="Small" Header="Records" Click="recordsTile_Click"/>



          </dxlc:TileLayoutControl>
          <Label x:Name="userName" Content="" Grid.Column="4" Margin="-23,6,30,0" VerticalAlignment="Top" Height="25" Grid.Row="1"/>
          <Button x:Name="logOutBtn" Content="Log Out" Grid.Column="4" Margin="13,37,36,0" VerticalAlignment="Top" Click="logOutBtn_Click" Grid.Row="1"/>
          <Label x:Name="user" Content="User" Grid.Column="3" Margin="0,5,48,0" VerticalAlignment="Top" HorizontalAlignment="Right" Width="70" Grid.Row="1"/>
          <dxwui:FlipView Margin="20,86,-47,58" Grid.ColumnSpan="3" Grid.Row="1">
              <dxwui:FlipViewItem>
                  <Grid x:Name="flipView" Background="#FFE5E5E5">
                      <Frame Content="Frame" HorizontalAlignment="Left" Height="100" Margin="161,184,-6,0" VerticalAlignment="Top" Width="100"/>
                  </Grid>
              </dxwui:FlipViewItem>
              <dxwui:FlipViewItem>
                  <Grid Background="#FFE5E5E5"/>
              </dxwui:FlipViewItem>
              <dxwui:FlipViewItem>
                  <Grid Background="#FFE5E5E5"/>
              </dxwui:FlipViewItem>
              <dxwui:FlipViewItem>
                  <Grid Background="#FFE5E5E5"/>
              </dxwui:FlipViewItem>
          </dxwui:FlipView>
          <Label Content="News" Height="29" Margin="20,52,41,0" VerticalAlignment="Top" FontWeight="Bold" FontStyle="Italic" FontSize="15" FontFamily="Segoe Script" Grid.ColumnSpan="2" Grid.Row="1"/>
          <Label Content="Meraki1.0- a Ayebale Bright Johnson Product" Grid.Column="3" Margin="-26,0,222,10" VerticalAlignment="Bottom" Height="30" Grid.Row="1"/>
      </Grid>

  </DockPanel>


     */



}
