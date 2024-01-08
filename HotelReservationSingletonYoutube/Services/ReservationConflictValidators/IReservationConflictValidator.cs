using HotelReservationSingletonYoutube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Services.ReservationConflictValidators
{
    public interface IReservationConflictValidator
    {
        Task<Reservation> GetReservationConflict(Reservation reservation);
    }
}
