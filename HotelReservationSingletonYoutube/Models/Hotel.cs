using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Models
{
    public class Hotel
    {
        public string Name { get; }
        private readonly ReservationBook book;

        public Hotel(string name)
        {
            Name = name;
            this.book = new ReservationBook();
        }

        public IEnumerable<Reservation> GetReservations() => book.GetReservations();
        public IEnumerable<Reservation> GetReservations(string userName) => book.GetReservations(userName);
        public void MakeReservation(Reservation reservation)=> book.AddReservations(reservation);
    }
}
