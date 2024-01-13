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

namespace PresentationLayer.AdminPanels {
    /// <summary>
    /// Interaction logic for UcAllLibraries.xaml
    /// </summary>
    public partial class UcAllLibraries : UserControl {
        public UcAllLibraries() {
            InitializeComponent();
        }

        private void btnRemoveLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void btnEditLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Metoda "btnLibraryEmployees_Click" otvara novi UserControl koji ima popis svih zaposlenika odabrane knjižnice
        /// David Matijanić
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLibraryEmployees_Click(object sender, RoutedEventArgs e) {
            // IMPLEMENTIRATI !
        }
    }
}
