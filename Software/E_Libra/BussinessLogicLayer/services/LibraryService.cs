using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class LibraryService {
        public List<Library> GetAllLibraries() {
            using (var repository = new LibraryRepository()) {
                return repository.GetAll().ToList();
            }
        }
    }
}
