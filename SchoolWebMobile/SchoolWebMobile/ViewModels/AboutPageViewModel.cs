using Prism.Navigation;

namespace SchoolWebMobile.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        public AboutPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Hello";
            Logo = "ic_icon_logoabout.png";
            ColorBrown = App.Current.Resources["ColorBrown"].ToString();
            ColorBlue = App.Current.Resources["ColorBlueMain"].ToString();
        }

        public string Logo { get; set; }

        public string ColorBrown { get; set; }

        public string ColorBlue { get; set; }
    }
}
