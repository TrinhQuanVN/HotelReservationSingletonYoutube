using HotelReservationSingletonYoutube.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Stores
{
    public class NavigationStore
    {
		private ViewModelBase currentViewModel;

		public ViewModelBase CurrentViewModel
		{
			get { return currentViewModel; }
			set { currentViewModel?.Dispose(); currentViewModel = value; OnCurrentViewModelChanged(); }
		}
        public event Action CurrentViewModelChanged;
        private void OnCurrentViewModelChanged()
		{
            CurrentViewModelChanged?.Invoke();
        }
	}
}
