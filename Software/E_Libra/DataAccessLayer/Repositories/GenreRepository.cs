using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenreRepository: Repository<Genre>
    {
        public GenreRepository(): base(new DatabaseModel())
        {
            
        }

        public override int Update(Genre entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }


    }
}
