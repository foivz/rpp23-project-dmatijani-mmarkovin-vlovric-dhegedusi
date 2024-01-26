using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ReservationRepository: Repository<Reservation>
    {
        public ReservationRepository() : base(new DatabaseModel())
        {

        }

        public override int Update(Reservation entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public int CheckPosition(int id)
        {
            var count = Entities.Count(r => r.Book_id == id);
            return count;
        }
        public bool CheckExistingReservation(int bookId, int memberId)
        {
            var query = (from r in Entities where r.Book_id == bookId && r.Member_id == memberId select r).Any();
            return query;
        }
        public override int Add(Reservation entity, bool saveChanges = true)
        {
            var reservation = new Reservation
            {
                //reservation_date = DateTime.Now, //ovo vjv promijenit da bude kad ističe
                Member_id = entity.Member_id,
                Book_id = entity.Book_id,
            };
            Entities.Add(reservation);
            if(saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public List<ReservationViewModel> GetReservationsForMember(int memberId)
        {
            var reservations = from r in Entities
                               where r.Member_id == memberId
                               join b in Context.Books on r.Book_id equals b.id
                               select new ReservationViewModel
                               {
                                   ReservationId = r.idReservation,
                                   BookName = b.name,
                                   Date = r.reservation_date
                               };

            return reservations.ToList();
        }
        public int RemoveReservation(int reservationId)
        {
            var reservation = Entities.FirstOrDefault(r => r.idReservation == reservationId);
            Entities.Remove(reservation);
            return SaveChanges();
        }
        public int CountExistingReservations(int memberId)
        {
            var count = Entities.Count(r => r.Member_id == memberId);
            return count;
        }
    }
}
