using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.DbContexts
{
    public class ReservationDesignTimeContextFactory : IDesignTimeDbContextFactory<ReservationDbContext>
    {
        public ReservationDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite().Options;
            return new ReservationDbContext(options);
        }
    }
}
