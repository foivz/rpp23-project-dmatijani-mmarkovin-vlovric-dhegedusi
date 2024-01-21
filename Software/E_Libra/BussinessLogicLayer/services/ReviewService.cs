using EntitiesLayer;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories;

namespace BussinessLogicLayer.services {
    public class ReviewService {
        public List<Review> GetReviewsForBook(int book_id) {
            using (var repo = new ReviewRepository()) {
                return repo.GetReviewsForBook(book_id).ToList();
            }
        }
    }
}
