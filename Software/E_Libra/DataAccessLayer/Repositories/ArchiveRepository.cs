using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ArchiveRepository : Repository<Archive>
    {
        public ArchiveRepository(): base(new DatabaseModel())
        {
            
        }

        public override int Update(Archive entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ArchivedBookInfo> GetArchive()
        {
            var query = from archive in Entities
                        join book in Context.Books on archive.Book_id equals book.id
                        join employee in Context.Employees on archive.Employee_id equals employee.id
                        select new ArchivedBookInfo
                        {
                            BookName = book.name,
                            EmployeeName = employee.name + " " + employee.surname,
                            ArchiveDate = archive.arhive_date
                        };
            return query;
        }
    }
    public class ArchivedBookInfo
    {
        public string BookName { get; set;}
        public string EmployeeName { get; set;}
        public DateTime ArchiveDate { get; set;}
    }
}
