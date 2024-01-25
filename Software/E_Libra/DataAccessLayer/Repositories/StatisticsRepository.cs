using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class StatisticsRepository: Repository<Book> {
        public StatisticsRepository() : base(new DatabaseModel()) {
        }
        public override int Update(Book entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }

        public List<Book> GetMostPopularBooks() {
            var books = from b in Entities
                        orderby b.Borrows.Count descending
                        select b;

            return books.ToList();
        }


    }
}
