using BussinessLogicLayer.Exceptions;
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
    /// <summary>
    /// Interaction logic for UcNewEmployee.xaml
    /// </summary>
    public partial class UcNewEmployee : UserControl {
        public UcNewEmployee(Library selectedLibrary = null) {
            InitializeComponent();
            PopulateComboBox(selectedLibrary);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            AdminGuiControl.LoadPreviousControl();
        }

        private void PopulateComboBox(Library selectedLibrary = null) {
            var libraryService = new LibraryService();
            var allLibraries = libraryService.GetAllLibraries();
            cboLibrary.ItemsSource = allLibraries;

            if (selectedLibrary != null) {
                cboLibrary.SelectedItem = allLibraries.FirstOrDefault(l => l.id == selectedLibrary.id);
            }
        }

        private void btnAddNewLibrary_Click(object sender, RoutedEventArgs e) {
            SaveEmployee();
        }

        private void SaveEmployee() {
            Library selectedLibrary = cboLibrary.SelectedItem as Library;
            if (selectedLibrary == null) {
                MessageBox.Show("Potrebno je odabrati knjižnicu!");
                return;
            }
            string newEmployeeName = tbEmployeeName.Text;
            string newEmployeeSurname = tbEmployeeSurname.Text;
            string newEmployeeUsername = tbEmployeeUsername.Text;
            string newEmployeePassword = tbEmployeePassword.Text;
            string newEmployeeOIB = tbEmployeeOIB.Text;

            Employee newEmployee = new Employee {
                name = newEmployeeName,
                surname = newEmployeeSurname,
                username = newEmployeeUsername,
                password = newEmployeePassword,
                OIB = newEmployeeOIB,
                Library = selectedLibrary
            };

            try {
                EmployeeService service = new EmployeeService();

                int result = service.AddEmployee(newEmployee);
                if (result > 0) {
                    AdminGuiControl.LoadNewControl(new UcAllEmployees(selectedLibrary));
                } else {
                    MessageBox.Show("Zaposlenika nije moguće dodati.");
                }
            } catch (EmployeeException ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
