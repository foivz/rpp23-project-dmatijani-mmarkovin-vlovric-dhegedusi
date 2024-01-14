using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class EmployeeService {
        public List<Employee> GetEmployeesByLibrary(Library library) {
            using (var repository = new EmployeeRepository()) {
                return repository.GetEmployeesByLibrary(library).ToList();
            }
        }
    }
}
