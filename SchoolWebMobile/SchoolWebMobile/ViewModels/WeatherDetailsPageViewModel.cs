using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchoolWebMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolWebMobile.ViewModels
{
    public class WeatherDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private WeatherResponse _weather;

        public WeatherDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public WeatherResponse Weather
        {
            get => _weather;
            set => SetProperty(ref _weather, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("weather"))
            {
                Weather = parameters.GetValue<WeatherResponse>("weather");
                Title = Weather.Name;
            }
        }
    }
}
