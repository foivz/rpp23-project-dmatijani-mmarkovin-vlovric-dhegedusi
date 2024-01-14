using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class LibraryRepository : Repository<Library> {
        public LibraryRepository() : base(new DatabaseModel()) {

        }

        public IQueryable<Library> GetLibrariesById(int libraryId) {
            var query = from l in Entities
                        where l.id == libraryId
                        select l;

            return query;
        }

        public IQueryable<Library> GetLibrariesByName(string libraryName) {
            var query = from l in Entities
                        where l.name == libraryName
                        select l;

            return query;
        }

        public IQueryable<Library> GetLibrariesByOIB(string libraryOIB) {
            var query = from l in Entities
                        where l.OIB == libraryOIB
                        select l;

            return query;
        }

        public override int Update(Library entity, bool saveChanges = true) {
            return 0; // TODO
        }
    }
}
