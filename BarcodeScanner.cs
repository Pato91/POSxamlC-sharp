using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Microsoft.PointOfService;
namespace Meraki101
{
    public partial class BarcodeScanner
    {
        private PosExplorer explorer;
        private Scanner scanner;

        public Label barcodeLabel;

        public void Event_Mehtod()
        {
            explorer = new PosExplorer();
            explorer.DeviceAddedEvent += new DeviceChangedEventHandler(explorer_DeviceAddedEvent);
        }

        private string UpdateEventHistory(string newEvent)
        {
            string updateHistory = newEvent;
            return updateHistory;
        }

        public void explorer_DeviceAddedEvent(object sender, DeviceChangedEventArgs e)
        {
            if (e.Device.Type == "Scanner")
            {
                scanner = (Scanner)explorer.CreateInstance(e.Device);
                scanner.Open();
                scanner.Claim(1000);
                scanner.DeviceEnabled = true;
                scanner.DataEvent += new DataEventHandler(scanner_DataEvent);
                scanner.DataEventEnabled = true;
                scanner.DecodeData = true;

            }
        }

        public void scanner_DataEvent(object sender, DataEventArgs e)
        {
            UpdateEventHistory("Data Event");
            ASCIIEncoding encoder = new ASCIIEncoding();
            try
            {
                // Display the ASCII encoded label text
                barcodeLabel.Content = encoder.GetString(scanner.ScanDataLabel);
                // Display the encoding type
                string barcodeType = scanner.ScanDataType.ToString();

                // re-enable the data event for subsequent scans
                scanner.DataEventEnabled = true;

            }
            catch (PosControlException)
            {
                // Log any errors
                UpdateEventHistory("DataEvent Operation Failed");
            }

        }



    }
}
