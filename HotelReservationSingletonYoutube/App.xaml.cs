using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using HotelReservationSingletonYoutube.Stores;
using HotelReservationSingletonYoutube.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationSingletonYoutube
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Hotel hotel;
        private readonly NavigationStore navigationStore;
        public App()
        {
            hotel = new Hotel("My hotel");
            navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = CreateReservationListingViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
        };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private ViewModelBase CreateReservationListingViewModel()
        {
            return new ReservationListingViewModel(hotel, new NavigationService(navigationStore, CreateMakeReservationViewModel));
        }

        private ViewModelBase CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(hotel, new NavigationService(navigationStore, CreateReservationListingViewModel));
        }
    }
}
