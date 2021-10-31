using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SchoolWebMobile.ViewModels
{
    public class MyEvaluationDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private CourseResponse _course;
        private ObservableCollection<EvaluationResponse> _evaluations;
        private bool _isRunning;

        public MyEvaluationDetailsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public CourseResponse Course
        {
            get => _course;
            set => SetProperty(ref _course, value);
        }

        public ObservableCollection<EvaluationResponse> Evaluations
        {
            get => _evaluations;
            set => SetProperty(ref _evaluations, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("course"))
            {
                Course = parameters.GetValue<CourseResponse>("course");
                Title = Course.Name;

                LoadEvaluationAsync();
            }
        }

        private async void LoadEvaluationAsync()
        {
            if (Course == null)
            {
                await App.Current.MainPage.DisplayAlert("Oops", "No course found", "Accept");
            }

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
            string controller = App.Current.Resources["ApiSchoolStudentEvaluations"].ToString() + $"/{mainViewModel.Email}" + $"/{Course.Id}";

            Response response = await _apiService.GetMultipleResultsAsync<EvaluationResponse>
                (url, prefix, controller, "bearer", MainViewModel.GetInstance().Token.Token);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Oops", response.Message, "Accept");
                return;
            }

            Evaluations = new ObservableCollection<EvaluationResponse>(response.Result as List<EvaluationResponse>);
        }
    }
}
