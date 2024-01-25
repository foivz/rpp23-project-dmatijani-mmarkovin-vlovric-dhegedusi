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
using EntitiesLayer.Entities;

namespace PresentationLayer {
    public partial class ucReviewsList : UserControl {
        private ReviewService services = new ReviewService();
        MemberService memberService = new MemberService();
        private int bookId;

        public ucReviewsList(int book_id) {
            InitializeComponent();
            bookId = book_id;
            LoadReviews();
        }

        private void LoadReviews() {
            if (bookId <= 0) {
                dgReviews.ItemsSource = new List<ReviewInfo>();
                return;
            }

            Task.Run(() => {
                List<ReviewInfo> reviews = services.GetReviewsForBook(bookId);

                Application.Current.Dispatcher.Invoke(() => {
                    dgReviews.ItemsSource = reviews;
                });
            });
        }

        private void btnRemoveReview_Click(object sender, RoutedEventArgs e) {
            int memberId = memberService.GetMemberId(LoggedUser.Username);

            services.DeleteReview(memberId, bookId);
            LoadReviews();
            MessageBox.Show("Vaša recenzija je uspješno obrisana.");
        }

        private void btnAddReview_Click(object sender, RoutedEventArgs e) {


            int memberId = memberService.GetMemberId(LoggedUser.Username);

            if (services.HasUserReviewedBook(memberId, bookId)) {
                MessageBox.Show("Već si napisao recenziju za ovu knjigu!");
            } 
            else {
                UcNewReview ucNewReview = new UcNewReview(bookId);
                (Window.GetWindow(this) as MemberPanel).contentPanel.Content = ucNewReview;
            }
        }

    }

}