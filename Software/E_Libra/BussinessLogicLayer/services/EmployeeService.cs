using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    public class EmployeeService
    {
        public void CheckLoginCredentials(string username, string password)
        {
            using (var employeeRepo = new EmployeeRepository())
            {
                var returned = employeeRepo.GetEmployeeLogin(username, password).ToList();

                if (returned.Count() == 1)
                {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Employee;
                }
            }
        }
        public int GetEmployeeLibraryId(string username)
        {
            using(var employeeRepo = new EmployeeRepository())
            {
                return employeeRepo.GetEmployeeLibraryId(username);
            }
        }
    }
}
