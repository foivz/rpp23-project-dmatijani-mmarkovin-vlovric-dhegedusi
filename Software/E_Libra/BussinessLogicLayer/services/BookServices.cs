using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.Repositories.BookRepository;

namespace BussinessLogicLayer.services
{
    public class BookServices
    {
        public bool AddBook(Book book, Author author)
        {
            bool isSuccesful = false;
            using(var repo = new BookRepository())
            {
                int affectedRows = repo.Add(book, author);
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
        
        public List<BookViewModel> SearchBooks(string searchTerm)
        {
            using (var repo = new BookRepository())
            {
                return repo.SearchBooks(searchTerm).ToList();
            }
        }
        public List<BookViewModel> GetBooksByGenre(string genreName)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooksByGenre(genreName).ToList();
            }
        }
        public List<BookViewModel> GetBooksByAuthor(string authorName)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooksByAuthor(authorName).ToList();
            }
        }
        public List<BookViewModel> GetBooksByYear(int year)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooksByYear(year).ToList();
            }
        }
    }
}
