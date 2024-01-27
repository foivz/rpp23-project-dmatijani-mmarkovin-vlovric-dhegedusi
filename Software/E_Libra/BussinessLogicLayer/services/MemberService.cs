using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class MemberService {
        EmployeeService employeeService = new EmployeeService();
        public void CheckLoginCredentials(string username, string password) {
            using (var memberRepo = new MemberRepository()) {
                var returned = memberRepo.GetMemberLogin(username, password).ToList();

                if (returned.Count() == 1) {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Member;
                }
            }
        }
        public bool AddNewMember(Member member)
        {
            using (var memberRepo = new MemberRepository())
            {
                memberRepo.Add(member);
                return true;
            }
        }
        public bool UpdateMember(Member member)
        {
            using (var memberRepo = new MemberRepository())
            {
                int edited = memberRepo.Update(member);
                if (edited != 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DeleteMember(Member member)
        {
            using (var memberRepo = new MemberRepository())
            {
                int deleted = memberRepo.Remove(member);
                if(deleted != 0)
                {
                    return true;
                }
                return false;
            }
        }
        public List<Member> GetAllMembersByFilter(string name, string surname)
        {
            using (var memberRepo = new MemberRepository())
            {
                return memberRepo.GetAllMembersByFilter(name, surname).ToList();
            }
        }

        public int GetMemberId(string username) {
            using (var memberRepo = new MemberRepository()) {
                return memberRepo.GetMemberId(username);
            }
        }

        public IQueryable<string> GetMemberNameSurname(int memberId) {
            using (var memberRepo = new MemberRepository()) {
                return memberRepo.GetMemberNameSurname(memberId);
            }
        }
        public int GetMemberLibraryId(string username)
        {
            using (var memberRepo = new MemberRepository())
            {
                return memberRepo.GetMemberLibraryId(username);
            }
        }
        public Member GetMemberByUsername(string username)
        {
            using (var memberRepo = new MemberRepository())
            { 
                return memberRepo.GetMembersByUsername(username).First();
            }
        }
        public List<Member> GetAllMembersByLybrary()
        {
            int LibraryId = employeeService.GetEmployeeLibraryId(LoggedUser.Username);
            using (var memberRepo = new MemberRepository())
            {
                return memberRepo.GetMembersByLibrary(LibraryId).ToList();
            }
        }
        public string RandomCodeGenerator()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] randomArray = new char[8];

            for (int i = 0; i < 8; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomArray);
        }
    }
}

