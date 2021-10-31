using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.Views;

namespace SchoolWebMobile.ViewModels
{
    public class LogoutPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _loadedCommand;

        public LogoutPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;

            Title = "Bye";
            ColorBrown = App.Current.Resources["ColorBrown"].ToString();
            Logout();
        }

        public string ColorBrown { get; set; }

        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(Redirect));

        private void Logout()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = null;
            mainViewModel.Email = string.Empty;
        }

        private async void Redirect()
        {
            await _navigationService.NavigateAsync
                ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{nameof(AboutPage)}");
        }
    }
}
