using HotelReservationSingletonYoutube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Stores
{
    public class HotelStore
    {
        private readonly List<Reservation> _reservation;
        private readonly Hotel _hotel;
        private Lazy<Task> _initializeLazy;

        public event Action<Reservation> ReservationMade;

        public HotelStore(Hotel hotel)
        {
            _reservation = new List<Reservation>();
            _hotel = hotel;
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public IEnumerable<Reservation> Reservations => _reservation;
        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
            }
        }
        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);
            _reservation.Add(reservation);
            OnReservationMade(reservation);
        }
        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }
        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetReservations();
            _reservation.Clear();
            _reservation.AddRange(reservations);
        }
    }
}
