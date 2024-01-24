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
            CheckIfDigital();
            ShowReserve();
        }

        private void ShowReserve()
        {
            if(book.current_copies > 0)
            {
                btnReserve.Visibility = Visibility.Collapsed;
            }
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
            tblDate.Text = bookUI.PublishDate.HasValue ? bookUI.PublishDate.Value.Date.ToString() : "Nepoznato";

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

        private void CheckIfDigital() {
            Console.WriteLine(book.digital);
            Console.WriteLine(book.id);
            if (book.digital == 1) {
                CreateDigitalButton();
                HideAvailable();
                HideReserve();
            } else {
                return;
            }
        }

        private void HideReserve()
        {
            btnReserve.Visibility = Visibility.Collapsed;
        }

        private void HideAvailable()
        {
            tblAvailable.Visibility = Visibility.Collapsed;
            lblAvailable.Visibility = Visibility.Collapsed;
        }

        private void CreateDigitalButton() {

            Button dynamicButton = new Button();
            dynamicButton.Content = "Otvori digitalnu verziju";
            dynamicButton.Width = 150;
            dynamicButton.Height = 40;
            dynamicButton.Margin = new Thickness(0, 0, 10, 0);
            dynamicButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#637E60");
            dynamicButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEFE8");

            dynamicButton.Click += DigitalButton_Click;
            ButtonStackPanel.Children.Add(dynamicButton);

        }

        private void DigitalButton_Click(object sender, RoutedEventArgs e) {
            string online_path = book.url_digital;
            UcDigitalBook ucDigitalBook = new UcDigitalBook(online_path);
            (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucDigitalBook;
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            int position = 0;
            string text = "Vi ste " + position + ". na redu čekanja. Potvrdite ili odbijte rezervaciju.";
            WinAcceptDecline winAcceptDecline = new WinAcceptDecline(text);
            winAcceptDecline.Show();
            if (winAcceptDecline.UserClickedAccept)
            {
                //kod
            }
            else
            {
                //kod
            }
        }
    }
}
