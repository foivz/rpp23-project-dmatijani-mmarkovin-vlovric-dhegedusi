using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class BorrowService {
        public List<Borrow> GetAllBorrowsForMember(int member_id, int library_id) {
            using (var context = new BorrowRepository()) {
                return context.GetAllBorrowsForMember(member_id, library_id).ToList();
            }
        }

        public List<Borrow> GetBorrowsForMemberByStatus(int member_id, int library_id, BorrowStatus status) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForMemberByStatus(member_id, library_id, status).ToList();
            }
        }

        public List<Borrow> GetBorrowsForMemberAndBook(int member_id, int book_id, int library_id) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForMemberAndBook(member_id, book_id, library_id).ToList();
            }
        }

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
