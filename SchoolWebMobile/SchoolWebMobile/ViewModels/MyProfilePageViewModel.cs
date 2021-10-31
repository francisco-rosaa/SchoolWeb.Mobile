using Prism.Navigation;
using SchoolWebMobile.Models;
using SchoolWebMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SchoolWebMobile.ViewModels
{
    public class MyProfilePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private StudentResponse _student;
        private string _profilePicturePath;
        private string _picturePath;
        private string _birthDate;
        private bool _isRunning;

        public MyProfilePageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = "My Profile";
            LoadStudentAsync();
        }

        public StudentResponse Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }

        public string ProfilePicturePath
        {
            get => _profilePicturePath;
            set => SetProperty(ref _profilePicturePath, value);
        }

        public string PicturePath
        {
            get => _picturePath;
            set => SetProperty(ref _picturePath, value);
        }

        public string BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private async void LoadStudentAsync()
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
            string controller = App.Current.Resources["ApiSchoolStudent"].ToString() + $"/{mainViewModel.Email}";

            Response response = await _apiService.GetSingleResultAsync<StudentResponse>
                (url, prefix, controller, "bearer", MainViewModel.GetInstance().Token.Token);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Oops", response.Message, "Accept");
                return;
            }

            Student = response.Result as StudentResponse;

            ProfilePicturePath = App.Current.Resources["ApiSchoolBaseUrl"].ToString() + Student.ProfilePicturePath.Replace("~", "");
            PicturePath = App.Current.Resources["ApiSchoolBaseUrl"].ToString() + Student.PicturePath.Replace("~", "");
            BirthDate = Student.BirthDate.ToString().Split(' ')[0];
        }
    }
}
