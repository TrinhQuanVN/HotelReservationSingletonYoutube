using HotelReservationSingletonYoutube.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.DbContexts
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
