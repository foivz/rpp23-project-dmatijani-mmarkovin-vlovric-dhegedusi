using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    public class MemberService
    {
        public void CheckLoginCredentials(string username, string password)
        {
            using (var memberRepo = new MemberRepository())
            {
                var returned = memberRepo.GetMemberLogin(username, password).ToList();

                if (returned.Count() == 1)
                {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Member;
                }
            }
        }

        public int GetMemberId(string username) {
            using (var repository = new MemberRepository()) {
                return repository.GetMemberId(username).FirstOrDefault();
            }
        }

        public int GetMemberLibraryId(string username) {
            using (var repository = new MemberRepository()) {
                return repository.GetMemberLibraryId(username).FirstOrDefault();
            }
        }
    }
}
