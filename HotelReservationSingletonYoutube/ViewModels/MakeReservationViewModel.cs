using HotelReservationSingletonYoutube.Commands;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using HotelReservationSingletonYoutube.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationSingletonYoutube.ViewModels
{
    public class MakeReservationViewModel:ViewModelBase , INotifyDataErrorInfo
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
			set 
            { 
                startDate = value; 
                OnPropertyChanged(nameof(StartDate));

                ClearError(nameof(StartDate));
                if (EndDate < StartDate)
                {
                    AddError(nameof(StartDate), "The start date cannot be after the end date.");
                    OnErrorsChange(nameof(StartDate));
                }
                OnPropertyChanged(nameof(EndDate));
            }
		}
		private DateTime endDate;
        public DateTime EndDate
		{
			get { return endDate; }
			set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearError(nameof(EndDate));
                if (EndDate < StartDate)
                {
                    AddError(nameof(EndDate), "The end date cannot be before the start date.");
                    OnErrorsChange(nameof(EndDate));
                }
            }
        }

        private void AddError(string propertyName,string errorMesssage)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName]= new List<string>{ errorMesssage };
        }

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearError(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChange(propertyName);
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
		private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService navigationService)
		{
			SubmitCommand = new MakeReservationCommand(this, hotelStore, navigationService);
			CancelCommand = new NavigateCommand(navigationService);
			_propertyNameToErrorsDictionary = new();
		}

        public IEnumerable GetErrors(string? propertyName)
        {
			return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName,new List<string>());
        }
    }
}
