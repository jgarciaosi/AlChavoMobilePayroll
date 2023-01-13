using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.User;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AlChavoMobilePayroll.ViewModels.User
{
    public class UserDetailViewModel : Common.BaseViewModel
    {
        private UserInfoDetailResponse UserModel { get; set; }

        public UserInfoDetailResponse userModel
        {
            get => UserModel;
            set
            {
                UserModel = value;
                OnPropertyChanged();
            }
        }

        public UserInfoDetailResponse userModel2
        {
            get => UserModel;
            set
            {
                UserModel = value;
                OnPropertyChanged();
            }
        }

        public UserDetailViewModel()
        {
            Title = "Personal Information";
            InitializeData();
        }

        public async void InitializeData()
        {
            var userGUID = await SecureStorage.GetAsync("UserGUID");
            userModel = await GetUserInfoDetail(userGUID).ConfigureAwait(false);

            userModel2 = await GetUserInfoDetail(userGUID).ConfigureAwait(false);
        }

        public async Task<UserInfoDetailResponse> GetUserInfoDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await UserService.GetUserInfoDetailed(id).ConfigureAwait(false);
            return data;
        }
    }
}
