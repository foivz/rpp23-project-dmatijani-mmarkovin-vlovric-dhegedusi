using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    public class ReservationService
    {
        public int CheckPosition(int id)
        {
            using(var repo = new ReservationRepository())
            {
                return repo.CheckPosition(id)+1;
            }
        }
        public bool CheckExistingReservation(int bookId, int memberId)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.CheckExistingReservation(bookId, memberId);
            }
        }
        public int Add(Reservation reservation)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.Add(reservation);
            }
        }
    }
}
