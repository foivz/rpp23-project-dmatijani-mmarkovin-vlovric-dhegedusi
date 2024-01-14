using BussinessLogicLayer.services;
using EntitiesLayer;
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
    public partial class UcAllLibraries : UserControl {
        private LibraryService service = new LibraryService();

        public UcAllLibraries() {
            InitializeComponent();
            ShowAllLibraries();
        }

        private void btnRemoveLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void btnEditLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {
            UcNewLibrary ucNewLibrary = new UcNewLibrary();
            AdminGuiControl.LoadNewControl(ucNewLibrary);
        }

        private void btnLibraryEmployees_Click(object sender, RoutedEventArgs e) {
            if (dgAllLibraries.SelectedItems.Count != 1) {
                return;
            }

            Library selectedLibrary = dgAllLibraries.SelectedItem as Library;
            if (selectedLibrary == null) {
                return;
            }

            UcAllEmployees ucAllEmployees = new UcAllEmployees(selectedLibrary);
            AdminGuiControl.LoadNewControl(ucAllEmployees);
        }

        private void ShowAllLibraries() {
            dgAllLibraries.ItemsSource = service.GetAllLibraries();
        }
    }
}
