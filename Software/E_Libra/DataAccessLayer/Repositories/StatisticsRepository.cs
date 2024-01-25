using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class StatisticsRepository : IDisposable {
        protected DatabaseModel Context { get; set; }
        public StatisticsRepository() {
            
        }
        public IQueryable<MostPopularBooks> GetMostPopularBooks() {
            using (var repo = new BookRepository()) {
                return repo.GetMostPopularBooks();
            }
        }

        public void Dispose() {
            Context.Dispose();
        }

    }
}
