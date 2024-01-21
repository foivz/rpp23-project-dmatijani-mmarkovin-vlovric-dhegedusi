using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(): base(new DatabaseModel())
        {
            
        }

        public override int Update(Book entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public int Add(Book entity, Author selectedAuthor, bool saveChanges = true)
        {
            var genre = Context.Genres.FirstOrDefault(g => g.id == entity.Genre.id);
            var library = Context.Libraries.FirstOrDefault(l => l.id == entity.Library_id);

            var book = new Book
            {
                name = entity.name,
                description = entity.description,
                publish_date = entity.publish_date,
                pages_num = entity.pages_num,
                digital = entity.digital,
                url_digital = entity.url_digital,
                url_photo = entity.url_photo,
                total_copies = entity.total_copies,
                current_copies = entity.total_copies,
                barcode_id = GenerateBarcodeId(),
                Genre = genre,
                Library = library,
            };
            Context.Entry(book).State = EntityState.Added;

            // Attach the selectedAuthor entity to the context
            Context.Entry(selectedAuthor).State = EntityState.Unchanged;

            Entities.Add(book);
            book.Authors.Add(selectedAuthor);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }

        }

        private string GenerateBarcodeId()
        {
            var rand = new Random();
            string res;
            do
            {
                res = rand.Next(10000000, 99999999).ToString();
            } while (BarcodeExists(res));
            return res;

        }

        public bool BarcodeExists(string barcode)
        {
            var sql = from b in Entities where b.barcode_id == barcode select b;
            return sql.Count() > 0;
        }

        public override IQueryable<Book> GetAll()
        {
            var sql = from b in Entities select b;
            return sql;
        }

        public IQueryable<Book> GetNonArchivedBooks()
        {
            var sql = from b in Context.Books
                      where !Context.Archives.Any(a => a.Book_id == b.id)
                      select b;
            return sql;
        }

       

        public int InsertNewCopies(int number, Book passedBook, bool saveChanges = true)
        {
            string name = passedBook.name;
            var book = (from b in Entities where b.name == name select b).FirstOrDefault();
            book.total_copies += number;
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public int ArhiveBook(Book passedBook, Archive archive)
        {
            string name = passedBook.name;
            var book = (from b in Entities where b.name == name select b).FirstOrDefault();
            book.Archives.Add(archive);
            return SaveChanges();
        }

        public IQueryable<Book> GetNonArchivedBooksByName(string searchTerm)
        {
            var nonArchivedBooks = from book in GetNonArchivedBooks()
                                   where book.name.Contains(searchTerm)
                                   select book;

            return nonArchivedBooks;
        }
        public IQueryable<Book> SearchBooks(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            var matchingBooks = from book in GetNonArchivedBooks()
                                where book.name.ToLower().Contains(searchTerm) ||
                                      book.Authors.Any(author => author.name.ToLower().Contains(searchTerm) || author.surname.ToLower().Contains(searchTerm)) ||
                                      book.Genre.name.ToLower().Contains(searchTerm) ||
                                      (book.publish_date != null && book.publish_date.Value.Year.ToString().Contains(searchTerm))
                                select book;

            return matchingBooks;
        }
        public IQueryable<Book> GetBooksByGenre(string genreName)
        {
            genreName = genreName.ToLower();

            var booksByGenre = from book in GetNonArchivedBooks()
                               where book.Genre.name.ToLower().Contains(genreName)
                               select book;

            return booksByGenre;
        }

        public IQueryable<Book> GetBooksByAuthor(string authorName)
        {
            authorName = authorName.ToLower();

            var booksByAuthor = from book in GetNonArchivedBooks()
                                where book.Authors.Any(author => author.name.ToLower().Contains(authorName) || author.surname.ToLower().Contains(authorName))
                                select book;

            return booksByAuthor;
        }
        public IQueryable<Book> GetBooksByYear(int publicationYear)
        {
            var booksByYear = from book in GetNonArchivedBooks()
                              where book.publish_date != null && book.publish_date.Value.Year == publicationYear
                              select book;

            return booksByYear;
        }


    }
}
