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

namespace PresentationLayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lblKorime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtKorime.Focus();
        }

        private void txtKorime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtKorime.Text) && txtKorime.Text.Length > 0) {
                lblKorime.Visibility = Visibility.Collapsed;
            } else
            {
                lblKorime.Visibility = Visibility.Visible;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                lblPassword.Visibility = Visibility.Collapsed;
            } else
            {
                lblPassword.Visibility = Visibility.Visible;
            }
        }

        private void lblPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserPanel userPanel = new UserPanel();
            Hide();
            userPanel.ShowDialog();
            Close();
        }
    }
}
