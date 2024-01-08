using HotelReservationSingletonYoutube.DbContexts;
using HotelReservationSingletonYoutube.Models;
using HotelReservationSingletonYoutube.Services;
using HotelReservationSingletonYoutube.Services.ReservationConflictValidators;
using HotelReservationSingletonYoutube.Services.ReservationCreators;
using HotelReservationSingletonYoutube.Services.ReservationProvider;
using HotelReservationSingletonYoutube.Stores;
using HotelReservationSingletonYoutube.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        private const string CONNECTIONSTRING = "Data source=reservation.db";
        private readonly ReservationDbContextFactory dbContextFactory;
        public App()
        {
            dbContextFactory = new ReservationDbContextFactory(CONNECTIONSTRING);
            var reservationProvider = new DatabaseReservationProvider(dbContextFactory);
            var reservationCreator = new DatabaseReservationCreator(dbContextFactory);
            var reservationConflictValidation = new ReservationConflictValidator(dbContextFactory);
            var reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidation);
            hotel = new Hotel("My hotel",reservationBook);
            navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using ( var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

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
