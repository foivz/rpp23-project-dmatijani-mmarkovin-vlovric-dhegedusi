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
using BussinessLogicLayer.services;
using PresentationLayer.AdminPanels;

namespace PresentationLayer {
    public partial class ucReviewsList : UserControl {
        private ReviewService services = new ReviewService();


        public ucReviewsList(int book_id) {
            InitializeComponent();

        }

      
        private void LoadReviews_Click(Book selectedBook) {
            if (selectedBook == null) {
                dgReviews.ItemsSource = new List<Book>();
                return;
            }

            Task.Run(() => {
                List<Review> reviews = services.GetReviewsForBook(selectedBook);

                Application.Current.Dispatcher.Invoke(() => {
                    dgReviews.ItemsSource = reviews;
                });
            });

        }

        private void btnRemoveReview_Click(object sender, RoutedEventArgs e) {

        }

        private void cboBook_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Book selectedBook = cboBook.SelectedItem as Book;
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e) {

        }
    }
}

