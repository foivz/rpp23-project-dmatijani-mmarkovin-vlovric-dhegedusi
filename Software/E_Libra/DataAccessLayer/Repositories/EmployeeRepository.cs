using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository() :base(new DatabaseModel())
        {
            
        }
        public override int Update(Employee entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
