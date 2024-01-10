using HotelReservationSingletonYoutube.Commands;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using HotelReservationSingletonYoutube.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationSingletonYoutube.ViewModels
{
    public class ReservationListingViewModel: ViewModelBase
    {
        private readonly HotelStore _hotelStore;
        private readonly ObservableCollection<ReservationViewModel> reservations;

        public IEnumerable<ReservationViewModel> Reservations => reservations;
        public ICommand MakeReservation { get; }
        public ICommand LoadReservationsCommand { get; }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }


        public ReservationListingViewModel(HotelStore hotelStore, NavigationService navigationService)
        {
            _hotelStore = hotelStore;
            reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservation = new NavigateCommand(navigationService);
            LoadReservationsCommand = new LoadReservationCommand(this, hotelStore);

            _hotelStore.ReservationMade += OnReservationMade;
        }
        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;
            base.Dispose();
        }

        private void OnReservationMade(Reservation arg)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(arg);
            reservations.Add(reservationViewModel);
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);

            viewModel.LoadReservationsCommand.Execute(null);

            return viewModel;
        }



        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            this.reservations.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                this.reservations.Add(reservationViewModel);
            }
        }
    }
}
