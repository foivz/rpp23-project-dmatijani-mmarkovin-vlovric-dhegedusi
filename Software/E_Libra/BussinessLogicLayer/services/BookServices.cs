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
    }
}
