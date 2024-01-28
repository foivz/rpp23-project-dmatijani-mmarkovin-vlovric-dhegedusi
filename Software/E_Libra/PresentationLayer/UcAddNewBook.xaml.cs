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
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            AuthorService authorService = new AuthorService();
            cmbAuthor.ItemsSource = authorService.GetAllAuthors();
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
                MessageBox.Show("Morate unijeti broj primjeraka knjige! Ako je knjiga digitalna unesite 0");
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
            if(cmbAuthor.Text == "")
            {
                MessageBox.Show("Morate odabrati autora!");
                return;
            }
            EmployeeService service = new EmployeeService();
            
            try
            {
                ConvertIntoDateTime(txtDate);
            }catch (Exception)
            {
                MessageBox.Show("Neispravan format datuma! Primjer formata je 05-09-2002");
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
            var author = cmbAuthor.SelectedItem as Author;
            var bookService = new BookServices();
            var rez = bookService.AddBook(book, author);
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

        private void btnNewAuthor_Click(object sender, RoutedEventArgs e)
        {
            UcNewAuthor ucNewAuthor = new UcNewAuthor();
            ucNewAuthor.PrevForm = this;
            ucNewAuthor.CancelButtonClicked += UcNewAuthor_CancelButtonClicked;
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucNewAuthor;
        }

        private void UcNewAuthor_CancelButtonClicked(object sender, EventArgs e)
        {
            LoadAuthors();
        }

        private void btnNewGenre_Click(object sender, RoutedEventArgs e)
        {
            UcNewGenre ucNewGenre = new UcNewGenre();
            ucNewGenre.PrevForm = this;
            ucNewGenre.ButtonClicked += UcNewGenre_ButtonClicked;
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = ucNewGenre;
        }

        private void UcNewGenre_ButtonClicked(object sender, EventArgs e)
        {
            LoadGenres();
        }
    }
}
