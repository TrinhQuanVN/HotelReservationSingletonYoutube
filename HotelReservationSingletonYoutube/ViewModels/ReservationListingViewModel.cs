using HotelReservationSingletonYoutube.Commands;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationSingletonYoutube.ViewModels
{
    public class ReservationListingViewModel: ViewModelBase
    {
        private readonly Hotel _hotel;
        private readonly ObservableCollection<ReservationViewModel> reservations;

        public IEnumerable<ReservationViewModel> Reservations => reservations;
        public ICommand MakeReservation { get; }

        public ReservationListingViewModel(Hotel hotel, NavigationService navigationService)
        {
            _hotel = hotel;
            reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservation = new NavigateCommand(navigationService);
            UpdateReservationListing();
        }

        private void UpdateReservationListing()
        {
            reservations.Clear();
            foreach (var item in _hotel.GetReservations())
            {
                reservations.Add(new ReservationViewModel(item));
            }
        }
    }
}
