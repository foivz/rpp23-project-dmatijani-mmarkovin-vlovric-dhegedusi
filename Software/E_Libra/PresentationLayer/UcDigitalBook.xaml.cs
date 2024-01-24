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
    /// Interaction logic for UcDigitalBook.xaml
    /// </summary>
    public partial class UcDigitalBook : UserControl {
        public UcDigitalBook() {
            InitializeComponent();
            string pdfFilePath = "https://www.gutenberg.org/cache/epub/2265/pg2265-images.html";
            LoadPDF(pdfFilePath);
        }


        public void LoadPDF(string pdfFilePath) {
            pdfReaderWeb.Navigate(new Uri(pdfFilePath));
        }
    }
}
