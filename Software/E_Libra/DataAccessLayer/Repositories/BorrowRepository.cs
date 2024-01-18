using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class BorrowRepository : Repository<Borrow> {
        public BorrowRepository() : base(new DatabaseModel()) {

        }

        public IQueryable<Borrow> GetAllBorrowsForLibrary(Library library) {
            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library.id
                        select b;

            return query;
        }

        public IQueryable<Borrow> GetAllBorrowsForLibrary(int library_id) {
            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library_id
                        select b;

            return query;
        }

        public IQueryable<Borrow> GetBorrowsForLibraryByStatus(Library library, BorrowStatus status) {
            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library.id && b.borrow_status == (int)status
                        select b;

            return query;
        }

        public IQueryable<Borrow> GetBorrowsForLibraryByStatus(int library_id, BorrowStatus status) {
            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library_id && b.borrow_status == (int)status
                        select b;

            return query;
        }

        public override int Update(Borrow entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }
    }
}
