using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolWebMobile.ItemViewModels
{
    public class CourseItemViewModel : CourseResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCourseCommand;

        public CourseItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCourseCommand =>
            _selectCourseCommand ?? (_selectCourseCommand = new DelegateCommand(SelectCourseAsync));

        private async void SelectCourseAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "course", this }
            };

            await _navigationService.NavigateAsync(nameof(MyEvaluationDetailsPage), parameters);
        }
    }
}
