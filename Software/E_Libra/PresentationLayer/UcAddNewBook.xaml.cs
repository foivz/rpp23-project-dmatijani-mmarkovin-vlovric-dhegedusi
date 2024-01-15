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
    /// Interaction logic for UcAddNewBook.xaml
    /// </summary>
    public partial class UcAddNewBook : UserControl
    {
        string checkboxValue;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Morate unijeti ime knjige!");
                return;
            }
            if (txtNumberCopies.Text == "")
            {
                MessageBox.Show("Morate unijeti broj primjeraka knjige!");
                return;
            }
            if (cmbGenre.Text == "")
            {
                MessageBox.Show("Morate odabrati žanr!");
                return;
            }
            if(GetCheckBoxValue() == 3)
            {
                MessageBox.Show("Morate odabrati je li knjiga digitalna!");
                return;
            }
            EmployeeService service = new EmployeeService();
            
            try
            {
                ConvertIntoDateTime(txtDate);
            }catch (Exception)
            {
                MessageBox.Show("Neispravan format datum! Primjer formata je 05-09-2002");
                return;
            }
            try
            {
                TryParseInt(txtNumberPages.Text);
            }catch(BookException ex)
            {
                MessageBox.Show(ex.Poruka);
                return;
            }
            try
            {
                TryParseInt(txtNumberCopies.Text);
            }
            catch (BookException ex)
            {
                MessageBox.Show(ex.Poruka);
                return;
            }


            var book = new Book
            {
                name = txtName.Text,
                description = txtDescription.Text,
                publish_date = ConvertIntoDateTime(txtDate),
                pages_num = TryParseInt(txtNumberPages.Text),
                digital = GetCheckBoxValue(),
                url_digital = txtLinkDigital.Text,
                url_photo = txtLinkPicture.Text,
                total_copies = (int)TryParseInt(txtNumberCopies.Text),
                Genre = cmbGenre.SelectedItem as Genre,
                Library_id = service.GetEmployeeLibraryId(LoggedUser.Username)
            };
            var bookService = new BookServices();
            var rez = bookService.AddBook(book);
            MessageBox.Show(rez ? "Uspješno" : "Neuspješno");
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcAddNewBook();
        }

        private DateTime? ConvertIntoDateTime(TextBox txtDate)
        {
            if(txtDate.Text == "")
            {
                return null;
            }
            DateTime date = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }

        private int GetCheckBoxValue()
        {
            if (checkboxValue == "Da") return 1;
            else if (checkboxValue == "Ne") return 0;
            else return 3;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            
            if(radioButton.IsChecked == true)
            {
                checkboxValue = radioButton.Content.ToString();
                
            }
        }
        private int? TryParseInt(string input)
        {
            if(input == "")
            {
                return null;
            }
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                throw new BookException("Polja u koja se upisuje broj moraju sadržavati samo brojeve!");
            }
        }
    }
}