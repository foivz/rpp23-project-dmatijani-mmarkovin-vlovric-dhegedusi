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
        public MainWindow() {
            InitializeComponent();
            adminService = new AdministratorService();

        private void lblKorime_MouseDown(object sender, MouseButtonEventArgs e) {
            txtKorime.Focus();
        }

        private void txtKorime_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(txtKorime.Text) && txtKorime.Text.Length > 0) {
                lblKorime.Visibility = Visibility.Collapsed;
            } else {
                lblKorime.Visibility = Visibility.Visible;
            }
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
            Close();
        }
    }
}
