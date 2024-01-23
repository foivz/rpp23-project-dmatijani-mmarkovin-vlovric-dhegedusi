using EntitiesLayer;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories;
using System;

namespace BussinessLogicLayer.services {
    public class ReviewService {
        public List<Review> GetReviewsForBook(int book_id) {
            using (var repo = new ReviewRepository()) {
                return repo.GetReviewsForBook(book_id).ToList();
            }
        }

        public int AddReview(Review newReview) {
            using (var repo = new ReviewRepository()) {
                return repo.Add(newReview);
            }
        }

        public int DeleteReview(Review review) {
            using (var repo = new ReviewRepository()) {
                return repo.Remove(review);
            }
        }

        public bool HasUserReviewedBook(int memberId, int bookId) {
                List<Review> userReviews = GetReviewsForMemberAndBook(memberId, bookId);
                return userReviews.Any();
            }

            private List<Review> GetReviewsForMemberAndBook(int memberId, int bookId) {
                using (var repo = new ReviewRepository()) {
                    return repo.GetReviewsForMemberAndBook(memberId, bookId);
                }
            }
        }

    }

