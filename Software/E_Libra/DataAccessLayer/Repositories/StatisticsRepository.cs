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
            Context = new DatabaseModel();
        }
        public List<MostPopularBooks> GetMostPopularBooks(int Library_id) {
            using (var repo = new BookRepository()) {
                return repo.GetMostPopularBooks(Library_id).ToList();
            }
        }

        public List<MostPopularGenres> GetMostPopularGenres(int Library_id) {
            using (var repo = new GenreRepository()) {
                return repo.GetMostPopularGenres(Library_id).ToList();
            }
        }

        public void Dispose() {
            if (Context != null) {
                Context.Dispose();
            }
        }

    }
}
