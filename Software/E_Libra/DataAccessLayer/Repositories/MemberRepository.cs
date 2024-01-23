using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MemberRepository : Repository<Member>
    {
        public DbSet<Member> Member { get; set; }
        public MemberRepository(): base(new DatabaseModel())
        {
                Member = Context.Set<Member>();
        }
        public IQueryable<Member> GetMemberLogin(string username, string password)
        {
            var sql = from m in Member
                      where m.username == username && m.password == password
                      select m;
            return sql;
        }
        public override int Update(Member entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public int GetMemberId(string username)
        {
            var sql = from m in Member where m.username == username select m.id;
            return sql.FirstOrDefault();
        }

        public IQueryable<string> GetMemberNameSurname(int memberId) {
            var sql = from m in Member
                      where m.id == memberId
                      select $"{m.name} {m.surname}";
            return sql;
        }
    }
}
