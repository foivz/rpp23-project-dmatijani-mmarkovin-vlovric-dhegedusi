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

namespace PresentationLayer {
    public partial class ucReviewsList : UserControl {
        private ReviewService services = new ReviewService();


        public ucReviewsList() {
            InitializeComponent();
        }

        private void LoadReviews_Click(object sender, RoutedEventArgs e) {

            int bookId = 1;
            var allreviews = services.GetReviewsForBook(bookId);
            dgReviews.ItemsSource = allreviews;
        }

        private void btnRemoveReview_Click(object sender, RoutedEventArgs e) {

        }

        private void cboBook_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Book selectedBook = cboBook.SelectedItem as Book;
        }
    }
}

