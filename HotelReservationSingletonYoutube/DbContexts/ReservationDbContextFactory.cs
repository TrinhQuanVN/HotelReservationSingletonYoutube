using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.DbContexts
{
    public class ReservationDbContextFactory
    {
        private readonly string _connectionString;

        public ReservationDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new ReservationDbContext(options);
        }
    }
}
