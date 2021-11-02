using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using SchoolWebMobile.Views;

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
            _searchCommand ?? (_searchCommand = new DelegateCommand(GoToWeatherDetailsFromSearch));

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

        private async void GoToWeatherDetailsFromSearch()
        {
            if (string.IsNullOrEmpty(Search))
            {
                await App.Current.MainPage.DisplayAlert("Oops", "Gotta search for something", "Accept");
                return;
            }

            if (Search.Length < 2)
            {
                await App.Current.MainPage.DisplayAlert("Oops", "Two characters minimum", "Accept");
                return;
            }

            NavigationParameters parameters = new NavigationParameters
            {
                { "weather", Search }
            };

            await _navigationService.NavigateAsync(nameof(WeatherDetailsPage), parameters);
        }
    }
}
