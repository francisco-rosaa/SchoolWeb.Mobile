using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using SchoolWebMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SchoolWebMobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = new ApiService();
            IsEnabled = true;

            Title = "Welcome";
            Logo = "ic_logo_wide.png";
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

        public string Logo { get; set; }

        private async void Login()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "No Internet connection", "Accept");
                });

                Password = string.Empty;
                return;
            }

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

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;

            if (mainViewModel.Token == null)
            {
                await _navigationService.NavigateAsync
                    ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{nameof(LoginPage)}");
            }

            if (mainViewModel.IsTokenValid())
            {
                await _navigationService.NavigateAsync
                    ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{nameof(MyProfilePage)}");
            }
        }
    }
}
