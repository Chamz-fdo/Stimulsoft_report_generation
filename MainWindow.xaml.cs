using Microsoft.Win32;
using Stimulsoft.Base;
using Stimulsoft.Report;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System;
using Stimulsoft.Base.Json;

namespace Connecting_to_Data_from_Code
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        [System.ComponentModel.Bindable(true)]
        [System.ComponentModel.Browsable(false)]
        public object SelectedItem { get; set; }
        public object ComboBoxFormat { get; private set; }

        public MainWindow()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);

            InitializeComponent();
        }

        private object GetComboBoxFormat()
        {
            return ComboBoxFormat;
        }

        private void ShowReport(DataSet dataSet, object comboBoxFormat)
        {
            var report = new StiReport();
            report.Load(@"Reports\PurchaseInvoiceNew.mrt");
            report.Dictionary.Databases.Clear();
            //report.RegData("Demo", dataSet);

            var stream = new MemoryStream();

            report.ExportDocument(StiExportFormat.Pdf, stream);
            saveFileDialog.DefaultExt = ".pdf";
            report.PrintWithWpf();

            MessageBox.Show("The print action is complete.", "Print Report");

            //saveFileDialog.FileName = report.ReportName;
            //if (saveFileDialog.ShowDialog() == true)
            // {
            // Save to Local Storage
            //  using (var fileStream = File.Create(saveFileDialog.FileName))
            //  {
            //      stream.Seek(0, SeekOrigin.Begin);
            //     stream.CopyTo(fileStream);
            //   }
            //  }

        }
        private void ButtonXML_Click(object sender, RoutedEventArgs e)
        {
            var dataSet = new DataSet();
            //dataSet.ReadXmlSchema(@"Data\Demo.xsd");
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\Administrator\\Desktop\\Samples-WPF-Platform-NET-Core-3.1-Reporting-Tool-master\\Connecting to Data from Code\\xmlrecords";         
            openFileDialog.Filter = "Stimulsoft Reports (*.xml)|*.xml";  
            
            if (openFileDialog.ShowDialog() == true)
            {
                dataSet.ReadXml(openFileDialog.FileName);          
            }       
            ShowReport(dataSet, GetComboBoxFormat());
        }

        private void ButtonJSON_Click(object sender, RoutedEventArgs e)
        {
            var dataSet = new DataSet();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\Administrator\\Desktop\\Samples-WPF-Platform-NET-Core-3.1-Reporting-Tool-master\\Connecting to Data from Code\\JData";
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";
             
            if (openFileDialog.ShowDialog() == true)
            {            
                // Get the selected file path
                string selectedFilePath = openFileDialog.FileName;

                // Read the JSON data from the file
                dataSet = StiJsonToDataSetConverterV2.GetDataSetFromFile(selectedFilePath);                 
            }           
            ShowReport(dataSet, GetComboBoxFormat());
        }
    }
}
