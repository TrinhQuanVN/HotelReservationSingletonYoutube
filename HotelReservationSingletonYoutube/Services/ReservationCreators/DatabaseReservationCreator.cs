using HotelReservationSingletonYoutube.DbContexts;
using HotelReservationSingletonYoutube.DTOs;
using HotelReservationSingletonYoutube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservationDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(ReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateReservation(Reservation reservation)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var reservationDTO = ToReservationDTO(reservation);
                context.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }

        private object ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomID?.FloorNumber ?? 0,
                RoomNumber = reservation.RoomID?.RoomNumber ?? 0,
                Username = reservation.UserName,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,

            };
        }
    }
}
