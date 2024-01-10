using HotelReservationSingletonYoutube.Exceptions;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using HotelReservationSingletonYoutube.Stores;
using HotelReservationSingletonYoutube.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationSingletonYoutube.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel viewModel;
        private readonly HotelStore hotelStore;
        private readonly NavigationService navigationService;
        

        public MakeReservationCommand(MakeReservationViewModel viewModel, HotelStore hotelStore,NavigationService navigationService)
        {
            this.viewModel = viewModel;
            this.hotelStore = hotelStore;
            this.navigationService = navigationService;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(viewModel.Username) && viewModel.FloorNumber>0 && base.CanExecute(parameter);
        }


        public  override async Task ExecuteAsync(object? parameter)
        {
            var reservation = new Reservation(
                    new RoomID(viewModel.RoomNumber, viewModel.FloorNumber),
                    viewModel.Username,
                    viewModel.StartDate,
                    viewModel.EndDate
                    );
            try
            {
                await hotelStore.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved room.", "Success",
                   MessageBoxButton.OK, MessageBoxImage.Information);

                navigationService.Navigate();

            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to make reservation." + Environment.NewLine + $"{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
                e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
