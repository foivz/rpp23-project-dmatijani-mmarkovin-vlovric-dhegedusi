﻿using BussinessLogicLayer.services;
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
    /// <summary>
    /// Interaction logic for UcAddNewBook.xaml
    /// </summary>
    public partial class UcAddNewBook : UserControl
    {
        public UcAddNewBook()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcCatalogueOptions();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGenres();
        }

        private void LoadGenres()
        {
            GenreServices genreServices = new GenreServices();
            var genres = genreServices.GetGenres();
            cmbGenre.ItemsSource = genres;
        }
    }
}
