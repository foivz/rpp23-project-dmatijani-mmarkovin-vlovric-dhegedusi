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

        public IQueryable<int> GetMemberId(string username) {
            var query = from m in Member
                        where m.username == username
                        select m.id;

            return query;
        }

        public IQueryable<int> GetMemberLibraryId(string username) {
            var query = from m in Member.Include("Library")
                        where m.username == username
                        select m.Library.id;

            return query;
        }

        public IQueryable<Member> GetMemberByBarcodeId(string barcodeId) {
            var query = from m in Member
                        where m.barcode_id == barcodeId
                        select m;

            return query;
        }

        public override int Update(Member entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
