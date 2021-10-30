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
                ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MyProfilePage, MyProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<SchoolWebMasterDetailPage, SchoolWebMasterDetailPageViewModel>();
        }
    }
}
