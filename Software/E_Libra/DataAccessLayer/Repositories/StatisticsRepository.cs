using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class StatisticsRepository : IDisposable {
        public StatisticsRepository() {
            
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public IQueryable<MostPopularBooks> GetMostPopularBooks() {
            using (var repo = new BookRepository()) {
                return repo.GetMostPopularBooks();
            }
        }

    }
}
