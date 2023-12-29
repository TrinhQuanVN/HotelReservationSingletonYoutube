using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Models
{
    public class Reservation
    {
        public RoomID RoomID { get;}
        public string UserName { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Reservation(RoomID roomID, string userName, DateTime startDate, DateTime endDate)
        {
            RoomID = roomID;
            UserName = userName;
            StartDate = startDate;
            EndDate = endDate;
        }

        public TimeSpan Length => EndDate.Subtract(StartDate);

        public bool Conflicts(Reservation reservation)
        {
            if (reservation.RoomID != RoomID)
            {
                return false;
            }
            return reservation.StartDate < EndDate && reservation.EndDate > StartDate;
        }
    }
}
