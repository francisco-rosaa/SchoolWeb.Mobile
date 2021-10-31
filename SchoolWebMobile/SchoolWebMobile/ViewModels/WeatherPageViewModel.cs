using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolWebMobile.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase
    {
        public WeatherPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Weather";
        }
    }
}
