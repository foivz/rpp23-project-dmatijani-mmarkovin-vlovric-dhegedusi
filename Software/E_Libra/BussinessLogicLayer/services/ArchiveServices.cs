using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    //Viktor Lovrić
    public class ArchiveServices
    {
        public List<ArchivedBookInfo> GetArchive()
        {
            using (var repo = new ArchiveRepository())
            {
                return repo.GetArchive().ToList();
            }
        }
    }
}
