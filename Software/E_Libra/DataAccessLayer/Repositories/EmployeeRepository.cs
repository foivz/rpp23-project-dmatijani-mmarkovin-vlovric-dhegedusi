using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : Repository<Employee>
    {
        public DbSet<Employee> Employee { get; set; }
        public EmployeeRepository() :base(new DatabaseModel())
        {
            Employee = Context.Set<Employee>();
        }
        public IQueryable<Employee> GetEmployeeLogin(string username, string password)
        {
            var sql = from e in Employee
                      where e.username == username && e.password == password
                      select e;
            return sql;
        }
        public override int Update(Employee entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public int GetEmployeeLibraryId(string username)
        {
            var sql = (from e in Employee where e.username == username select e.Library_id).FirstOrDefault();
            return sql;
        }
    }
}
