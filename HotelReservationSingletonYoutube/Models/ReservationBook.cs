using HotelReservationSingletonYoutube.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> reservations;

        public ReservationBook()
        {
            this.reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservations() => reservations;
        public IEnumerable<Reservation> GetReservations(string userName)
        {
            return reservations.Where(x => x.UserName == userName).ToList();
        }
        public void AddReservations(Reservation reservation)
        {
            foreach (var item in reservations)
            {
                if (item.Conflicts(reservation))
                {
                    throw new ReservationConflictException(item, reservation);
                }
            }
            reservations.Add(reservation);
        }

    }
}
