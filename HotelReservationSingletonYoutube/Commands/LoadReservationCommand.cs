using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Stores;
using HotelReservationSingletonYoutube.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationSingletonYoutube.Commands
{
    public class LoadReservationCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _viewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _viewModel = viewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.ErrorMessage = string.Empty;
            try
            {
                await _hotelStore.Load();

                //throw new Exception();

                await Task.Delay(2000);

                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations.";
            }
            _viewModel.IsLoading = false;
        }
    }
}
