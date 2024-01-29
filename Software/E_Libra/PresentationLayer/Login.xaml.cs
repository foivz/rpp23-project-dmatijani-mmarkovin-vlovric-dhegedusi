using PresentationLayer.AdminPanels;
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

namespace PresentationLayer {
    public partial class MainWindow : Window {
        private AdministratorService adminService;
        private MemberService memberService;
        private EmployeeService employeeService;
        public MainWindow() {
            InitializeComponent();
            adminService = new AdministratorService();
            memberService = new MemberService();
            employeeService = new EmployeeService();

            KeyDown += MainWindow_KeyDown;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0) {
                lblPassword.Visibility = Visibility.Collapsed;
            } else {
                lblPassword.Visibility = Visibility.Visible;
            }
        }
        private void lblUsrname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && txtUsername.Text.Length > 0)
            {
                lblUsrname.Visibility = Visibility.Collapsed;
            } else
            {
                lblUsrname.Visibility = Visibility.Visible;
            }
        }

        private void lblPassword_MouseDown(object sender, MouseButtonEventArgs e) {
            txtPassword.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {

            var password = txtPassword.Password;
            var username = txtUsername.Text;

            adminService.CheckLoginCredentials(username, password);
            employeeService.CheckLoginCredentials(username, password);
            memberService.CheckLoginCredentials(username, password);

            switch (LoggedUser.UserType)
            {
                case Role.Admin:
                    {
                        AdminPanel adminPanel = new AdminPanel();
                        Hide();
                        adminPanel.ShowDialog();
                        Close();
                        break;
                    }
                case Role.Employee:
                    {
                        EmployeePanel employeePanel = new EmployeePanel();
                        Hide();
                        employeePanel.ShowDialog();
                        Close();
                        break;
                    }
                    
                case Role.Member:
                    {
                        MemberPanel memberPanel = new MemberPanel();
                        Hide();
                        memberPanel.ShowDialog();
                        Close();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Unijeli ste krive korisničke podatke!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.F1) {
                ShowHelp();
            }
        }

        private void ShowHelp() {
            WinHelpLogin helpWindow = new WinHelpLogin();
            helpWindow.ShowDialog();
        }
    }
}
