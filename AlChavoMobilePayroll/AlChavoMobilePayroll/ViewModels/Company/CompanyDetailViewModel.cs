using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.Company;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlChavoMobilePayroll.ViewModels.Company
{
    [QueryProperty(nameof(name), nameof(name))]
    [QueryProperty(nameof(email), nameof(email))]
    public class CompanyDetailViewModel : Common.BaseViewModel
    {
        private CompanyDetailResponse CompanyModel { get; set; }

        public CompanyDetailResponse companyModel
        {
            get => CompanyModel;
            set
            {
                CompanyModel = value;
                OnPropertyChanged();
            }
        }

        private string Name { get; set; }
        private string Email { get; set; }

        public string name
        {
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged();
            }
        }

        public string email
        {
            get => Email;
            set
            {
                Email = value;
                OnPropertyChanged();
            }
        }

        public CompanyDetailViewModel()
        {
            InitializeData();
        }

        public async void InitializeData()
        {
            Title = "Company Information";
            var compId = await SecureStorage.GetAsync("CompId");
            companyModel = await GetCompanyInfo(compId).ConfigureAwait(false);
        }

        public async Task<CompanyDetailResponse> GetCompanyInfo(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            var data = await CompanyService.GetByID(id).ConfigureAwait(false);
            return data;
        }
    }
}
