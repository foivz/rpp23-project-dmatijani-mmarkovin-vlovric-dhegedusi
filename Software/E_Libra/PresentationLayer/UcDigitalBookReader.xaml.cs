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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for UcDigitalBookReader.xaml
    /// </summary>
    public partial class UcDigitalBookReader : UserControl {
        public UcDigitalBookReader() {
            InitializeComponent();
        }

        public void LoadPDF(string pdfFilePath) {
            pdfReaderWeb.Navigate(new Uri(pdfFilePath));
        }
    }
}
