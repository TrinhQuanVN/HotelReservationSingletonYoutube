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
            try
            {
                await _hotelStore.Load();

                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load reservations." +Environment.NewLine + $"{ex.Message}", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
