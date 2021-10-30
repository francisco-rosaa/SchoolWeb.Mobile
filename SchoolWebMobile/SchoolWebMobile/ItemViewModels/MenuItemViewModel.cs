using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.ViewModels;
using SchoolWebMobile.Views;
using System;

namespace SchoolWebMobile.ItemViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ??
            (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (IsLoginRequired)
            {
                var mainViewModel = MainViewModel.GetInstance();

                if (mainViewModel.Token == null)
                {
                    await _navigationService.NavigateAsync
                        ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{nameof(LoginPage)}");
                }

                if (mainViewModel.IsTokenValid())
                {
                    await _navigationService.NavigateAsync
                        ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{PageName}");
                }
            }
            else
            {
                await _navigationService.NavigateAsync
                    ($"/{nameof(SchoolWebMasterDetailPage)}/NavigationPage/{PageName}");
            }
        }
    }
}
