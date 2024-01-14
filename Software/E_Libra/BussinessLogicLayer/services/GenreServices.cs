using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    public class GenreServices
    {
        public List<Genre> GetGenres()
        {
            using (var repo = new GenreRepository())
            {
                return repo.GetAll().ToList();
            }
        }
    }
}
