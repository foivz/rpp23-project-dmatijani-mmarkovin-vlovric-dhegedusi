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

        public override int Update(Book entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public override int Add(Book entity, bool saveChanges = true)
        {
            return base.Add(entity, saveChanges);
        }
    }
}
