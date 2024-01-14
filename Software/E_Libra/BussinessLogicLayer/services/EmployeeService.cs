using BussinessLogicLayer.Exceptions;
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

        public int AddEmployee(Employee newEmployee) {
            using (var repository = new EmployeeRepository()) {
                var employeesWithId = repository.GetEmployeesById(newEmployee.id);
                if (employeesWithId.ToList().Count > 0) {
                    throw new EmployeeWithSameIDException("Zaposlenik sa istim ID već postoji!");
                }

                var employeesWithOIB = repository.GetEmployeesByOIB(newEmployee.OIB);
                if (employeesWithOIB.ToList().Count > 0) {
                    throw new EmployeeWithSameOIBException("Zaposlenik sa istim OIB već postoji!");
                }

                return repository.Add(newEmployee);
            }
        }

        public int UpdateEmployee(Employee employee) {
            using (var repository = new EmployeeRepository()) {
                var employeesWithId = repository.GetEmployeesById(employee.id);
                if (employeesWithId.ToList().Count == 0) {
                    throw new EmployeeWithSameIDException("Zaposlenik sa tim ID ne postoji!");
                }

                var employeesWithOIB = repository.GetEmployeesByOIB(employee.OIB);
                List<Employee> otherEmployeesWithOIB = employeesWithOIB.ToList().FindAll(e => e.id != employee.id);
                if (employeesWithOIB.ToList().Count > 0) {
                    throw new EmployeeWithSameOIBException("Drugi zaposlenik već ima taj OIB!");
                }

                return repository.Add(employee);
            }
        }
    }
}
