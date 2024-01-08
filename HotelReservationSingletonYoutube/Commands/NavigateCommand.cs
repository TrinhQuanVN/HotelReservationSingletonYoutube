using HotelReservationSingletonYoutube.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void ExecuteAsync(object? parameter)
        {
            navigationService.Navigate();
        }
    }
}
