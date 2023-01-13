using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.Payee;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class PayeeService : BaseService
    {
        const string Module = "payee";
      //  private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"]; // = https://osialchavoapi.azurewebsites.net/api/
        private readonly string apiUrl = GetAPIKEY("ApiKey");

        public async Task<List<PayeeRequest>> GetPayee(string CompID, string PayeeId)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetPayee)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", CompID);
            uri.AddQuery("PayeeId", PayeeId);

            var helper = new HttpHelper<List<PayeeRequest>>();
            var Payees = await helper.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return Payees;
        }

        public async Task<PayeeGetDefaultsResponse> GetDefaultsById(string Compid, string PayeeID)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetDefaultsById)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", Compid);
            uri.AddQuery("PayeeId", PayeeID);

            var helper = new HttpHelper<PayeeGetDefaultsResponse>();
            var Payees = await helper.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return Payees;
        }

        public async Task<List<PayeeDeliveryInstructionRequest>> GetDeliveryInstruction(string Lang)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetDeliveryInstruction)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Lang", Lang);

            var helper = new HttpHelper<List<PayeeDeliveryInstructionRequest>>();
            var DeliveryInstructions = await helper.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return DeliveryInstructions;
        }

        
    }
}
