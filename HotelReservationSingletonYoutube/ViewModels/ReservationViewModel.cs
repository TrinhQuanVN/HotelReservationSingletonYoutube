using HotelReservationSingletonYoutube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.ViewModels
{
    public class ReservationViewModel
    {
        private Reservation reservation;

        public string RoomID => reservation.RoomID.ToString();
        public string Username => reservation.UserName;
        public string StartDate => reservation.StartDate.ToString("d");
        public string EndDate => reservation.EndDate.ToString("d");

        public ReservationViewModel(Reservation reservation)
        {
            this.reservation = reservation;
        }
    }
}
