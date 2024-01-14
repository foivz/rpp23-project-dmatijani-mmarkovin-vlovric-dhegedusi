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
    /// Interaction logic for UcArchiveBook.xaml
    /// </summary>
    public partial class UcArchiveBook : UserControl
    {
        public UcArchiveBook()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as EmployeePanel).contentPanel.Content = new UcCatalogueOptions();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(dgvBookNamesArchive.SelectedItem == null)
            {
                MessageBox.Show("Morate odabrati knjigu!");
                return;
            }

            Book book = dgvBookNamesArchive.SelectedItem as Book;
            BookServices bookServices = new BookServices();
            EmployeeService employeeServices = new EmployeeService();
            var archive = new Archive
            {
                Book_id = book.id,
                Employee_id = employeeServices.GetEmployeeId(LoggedUser.Username),
                arhive_date = DateTime.Now,
            };

            if(bookServices.ArchiveBook(book, archive))
            {
                MessageBox.Show("Uspješno!");
                LoadDgv();
            }
            else
            {
                MessageBox.Show("Neuspješno!");
            }

            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDgv();
        }

        private void LoadDgv()
        {
            BookServices services = new BookServices();
            dgvBookNamesArchive.ItemsSource = services.GetNonArchivedBooks();
            foreach (var column in dgvBookNamesArchive.Columns)
            {
                if (column.Header.ToString() != "name")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void txtBookName_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookServices bookServices = new BookServices();
            string text = txtBookName.Text;
            dgvBookNamesArchive.ItemsSource= bookServices.GetNonArchivedBooksByName(text);
            foreach (var column in dgvBookNamesArchive.Columns)
            {
                if (column.Header.ToString() != "name")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
