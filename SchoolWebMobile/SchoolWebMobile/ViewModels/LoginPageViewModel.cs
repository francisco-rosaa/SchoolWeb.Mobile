using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using SchoolWebMobile.Views;

namespace SchoolWebMobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private ApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Welcome";
            IsEnabled = true;
            _apiService = new ApiService();
            ColorYelow = App.Current.Resources["ColorYelow"].ToString();
            ColorBrown = App.Current.Resources["ColorBrown"].ToString();
        }

        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string ColorYelow { get; set; }

        public string ColorBrown { get; set; }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Hi", "Please enter your email", "Accept");
                Password = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Hi", "Please enter your password", "Accept");
                Password = string.Empty;
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new TokenRequest
            {
                Username = this.Email,
                Password = this.Password
            };

            string url = App.Current.Resources["ApiSchoolBaseUrl"].ToString();
            string controller = App.Current.Resources["ApiCreateToken"].ToString();

            var response = await _apiService.GetTokenAsync(url, controller, request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Oops", "Please try again", "Accept");
                Password = string.Empty;
                return;
            }

            var token = response.Result as TokenResponse;

            var mainViewMode = MainViewModel.GetInstance();
            mainViewMode.Token = token;

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(MyProfilePage)}");
        }
    }
}
