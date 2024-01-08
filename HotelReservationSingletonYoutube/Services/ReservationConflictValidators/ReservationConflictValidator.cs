using HotelReservationSingletonYoutube.DbContexts;
using HotelReservationSingletonYoutube.DTOs;
using HotelReservationSingletonYoutube.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Services.ReservationConflictValidators
{
    public class ReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReservationDbContextFactory _dbContextFactory;

        public ReservationConflictValidator(ReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetReservationConflict(Reservation reservation)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Reservations.Select(r => ToReservation(r)).FirstOrDefaultAsync(r => r.Conflicts(reservation));
            }

        }
        private Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(new RoomID(reservationDTO.FloorNumber, reservationDTO.RoomNumber), reservationDTO.Username, reservationDTO.StartDate, reservationDTO.EndDate);
        }
    }
}
