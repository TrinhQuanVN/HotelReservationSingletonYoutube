using HotelReservationSingletonYoutube.Exceptions;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
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
    public class MakeReservationCommand : CommandBase
    {
        private readonly MakeReservationViewModel viewModel;
        private readonly Hotel hotel;
        private readonly NavigationService navigationService;
        

        public MakeReservationCommand(MakeReservationViewModel viewModel, Hotel hotel,NavigationService navigationService)
        {
            this.viewModel = viewModel;
            this.hotel = hotel;
            this.navigationService = navigationService;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(viewModel.Username) && viewModel.FloorNumber>0 && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            var reservation = new Reservation(
                    new RoomID(viewModel.RoomNumber, viewModel.FloorNumber),
                    viewModel.Username,
                    viewModel.StartDate,
                    viewModel.EndDate
                    );
            try
            {
                hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved room.", "Success",
                   MessageBoxButton.OK, MessageBoxImage.Information);

                navigationService.Navigate();

            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken.", "Error",
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
