using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class BorrowService {
        public List<Borrow> GetAllBorrowsForLibrary(Library library) {
            using (var context = new BorrowRepository()) {
                return context.GetAllBorrowsForLibrary(library).ToList();
            }
        }

        public List<Borrow> GetAllBorrowsForLibrary(int library_id) {
            using (var context = new BorrowRepository()) {
                return context.GetAllBorrowsForLibrary(library_id).ToList();
            }
        }

        public List<Borrow> GetBorrowsForLibraryByStatus(Library library, BorrowStatus status) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForLibraryByStatus(library, status).ToList();
            }
        }

        public List<Borrow> GetBorrowsForLibraryByStatus(int library_id, BorrowStatus status) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForLibraryByStatus(library_id, status).ToList();
            }
        }
    }
}
