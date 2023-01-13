using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.User;

using System;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class UserService : BaseService
    {
        const string Module = "User";
       // private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private readonly string apiUrl = GetAPIKEY("ApiKey");

        public async Task<UserInfoDetailResponse> GetUserInfoDetailed(string id)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetUserInfoDetailed)}";
            var uri = new UriBuilder(url).AddQuery("guid", id);

            var service = new HttpHelper<UserInfoDetailResponse>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<UserInfoResponse> GetUserInfo(string id)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetUserInfo)}";
            var uri = new UriBuilder(url).AddQuery("guid", id);

            var service = new HttpHelper<UserInfoResponse>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task UpdateAPBankIdByComp(string bankId, string compId)
        {
            var url = $"{apiUrl}{Module}/{nameof(UpdateAPBankIdByComp)}";
            var service = new HttpHelper<Task>();

            var request = new { BankId = bankId, CompId = compId };
           await service.PostRestServiceDataAsync(url.ToString(), request).ConfigureAwait(false);
        }
    }
}
