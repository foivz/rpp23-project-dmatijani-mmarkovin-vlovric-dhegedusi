using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AdministratorRepository: Repository<Administrator>
    {
        public AdministratorRepository() :base(new DatabaseModel())
        {
            
        }

        public override int Update(Administrator entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
