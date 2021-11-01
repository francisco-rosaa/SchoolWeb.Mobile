using Prism;
using Prism.Ioc;
using SchoolWebMobile.Services;
using SchoolWebMobile.ViewModels;
using SchoolWebMobile.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace SchoolWebMobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync
                ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{nameof(AboutPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SchoolWebMasterDetailPage, SchoolWebMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MyProfilePage, MyProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MyEvaluationsPage, MyEvaluationsPageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
            containerRegistry.RegisterForNavigation<LogoutPage, LogoutPageViewModel>();
            containerRegistry.RegisterForNavigation<MyEvaluationDetailsPage, MyEvaluationDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherDetailsPage, WeatherDetailsPageViewModel>();
        }
    }
}
