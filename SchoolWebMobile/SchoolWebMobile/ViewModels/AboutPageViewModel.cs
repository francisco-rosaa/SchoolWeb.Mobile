using Prism.Navigation;

namespace SchoolWebMobile.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        public AboutPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "About";
            ColorBrown = App.Current.Resources["ColorBrown"].ToString();
        }

        public string ColorBrown { get; set; }
    }
}
