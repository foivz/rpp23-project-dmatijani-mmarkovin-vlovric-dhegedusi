using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    public class BookServices
    {
        public bool AddBook(Book book)
        {
            bool isSuccesful = false;
            using(var repo = new BookRepository())
            {
                int affectedRows = repo.Add(book);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }
        public List<Book> GetAllBooks()
        {
            using (var repo = new BookRepository())
            {
                return repo.GetAll().ToList();
            }
        }

        public List<Book> GetNonArchivedBooks()
        {
            using(var repo = new BookRepository())
            {
                return repo.GetNonArchivedBooks().ToList();
            }
        }
        public bool InsertNewCopies(int number, Book book)
        {
            bool isSuccesful = false;
            using (var repo = new BookRepository())
            {
                int affectedRows = repo.InsertNewCopies(number, book);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }
        public bool ArchiveBook(Book book, Archive archive)
        {
            bool isSuccesful = false;
            using(var repo = new BookRepository())
            {
                int affectedRows = repo.ArhiveBook(book, archive);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }
        public List<Book> GetNonArchivedBooksByName(string searchTerm)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetNonArchivedBooksByName(searchTerm).ToList();
            }
        }
        
        public Book GetBookByBarcodeId(int libraryId, string barcodeId) {
            using (var repository = new BookRepository()) {
                List<Book> returned = repository.GetBookByBarcodeId(barcodeId).ToList();

                if (returned.Count == 0) {
                    throw new BookNotFoundException("Knjiga s tim barkodom ne postoji!");
                }

                Book book = returned.FirstOrDefault();

                if (book.Library.id != libraryId) {
                    throw new WrongLibraryException("Knjiga s tim barkodom ovdje ne postoji!");
                }

                return book;
            }
        }

        public int UpdateBook(Book book, bool saveChanges = true) {
            using (var context = new BookRepository()) {
                return context.Update(book, saveChanges);
            }
        }
    }
}
