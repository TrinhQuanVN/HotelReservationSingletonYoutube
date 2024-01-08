using HotelReservationSingletonYoutube.Exceptions;
using HotelReservationSingletonYoutube.Services.ReservationConflictValidators;
using HotelReservationSingletonYoutube.Services.ReservationCreators;
using HotelReservationSingletonYoutube.Services.ReservationProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetReservations() => await _reservationProvider.GetReservations();
        //public IEnumerable<Reservation> GetReservations(string userName)
        //{
        //    return new IEnumerable<Reservation>();
        //    return reservations.Where(x => x.UserName == userName).ToList();
        //}
        public async Task AddReservations(Reservation reservation)
        {
            var reservationConflict = await _reservationConflictValidator.GetReservationConflict(reservation);
            if (reservationConflict != null)
            {
                throw new ReservationConflictException(reservationConflict, reservation);
            }
            
            await _reservationCreator.CreateReservation(reservation);
        }

    }
}
