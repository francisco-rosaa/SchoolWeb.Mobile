using Prism.Commands;
using Prism.Navigation;
using SchoolWebMobile.ItemViewModels;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SchoolWebMobile.ViewModels
{
    public class MyEvaluationsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private DelegateCommand _searchCommand;
        private ObservableCollection<CourseItemViewModel> _courses;
        private List<CourseResponse> _myCourses;
        private bool _isRunning;
        private string _search;

        public MyEvaluationsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;

            Title = "My Evaluations";
            ColorBrown = App.Current.Resources["ColorBrown"].ToString();
            LoadCoursesAsync();
        }

        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCourses));

        public ObservableCollection<CourseItemViewModel> Courses
        {
            get => _courses;
            set => SetProperty(ref _courses, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCourses();
            }
        }

        public string ColorBrown { get; set; }

        private async void LoadCoursesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "No Internet connection", "Accept");
                });

                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            if (!mainViewModel.IsTokenValid() || !mainViewModel.IsEmailSaved())
            {
                await App.Current.MainPage.DisplayAlert("Oops", "Insuficient user information", "Accept");
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["ApiSchoolBaseUrl"].ToString();
            string prefix = App.Current.Resources["ApiSchoolPrefix"].ToString();
            string controller = App.Current.Resources["ApiSchoolStudentCourses"].ToString() + $"/{mainViewModel.Email}";

            Response response = await _apiService.GetMultipleResultsAsync<CourseResponse>
                (url, prefix, controller, "bearer", MainViewModel.GetInstance().Token.Token);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Oops", response.Message, "Accept");
                return;
            }

            _myCourses = (response.Result as List<CourseResponse>).OrderBy(x => x.Name).ToList();
            ShowCourses();
        }

        private void ShowCourses()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Courses = new ObservableCollection<CourseItemViewModel>
                    (_myCourses.Select(x => new CourseItemViewModel(_navigationService)
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        Area = x.Area,
                        Duration = x.Duration
                    })
                    .ToList());
            }
            else
            {
                Courses = new ObservableCollection<CourseItemViewModel>
                    (_myCourses.Select(x => new CourseItemViewModel(_navigationService)
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        Area = x.Area,
                        Duration = x.Duration
                    })
                    .Where(x => x.Name.ToLower().Contains(Search.ToLower()) || x.Area.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
