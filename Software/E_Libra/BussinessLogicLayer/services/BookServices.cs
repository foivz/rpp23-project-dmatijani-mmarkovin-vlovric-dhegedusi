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

        public List<Book> GetNonArchivedBooks(bool digital)
        {
            using(var repo = new BookRepository())
            {
                return repo.GetNonArchivedBooks(digital).ToList();
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
        
        public List<BookViewModel> SearchBooks(string searchTerm, bool digital)
        {
            using (var repo = new BookRepository())
            {
                return repo.SearchBooks(searchTerm, digital).ToList();
            }
        }
        public List<BookViewModel> GetBooksByGenre(string genreName, bool digital)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooksByGenre(genreName, digital).ToList();
            }
        }
        public List<BookViewModel> GetBooksByAuthor(string authorName, bool digital)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooksByAuthor(authorName, digital).ToList();
            }
        }
        public List<BookViewModel> GetBooksByYear(int year, bool digital)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooksByYear(year, digital).ToList();
            }
        }
        public Book GetBookById(int id)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBookById(id);
            }
        }
        public List<BookViewModel> GetWishlistedBooks()
        {
            using(var repo = new BookRepository())
            {
                return repo.GetWishlistBooksForMember(LoggedUser.Username).ToList();
            }
        }
        public bool AddBookToWishlist(int bookId)
        {
            MemberRepository memberRepository = new MemberRepository();
            int userId = memberRepository.GetMemberId(LoggedUser.Username);

            using(var repo = new BookRepository())
            {
                return repo.AddBookToWishlist(userId, bookId);
            }
        }
        public bool RemoveBookFromWishlist(int bookId)
        {
            MemberRepository memberRepository = new MemberRepository();
            int userId = memberRepository.GetMemberId(LoggedUser.Username);

            using (var repo = new BookRepository())
            {
                return repo.RemoveBookFromWishlist(userId, bookId);
            }
        }

    }
}
