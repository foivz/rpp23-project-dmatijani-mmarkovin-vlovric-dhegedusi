using BussinessLogicLayer.Exceptions;
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

        public int AddNewBorrow(Borrow borrow) {
            Book book = borrow.Book;
            if (borrow.borrow_status == (int)BorrowStatus.Borrowed) {
                if (book.current_copies < 1) {
                    throw new NoMoreBookCopiesException("Odabrane knjige trenutno nema na stanju!");
                }
                BookServices bookService = new BookServices();
                book.current_copies--;
                bookService.UpdateBook(book);
            }

            using (var context = new BorrowRepository()) {
                return context.Add(borrow);
            }
        }

        public int UpdateBorrow(Borrow borrow) {
            Book book = borrow.Book;
            if (borrow.borrow_status == (int)BorrowStatus.Borrowed) {
                if (book.current_copies < 1) {
                    throw new NoMoreBookCopiesException("Odabrane knjige trenutno nema na stanju!");
                }
                BookServices bookService = new BookServices();
                book.current_copies--;
                bookService.UpdateBook(book);
            } else if (borrow.borrow_status == (int)BorrowStatus.Returned) {
                BookServices bookService = new BookServices();
                book.current_copies++;
                bookService.UpdateBook(book);
            }

            using (var context = new BorrowRepository()) {
                return context.Update(borrow);
            }
        }
    }
}
