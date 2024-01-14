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
    public partial class UcAllEmployees : UserControl {
        private EmployeeService service = new EmployeeService();

        public UcAllEmployees() {
            InitializeComponent();
            PopulateComboBox();
        }

        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e) {
            UcNewEmployee ucNewEmployee = new UcNewEmployee();
            AdminGuiControl.LoadNewControl(ucNewEmployee);
        }

        private void PopulateComboBox() {
            var libraryService = new LibraryService();
            cboLibrary.ItemsSource = libraryService.GetAllLibraries();
        }

        private void cboLibrary_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Library selectedLibrary = cboLibrary.SelectedItem as Library;
            LoadEmployees(selectedLibrary);
        }
        
        private void LoadEmployees(Library selectedLibrary) {
            if (selectedLibrary == null) {
                dgAllEmployees.ItemsSource = new List<Library>();
                return;
            }

            List<Employee> employees = service.GetEmployeesByLibrary(selectedLibrary);
            dgAllEmployees.ItemsSource = employees;
        }
    }
}
