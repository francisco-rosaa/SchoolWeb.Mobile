using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchoolWebMobile.ItemViewModels;
using SchoolWebMobile.Models;
using SchoolWebMobile.Views;
using System;
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
                    Icon = "",
                    PageName = $"{nameof(MyProfilePage)}",
                    Title = "My Profile",
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "",
                    PageName = $"{nameof(LoginPage)}",
                    Title = "Login",
                    IsLoginRequired = false
                }
            };

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
