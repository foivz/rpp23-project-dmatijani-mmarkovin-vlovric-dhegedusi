using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    public class EmployeeRepository : Repository<Employee> {
        public EmployeeRepository() : base(new DatabaseModel()) {

        }

        public IQueryable<Employee> GetEmployeesByLibrary(Library library) {
            var query = from e in Entities.Include("Library")
                        where e.Library_id == library.id
                        select e;

            return query;
        }

        public IQueryable<Employee> GetEmployeesById(int employeeId) {
            var query = from e in Entities.Include("Library")
                        where e.id == employeeId
                        select e;

            return query;
        }

        public IQueryable<Employee> GetEmployeesByOIB(string employeeOIB) {
            var query = from e in Entities.Include("Library")
                        where e.OIB == employeeOIB
                        select e;

            return query;
        }

        public override int Add(Employee employee, bool saveChanges = true) {
            var library = Context.Libraries.SingleOrDefault(l => l.id == employee.Library.id);

            var newEmployee = new Employee {
                id = employee.id,
                name = employee.name,
                surname = employee.surname,
                username = employee.username,
                password = employee.password,
                OIB = employee.OIB,
                Library = library
            };

            Entities.Add(newEmployee);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public override int Update(Employee employee, bool saveChanges = true) {
            var library = Context.Libraries.SingleOrDefault(l => l.id == employee.Library.id);

            var existingEmployee = Entities.SingleOrDefault(e => e.id == employee.id);
            existingEmployee.name = employee.name;
            existingEmployee.surname = employee.surname;
            existingEmployee.username = employee.username;
            existingEmployee.password = employee.password;
            existingEmployee.OIB = employee.OIB;
            existingEmployee.Library = library;

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }
    }
}
