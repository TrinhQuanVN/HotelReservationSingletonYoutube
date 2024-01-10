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

        public async Task<Reservation?> GetReservationConflict(Reservation reservation)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var reservationDTO =  await context.Reservations
                    .Where(r=> r.FloorNumber==reservation.RoomID.FloorNumber)
                    .Where(r=> r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r=> r.StartDate < reservation.EndDate)
                    .Where(r => r.EndDate > reservation.StartDate)
                    .FirstOrDefaultAsync();
                if (reservationDTO == null) return null;
                return ToReservation(reservationDTO);
            }

        }
        private Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(new RoomID(reservationDTO.FloorNumber, reservationDTO.RoomNumber), reservationDTO.Username, reservationDTO.StartDate, reservationDTO.EndDate);
        }
    }
}
