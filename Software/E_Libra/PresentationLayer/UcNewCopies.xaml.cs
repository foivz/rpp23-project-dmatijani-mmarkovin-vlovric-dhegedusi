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
    //Viktor Lovrić
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
            dgvBookNamesArchive.ItemsSource = services.GetNonArchivedBooks(false);
            HideColumns();
        }

        private void HideColumns()
        {
            var columnName = dgvBookNamesArchive.Columns.FirstOrDefault(c => c.Header.ToString() == "name");
            columnName.Header = "Naziv";
            var columnCopies = dgvBookNamesArchive.Columns.FirstOrDefault(c => c.Header.ToString() == "total_copies");
            columnCopies.Header = "Ukupan broj primjeraka";
            foreach (var column in dgvBookNamesArchive.Columns)
            {
                if (column.Header.ToString() != "Naziv" && column.Header.ToString() != "Ukupan broj primjeraka")
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
                MessageBox.Show("Uspješno!");
            }
            else
            {
                MessageBox.Show("Neuspješno!");
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
            if (string.IsNullOrEmpty(text))
            {
                dgvBookNamesArchive.ItemsSource = null;
                return;
            }
            dgvBookNamesArchive.ItemsSource = bookServices.GetNonArchivedBooksByName(text);
            HideColumns();
        }
    }
}
