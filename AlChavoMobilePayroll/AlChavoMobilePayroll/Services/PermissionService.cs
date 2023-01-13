using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.FileManager;
using AlChavoMobilePayroll.Models.SManagement.Permission;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class PermissionService : BaseService
    {
        const string Module = "Sec";

        internal static void Register<T>()
        {
            throw new NotImplementedException();
        }

       // private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private readonly string apiUrl = GetAPIKEY("ApiKey");
        public async Task<List<PermissionResponse>> GetPermissions(string ParamCompID, string ParamUserID)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetPermissions)}";
            var uirBuilder = new UriBuilder(url).AddQuery(Constants.CompIdParameter, ParamCompID).AddQuery(Constants.GuidParameter, ParamUserID.ToString());

            var service = new HttpHelper<List<PermissionResponse>>();
            var data = await service.GetRestServiceDataAsync(uirBuilder.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<CompanyFileRegisterInfoResponse> GetCompanyFileRegisterInfo(string CompId)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetCompanyFileRegisterInfo)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("compId", CompId);

            var service = new HttpHelper<CompanyFileRegisterInfoResponse>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            return data;

        }
    }
}
