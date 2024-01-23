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
            LoadBook();
        }

        private void LoadBook() {
            // Set the path to your PDF file
            string pdfFilePath = "Path_To_Your_PDF_File.pdf";

            // Load the PDF file into the PDFViewerControl
            digitalBookReader.LoadPDF(pdfFilePath);
        }
    }
 }
