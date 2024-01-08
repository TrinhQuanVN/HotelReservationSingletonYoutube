using HotelReservationSingletonYoutube.DbContexts;
using HotelReservationSingletonYoutube.DTOs;
using HotelReservationSingletonYoutube.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Services.ReservationProvider
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ReservationDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                return ToReservation(reservationDTOs);
            }
        }

        private IEnumerable<Reservation> ToReservation(IEnumerable<ReservationDTO> reservationDTOs)
        {
            return reservationDTOs.Select(p=> new Reservation(new RoomID(p.FloorNumber,p.RoomNumber),p.Username,p.StartDate,p.EndDate));
        }
    }
}
