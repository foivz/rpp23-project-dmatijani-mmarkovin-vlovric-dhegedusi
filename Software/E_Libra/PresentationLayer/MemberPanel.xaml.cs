using EntitiesLayer;
using PresentationLayer.MemberPanels;
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
    /// Interaction logic for MemberPanel.xaml
    /// </summary>
    public partial class MemberPanel : Window {
        public MemberPanel() {
            InitializeComponent();
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcMemberBorrows();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcBookSearchFilter();
        }

        private void btnWishlist_Click(object sender, RoutedEventArgs e) {
            contentPanel.Content = new UcWishlist();
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e) {
            UcAllNotificationsMember memberNotifications = new UcAllNotificationsMember();
            contentPanel.Content = memberNotifications;
        }
    }
}
