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

        public IQueryable<Member> GetMembersByUsername(string username) {
            var query = from e in Entities
                        where e.username == username
                        select e;

            return query;
        }

        public IQueryable<Member> GetMembersByLibrary(int libraryID) {
            var query = from e in Entities
                        where e.Library_id == libraryID
                        select e;

            return query;
        }
        public int GetMemberLibraryId(string username) {
            var sql = (from m in Entities.Include("Library") where m.username == username select m.Library_id).FirstOrDefault();
            return sql;
        }

        public IQueryable<Member> GetMemberByBarcodeId(string barcodeId) {
            var query = from m in Member.Include("Library")
                        where m.barcode_id == barcodeId
                        select m;

            return query;
        }
    }
}
