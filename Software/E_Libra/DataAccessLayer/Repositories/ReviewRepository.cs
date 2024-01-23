using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class ReviewRepository : Repository<Review> {
        public ReviewRepository() : base(new DatabaseModel()) {
        }

        public IQueryable<Review> GetReviewsForBook(int bookId) {
            var query = from r in Entities
                        where r.Book_id == bookId
                        select r;

            return query;
        }

        public override int Add(Review review, bool saveChanges = true) {

            var newReview = new Review() {
                Member_id = review.Member_id,
                Book_id = review.Book_id,
                comment = review.comment,
                rating = review.rating,
                date = review.date
            };

            Entities.Add(newReview);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }
        public override int Update(Review review, bool saveChanges = true) {
            var existingReview = Entities.SingleOrDefault(r => r.Member_id == review.Member_id && r.Book_id == review.Book_id);

            if (existingReview != null) {
                existingReview.rating = review.rating;
                existingReview.comment = review.comment;
                existingReview.date = review.date;

                if (saveChanges) {
                    return SaveChanges();
                }
            }

            return 0;
        }
    }
}
