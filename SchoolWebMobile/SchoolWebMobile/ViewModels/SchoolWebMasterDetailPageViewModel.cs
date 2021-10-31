using Prism.Navigation;
using SchoolWebMobile.ItemViewModels;
using SchoolWebMobile.Models;
using SchoolWebMobile.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolWebMobile.ViewModels
{
    public class SchoolWebMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SchoolWebMasterDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_icon_myprofile.png",
                    PageName = $"{nameof(MyProfilePage)}",
                    Title = "My Profile",
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "ic_icon_evaluations.png",
                    PageName = $"{nameof(MyEvaluationsPage)}",
                    Title = "My Evaluations",
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "ic_icon_weather.png",
                    PageName = $"{nameof(WeatherPage)}",
                    Title = "Weather",
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "ic_icon_about.png",
                    PageName = $"{nameof(AboutPage)}",
                    Title = "About",
                    IsLoginRequired = false
                }
            };

            var mainViewModel = MainViewModel.GetInstance();

            if (mainViewModel.IsTokenValid() && mainViewModel.IsEmailSaved())
            {
                var menuItem = new Menu
                {
                    Icon = "ic_icon_logout.png",
                    PageName = $"{nameof(LogoutPage)}",
                    Title = "Logout",
                    IsLoginRequired = false
                };

                menus.Add(menuItem);
            }
            else
            {
                var menuItem = new Menu
                {
                    Icon = "ic_icon_login.png",
                    PageName = $"{nameof(LoginPage)}",
                    Title = "Login",
                    IsLoginRequired = false
                };

                menus.Add(menuItem);
            }

            Menus = new ObservableCollection<MenuItemViewModel>
                (menus.Select(x => new MenuItemViewModel(_navigationService)
                {
                    Icon = x.Icon,
                    PageName = x.PageName,
                    Title = x.Title,
                    IsLoginRequired = x.IsLoginRequired
                }).ToList());
        }
    }
}
