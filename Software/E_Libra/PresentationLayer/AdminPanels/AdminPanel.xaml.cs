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

namespace PresentationLayer.AdminPanels {
    public partial class AdminPanel : Window {
        public AdminPanel() {
            InitializeComponent();
        }

        private void btnAllLibraries_Click(object sender, RoutedEventArgs e) {
            UcAllLibraries ucAllLibraries = new UcAllLibraries();
            contentPanel.Content = ucAllLibraries;
        }

        private void btnAllEmployees_Click(object sender, RoutedEventArgs e) {
            UcAllEmployees ucAllEmployees = new UcAllEmployees();
            contentPanel.Content = ucAllEmployees;
        }

        private void btnNewLibrary_Click(object sender, RoutedEventArgs e) {
            UcNewLibrary ucNewLibrary = new UcNewLibrary();
            contentPanel.Content = ucNewLibrary;
        }
    }
}
