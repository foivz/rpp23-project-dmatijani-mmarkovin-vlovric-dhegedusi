using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class StatisticsService {
        public List<Book> GetMostPopularBooks() {
            using (var repo = new StatisticsRepository()) {
                return repo.GetMostPopularBooks();
            }
        }
    }
}
