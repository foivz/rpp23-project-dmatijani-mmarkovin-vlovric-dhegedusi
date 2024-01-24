﻿using System;
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
        public UcAllNotifications()
        {
            InitializeComponent();
        }

        private void btnNewNotification_Click(object sender, RoutedEventArgs e)
        {
            UcNewNotification newNotification = new UcNewNotification();
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = newNotification;
        }
    }
}