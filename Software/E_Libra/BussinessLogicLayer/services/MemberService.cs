using BussinessLogicLayer.Exceptions;
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
                    LoggedUser.LibraryId = returned[0].Library_id;
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

        public Member GetMemberByBarcodeId(int libraryId, string barcodeId) {
            using (var repository = new MemberRepository()) {
                List<Member> returned = repository.GetMemberByBarcodeId(barcodeId).ToList();

                if (returned.Count == 0) {
                    throw new MemberNotFoundException("Član knjižnice sa tim barkodom ne postoji!");
                }

                Member member = returned.FirstOrDefault();

                if (member.Library.id != libraryId) {
                    throw new WrongLibraryException("Član ove knjižnice s tim barkodom ne postoji!");
                }

                return member;
            }
        }
    }
}
