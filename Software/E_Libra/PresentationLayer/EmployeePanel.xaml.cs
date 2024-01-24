using EntitiesLayer;
using PresentationLayer.EmployeePanels;
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

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Window {
        public EmployeePanel() {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            Hide();
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void btnBookCatalog_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcCatalogueOptions();
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            UcAllNotifications ucAllNotifications = new UcAllNotifications();
            contentPanel.Content = ucAllNotifications;
        }
    }
}
