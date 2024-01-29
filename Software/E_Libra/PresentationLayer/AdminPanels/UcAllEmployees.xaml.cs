﻿using BussinessLogicLayer.Exceptions;
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
    // David Matijanić
    public partial class UcAllEmployees : UserControl {
        private EmployeeService service = new EmployeeService();

        public UcAllEmployees(Library selectedLibrary = null) {
            InitializeComponent();
            PopulateComboBox(selectedLibrary);

            if (selectedLibrary != null) {
                LoadEmployees(selectedLibrary);
            }
        }

        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e) {
            Library selectedLibrary = cboLibrary.SelectedItem as Library;
            UcNewEmployee ucNewEmployee = new UcNewEmployee(selectedLibrary);
            AdminGuiControl.LoadNewControl(ucNewEmployee);
        }

        private void PopulateComboBox(Library selectedLibrary = null) {
            var libraryService = new LibraryService();
            Task.Run(() => {
                var allLibraries = libraryService.GetAllLibraries();

                Application.Current.Dispatcher.Invoke(() => {
                    cboLibrary.ItemsSource = allLibraries;

                    if (selectedLibrary != null) {
                        cboLibrary.SelectedItem = allLibraries.FirstOrDefault(l => l.id == selectedLibrary.id);
                    }
                });
            });
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

            Task.Run(() => {
                List<Employee> employees = service.GetEmployeesByLibrary(selectedLibrary);

                Application.Current.Dispatcher.Invoke(() => {
                    dgAllEmployees.ItemsSource = employees;
                });
            });
        }

        private void btnEditEmployee_Click(object sender, RoutedEventArgs e) {
            Employee selectedEmployee = GetSelectedEmployee();
            if (selectedEmployee == null) {
                return;
            }

            UcNewEmployee ucEditEmployee = new UcNewEmployee(selectedEmployee);
            AdminGuiControl.LoadNewControl(ucEditEmployee);
        }

        private Employee GetSelectedEmployee() {
            if (dgAllEmployees.SelectedItems.Count != 1) {
                MessageBox.Show("Odaberite jednog zaposlenika!");
                return null;
            }

            Employee selectedEmployee = dgAllEmployees.SelectedItem as Employee;
            if (selectedEmployee == null) {
                MessageBox.Show("Odaberite jednog zaposlenika!");
                return null;
            }

            return selectedEmployee;
        }

        private void btnRemoveEmployee_Click(object sender, RoutedEventArgs e) {
            Employee selectedEmployee = GetSelectedEmployee();
            if (selectedEmployee == null) return;

            try {
                service.DeleteEmployee(selectedEmployee);
            } catch (EmployeeException ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            
            Library selectedLibrary = cboLibrary.SelectedItem as Library;
            if (selectedLibrary != null) {
                LoadEmployees(selectedLibrary);
            }
        }
    }
}
