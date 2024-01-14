using BussinessLogicLayer.Exceptions;
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

        public int AddLibrary(Library newLibrary) {
            using (var repository = new LibraryRepository()) {
                var librariesWithId = repository.GetLibrariesById(newLibrary.id);
                if (librariesWithId.ToList().Count > 0) {
                    throw new LibraryWithSameIDException("Knjižnica sa istim ID već postoji!");
                }

                var librariesWithOIB = repository.GetLibrariesByOIB(newLibrary.OIB);
                if (librariesWithOIB.ToList().Count > 0) {
                    throw new LibraryWithSameOIBException("Knjižnica sa istim OIB već postoji!");
                }

                return repository.Add(newLibrary);
            }
        }
    }
}
