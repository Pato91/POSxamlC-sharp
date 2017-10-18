using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Meraki101
{
    /// <summary>
    /// Interaction logic for MyDialog.xaml
    /// </summary>
    public partial class MyDialog : Window
    {
        public MyDialog()
        {
            InitializeComponent();
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(),
                delegate(Char c) { return Char.IsDigit(c); });
        }

        private void quantityPromptTextBox_PreviewText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        public string ResponseText
        {
            get { return quantityPromptTextBox.Text; }
            set { quantityPromptTextBox.Text = value; }
        }
        private void quantitySubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
