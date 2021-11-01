using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using SchoolWebMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SchoolWebMobile.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _searchCommand;
        private WeatherResponse _weather;
        private bool _isRunning;
        private string _search;

        public WeatherPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;

            Title = "Weather";
            ColorBrown = App.Current.Resources["ColorBrown"].ToString();
        }

        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(LoadWeatherSearch));

        public WeatherResponse Weather
        {
            get => _weather;
            set => SetProperty(ref _weather, value);
        }

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public string ColorBrown { get; set; }

        private async void LoadWeatherSearch()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "No Internet connection", "Accept");
                });

                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["ApiWeatherBaseUrl"].ToString();
            string prefix = App.Current.Resources["ApiWeatherPrefix"].ToString();
            string controller = $"{App.Current.Resources["ApiWeatherCity"]}?q={Search}&appid={App.Current.Resources["ApiWeatherKey"]}";

            Response response = await _apiService.GetSingleResultAsync<WeatherResponse>(url, prefix, controller);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Oops", response.Message, "Accept");
                return;
            }

            Weather = response.Result as WeatherResponse;

            NavigationParameters parameters = new NavigationParameters
            {
                { "weather", Weather }
            };

            await _navigationService.NavigateAsync(nameof(WeatherDetailsPage), parameters);
        }
    }
}
