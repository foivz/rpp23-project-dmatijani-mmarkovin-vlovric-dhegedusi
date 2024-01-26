using EntitiesLayer;
using System;
using System.Collections.Generic;
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

        public override int Add(Book entity, bool saveChanges = true)
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
            Entities.Add(book);
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

        public IQueryable<Book> GetBookByBarcodeId(string barcodeId) {
            var query = from b in Entities.Include("Library")
                        where b.barcode_id == barcodeId
                        select b;

            return query;
        }

        public override int Update(Book book, bool saveChanges = true) {
            var library = Context.Libraries.SingleOrDefault(l => l.id == book.Library.id);
            var genre = Context.Genres.SingleOrDefault(g => g.id == book.Genre.id);

            Book existingBook = Context.Books.SingleOrDefault(b => b.id == book.id);
            existingBook.name = book.name;
            existingBook.description = book.description;
            existingBook.publish_date = book.publish_date;
            existingBook.pages_num = book.pages_num;
            existingBook.digital = book.digital;
            existingBook.url_photo = book.url_photo;
            existingBook.url_digital = book.url_digital;
            existingBook.barcode_id = book.barcode_id;
            existingBook.total_copies = book.total_copies;
            existingBook.Genre = genre;
            existingBook.Library = library;

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }
    }
}
