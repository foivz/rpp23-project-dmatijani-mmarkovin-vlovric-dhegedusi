using BussinessLogicLayer.services;
using DataAccessLayer.Repositories;
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
using static DataAccessLayer.Repositories.BookRepository;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for UcBookDetails.xaml
    /// </summary>
    public partial class UcBookDetails : UserControl
    {
        BookServices bookServices = new BookServices();
        private UcBookSearchFilter prevForm;

        public UcBookSearchFilter PrevForm
        {
            set { prevForm = value; }
        }
        Book book;
        BookViewModel bookUI;
        public UcBookDetails(BookViewModel passedBook)
        {
            InitializeComponent();
            book = bookServices.GetBookById(passedBook.Id);
            bookUI = passedBook;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = prevForm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MakeImage(book.url_photo);
            tblName.Text = bookUI.Name;
            tblAuthor.Text = bookUI.AuthorName;
            tblDescription.Text = book.description;
            tblGenre.Text = bookUI.GenreName;
            tblDate.Text = bookUI.PublishDate.ToString();
            tblPageNum.Text = book.pages_num.ToString();
            
            if (book.current_copies > 0)
            {
                tblAvailable.Text = "Da, broj raspoloživih primjeraka je: " + book.current_copies;
            }
            else
            {
                tblAvailable.Text = "Ne";
            }
            
            

        }

        private void MakeImage(string url)
        {
            if(!string.IsNullOrEmpty(book.url_photo))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                try
                {
                    bitmapImage.UriSource = new Uri(url, UriKind.Absolute);
                    bitmapImage.EndInit();
                    imgBook.Source = bitmapImage;
                }
                catch (Exception)
                {
                    imgBook_ImageFailed(imgBook, null);
                }
                
            }
            else
            {
                imgBook_ImageFailed(imgBook, null);
            }
            
        }

        private void imgBook_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png"));
        }

        private void btnSaveReadList_Click(object sender, RoutedEventArgs e)
        {
            if (bookServices.AddBookToWishlist(book.id))
            {
                MessageBox.Show("Uspješno dodana knjiga na popis Želim pročitati!");
                return;
            }
            else
            {
                MessageBox.Show("Knjiga Vam je već na popisu Želim pročitati!");
                return;
            }
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e) {
            ucReviewsList ucReviewList = new ucReviewsList(book.id);
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucReviewList;

        }

    }
}
