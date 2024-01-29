﻿using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    // David Matijanić: GetMemberByBarcodeId
    public class MemberService {
        EmployeeService employeeService = new EmployeeService();
        BorrowService borrowService = new BorrowService();
        ReservationService reservationService = new ReservationService();
        
        public void CheckLoginCredentials(string username, string password) {
            using (var memberRepo = new MemberRepository()) {
                var returned = memberRepo.GetMemberLogin(username, password).ToList();

                if (returned.Count() == 1) {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Member;
                    LoggedUser.LibraryId = returned[0].Library_id;
                }
            }
        }
        public bool CheckMembershipDateLogin(string username, string password) {
            using (var memberRepo = new MemberRepository())
            {
                LibraryService libraryService = new LibraryService();
                Member returned = memberRepo.GetMemberLogin(username, password).ToList().FirstOrDefault();
                if(returned != null){
                    decimal membershipDuration = libraryService.GetLibraryMembershipDuration(returned.Library_id);

                    DateTime? membershipRunOutDate = returned.membership_date.HasValue ? returned.membership_date.Value.AddDays(Convert.ToInt16(membershipDuration)) : (DateTime?)null;
                    DateTime dateNow = DateTime.Now;
                    if (dateNow > membershipRunOutDate)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool CheckExistingUsername(Member member)
        {
            using (var memberRepo = new MemberRepository())
            {
                List<Member> existingMembers = memberRepo.GetAll().ToList();
                foreach (var m in existingMembers)
                {
                    if(m.username == member.username)
                        return true;
                }
            }
            return false;
        }
        public bool CheckBarcodeUnoque(Member member)
        {
            using (var memberRepo = new MemberRepository())
            {
                List<Member> existingMembers = memberRepo.GetAll().ToList();
                foreach (var m in existingMembers)
                {
                    if (m.barcode_id == member.barcode_id)
                        return true;
                }
            }
            return false;
        }
        public bool CheckOibUnoque(Member member)
        {
            using (var memberRepo = new MemberRepository())
            {
                List<Member> existingMembers = memberRepo.GetAll().ToList();
                foreach (var m in existingMembers)
                {
                    if (m.OIB == member.OIB)
                        return true;
                }
            }
            return false;
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
            bool borrowsClear = CheckBorrowsForMember(member);
            bool resrevationsClear = CheckReservationsForMember(member);
            if(borrowsClear && resrevationsClear)
            {
                using (var memberRepo = new MemberRepository())
                {
                    int deleted = memberRepo.DeleteMember(member.id);
                    if(deleted != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckBorrowsForMember(Member member)
        {
            List<Borrow> borrowsForMember = borrowService.GetAllBorrowsForMember(member.id, member.Library_id);
            foreach (var borrow in borrowsForMember)
            {
                if(borrow.borrow_status == (int)BorrowStatus.Late
                    || borrow.borrow_status == (int)BorrowStatus.Borrowed)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckReservationsForMember(Member member)
        {
            List<Reservation> resrevationsForMember = reservationService.GetReservationsForMemberNormal(member.id);
            foreach (var reservation in resrevationsForMember)
            {
                if (reservation.reservation_date != null)
                {
                    return false;
                }
            }
            return true;
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
            using (var memberRepo = new MemberRepository()) {
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
        public bool RestoreMembership(Member member)
        {
            LibraryService libraryService = new LibraryService();
            decimal membershipDuration = libraryService.GetLibraryMembershipDuration(member.Library_id);

            DateTime? membershipRunOutDate = member.membership_date.HasValue? member.membership_date.Value.AddDays(Convert.ToInt16(membershipDuration)): (DateTime?)null;
            DateTime dateNow = DateTime.Now;
            if(dateNow > membershipRunOutDate)
            {
                using (var memberRepo = new MemberRepository())
                {
                    int restored = memberRepo.UpdateMembershipDate(member,dateNow);
                    if(restored > 0)
                    {
                        return true;
                    }
                }
            }
            return false;

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

        public string GetMemberBarcode(int id) {
            using (var repository = new MemberRepository()) {
                return repository.GetMemberBarcode(id).FirstOrDefault();
            }
        }

        public List<Member> GetMembersByLibrary(int libraryId) {
            using (var repository = new MemberRepository()) {
                return repository.GetMembersByLibrary(libraryId).ToList();
            }
        }
    }
}

