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

        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            this.book = reservationBook;
        }

        public async Task<IEnumerable<Reservation>> GetReservations() => await book.GetReservations();
        //public IEnumerable<Reservation> GetReservations(string userName) => book.GetReservations(userName);
        public async Task MakeReservation(Reservation reservation)=> await book.AddReservations(reservation);
    }
}
