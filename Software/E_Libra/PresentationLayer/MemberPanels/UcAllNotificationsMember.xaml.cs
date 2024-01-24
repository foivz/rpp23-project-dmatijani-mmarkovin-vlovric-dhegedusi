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

namespace PresentationLayer.MemberPanels
{
    /// <summary>
    /// Interaction logic for UcAllNotificationsMember.xaml
    /// </summary>
    public partial class UcAllNotificationsMember : UserControl
    {
        NotificationService notificationService;
        MemberService memberService;
        public UcAllNotificationsMember()
        {
            InitializeComponent();
            notificationService = new NotificationService();
            memberService = new MemberService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var id = memberService.GetMemberLibraryId(LoggedUser.Username);
            dgvNotifications.ItemsSource = notificationService.GetAllNotificationByLibrary(id);
            dgvNotifications.Columns[0].Visibility = Visibility.Hidden;
            dgvNotifications.Columns[3].Visibility = Visibility.Hidden;
            dgvNotifications.Columns[4].Visibility = Visibility.Hidden;
            dgvNotifications.Columns[5].Visibility = Visibility.Hidden;
        }

        private void btnNotificationDetails_Click(object sender, RoutedEventArgs e)
        {
            Notification selectedNotification = dgvNotifications.SelectedItem as Notification;
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = new UcDetailsNotification(selectedNotification);
        }
    }
}
