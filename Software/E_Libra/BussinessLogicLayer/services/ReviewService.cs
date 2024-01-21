using EntitiesLayer;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories;

namespace BussinessLogicLayer.services {
    public class ReviewService {
        public List<Review> GetReviewsForBook(Book selectedBook) {
            using (var repo = new ReviewRepository()) {
                return repo.GetAll().ToList();
            }
        }
    }
}
