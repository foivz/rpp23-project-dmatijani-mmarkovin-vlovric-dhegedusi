using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MemberRepository : Repository<Member>
    {
        public MemberRepository(): base(new DatabaseModel())
        {
                
        }
        public override int Update(Member entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
