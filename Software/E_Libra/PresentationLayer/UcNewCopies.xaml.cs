﻿using BussinessLogicLayer.Exceptions;
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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for UcNewCopies.xaml
    /// </summary>
    public partial class UcNewCopies : UserControl
    {
        public UcNewCopies()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcCatalogueOptions();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            BookServices services = new BookServices();
            dgvBookNamesArchive.ItemsSource = services.GetNonArchivedBooks();
            foreach (var column in dgvBookNamesArchive.Columns)
            {
                if (column.Header.ToString() != "name" && column.Header.ToString() != "total_copies")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TryParseInt(txtNumberCopies.Text);
            }catch (BookException ex)
            {
                MessageBox.Show(ex.Poruka);
                return;
            }
            if(dgvBookNamesArchive.SelectedItem == null)
            {
                MessageBox.Show("Morate odabrati knjigu!");
                return;
            }

            BookServices services = new BookServices();
            int number = TryParseInt(txtNumberCopies.Text);
            var book = dgvBookNamesArchive.SelectedItem as Book;
            if(services.InsertNewCopies(number, book))
            {
                MessageBox.Show("Uspjesno!");
            }
            else
            {
                MessageBox.Show("Neuspjesno!");
            };
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcNewCopies();
        }

        private int TryParseInt(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                throw new BookException("Broj novih primjeraka mora sadržavati samo brojeve!");
            }
        }

        private void txtBookName_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookServices bookServices = new BookServices();
            string text = txtBookName.Text;
            dgvBookNamesArchive.ItemsSource = bookServices.GetNonArchivedBooksByName(text);
            foreach (var column in dgvBookNamesArchive.Columns)
            {
                if (column.Header.ToString() != "name" && column.Header.ToString() != "total_copies")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
