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

        public override IQueryable<Genre> GetAll()
        {
            var query = from g in Entities select g;
            return query;
        }
        public override int Add(Genre entity, bool saveChanges = true)
        {
            var genre = new Genre
            {
                name = entity.name,
            };
            Entities.Add(genre);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
