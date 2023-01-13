using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.Login;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class LoginService : BaseService
    {
        const string Module = "login";
        //private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"]; // = https://osialchavoapi.azurewebsites.net/api/
        private readonly string apiUrl = GetAPIKEY("ApiPAKey");
        public async Task<LoginResponse> authenticate(LoginRequest AuthUser)
        {
            var url = $"{apiUrl}{Module}/{nameof(authenticate)}";
            var helper = new HttpHelper<LoginResponse>();
            var result = await helper.PostLoginRestServiceDataAsync (url, AuthUser).ConfigureAwait(false);
            return result;
        }


    }
}
