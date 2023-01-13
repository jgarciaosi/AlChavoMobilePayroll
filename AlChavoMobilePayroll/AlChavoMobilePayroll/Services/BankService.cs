using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.Bank;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class BankService : BaseService
    {
        const string Module = "Bank";
        //private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private readonly string apiUrl = GetAPIKEY("ApiKey");

        public async Task<List<GetAllBankResponse>> GetAll(string compId)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetAll)}";
            var uirBuilder = new UriBuilder(url).AddQuery(nameof(GetAllBankResponse.CompId), compId);

            var service = new HttpHelper<List<GetAllBankResponse>>();
            var data = await service.GetRestServiceDataAsync(uirBuilder.ToString()).ConfigureAwait(false);
            return data;
        }
    }
}
