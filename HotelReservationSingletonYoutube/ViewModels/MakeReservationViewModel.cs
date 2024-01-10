using HotelReservationSingletonYoutube.Commands;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using HotelReservationSingletonYoutube.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationSingletonYoutube.ViewModels
{
    public class MakeReservationViewModel:ViewModelBase
    {
		private string username;

		public string Username
		{
			get { return username; }
			set { username = value; OnPropertyChanged(nameof(Username)); }
		}
		private int floorNumber;

		public int FloorNumber
		{
			get { return floorNumber; }
			set { floorNumber = value; OnPropertyChanged(nameof(FloorNumber)); }
		}
		private int roomNumber;

		public int RoomNumber
		{
			get { return roomNumber; }
			set { roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
		}
		private DateTime startDate;

		public DateTime StartDate
		{
			get { return startDate; }
			set { startDate = value; OnPropertyChanged(nameof(StartDate)); }
		}
		private DateTime endDate;

		public DateTime EndDate
		{
			get { return endDate; }
			set { endDate = value; OnPropertyChanged(nameof(EndDate)); }
		}
		public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

		public MakeReservationViewModel(HotelStore hotelStore, NavigationService navigationService)
		{
			SubmitCommand = new MakeReservationCommand(this, hotelStore, navigationService);
			CancelCommand = new NavigateCommand(navigationService);
		}
	}
}
