using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class LibraryRepository : Repository<Library> {
        public LibraryRepository(DatabaseModel context) : base(context) {

        }

        public override int Update(Library entity, bool saveChanges = true) {
            return 0; // TODO
        }
    }
}
