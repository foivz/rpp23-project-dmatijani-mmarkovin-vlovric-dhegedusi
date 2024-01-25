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
            ShowNotifications();
            dgvNotifications.Columns[0].Visibility = Visibility.Hidden;
            dgvNotifications.Columns[3].Visibility = Visibility.Hidden;
            dgvNotifications.Columns[4].Visibility = Visibility.Hidden;
            dgvNotifications.Columns[5].Visibility = Visibility.Hidden;
        }
        private void ShowNotifications()
        {
            var id = memberService.GetMemberLibraryId(LoggedUser.Username);
            Member loggedMember = memberService.GetMemberByUsername(LoggedUser.Username);
            List<Notification> readNotifications = notificationService.GetReadNotificationsForMember(loggedMember);
            List<Notification> allNotificationsFotLibrary = notificationService.GetAllNotificationByLibrary(id);
            List<Notification> sortedNotifications = allNotificationsFotLibrary.OrderBy(notification => readNotifications.Contains(notification)).ToList();

            dgvNotifications.ItemsSource = allNotificationsFotLibrary;
        }

        private void btnNotificationDetails_Click(object sender, RoutedEventArgs e)
        {
            Notification selectedNotification = dgvNotifications.SelectedItem as Notification;
            if (selectedNotification != null) { 
                (Window.GetWindow(this) as MemberPanel).contentPanel.Content = new UcDetailsNotification(selectedNotification);
            notificationService.AddNotificationRead(selectedNotification);
            } else
            {
                MessageBox.Show("Odaberite obavijest!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
