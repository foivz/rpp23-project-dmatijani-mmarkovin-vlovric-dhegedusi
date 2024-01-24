﻿using BussinessLogicLayer.services;
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

namespace PresentationLayer.EmployeePanels
{
    /// <summary>
    /// Interaction logic for UcAllNotifications.xaml
    /// </summary>
    public partial class UcAllNotifications : UserControl
    {
        NotificationService notificationService;
        EmployeeService employeeService;
        public UcAllNotifications()
        {
            InitializeComponent();
            notificationService = new NotificationService();
            employeeService = new EmployeeService();
        }

        private void btnNewNotification_Click(object sender, RoutedEventArgs e)
        {
            UcNewNotification newNotification = new UcNewNotification();
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = newNotification;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var id = employeeService.GetEmployeeLibraryId(LoggedUser.Username);
            dgvAllNotifications.ItemsSource = notificationService.GetAllNotificationByLibrary(id);
            dgvAllNotifications.Columns[0].Visibility = Visibility.Hidden;
            dgvAllNotifications.Columns[3].Visibility = Visibility.Hidden;
            dgvAllNotifications.Columns[4].Visibility = Visibility.Hidden;
            dgvAllNotifications.Columns[5].Visibility = Visibility.Hidden;
        }
    }
}
