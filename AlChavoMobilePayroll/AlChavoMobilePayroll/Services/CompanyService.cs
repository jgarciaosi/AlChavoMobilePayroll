using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.SManagement.Company;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlChavoMobilePayroll.Services
{
    public class CompanyService : BaseService
    {
        const string Module = "company";
    //    private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private readonly string apiUrl = GetAPIKEY("ApiPAKey");
        public async Task<CompanyDetailResponse> GetByID(string id)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetByID)}";
            var uri = new UriBuilder(url).AddQuery(nameof(CompanyDetailResponse.CompID), id);

            var service = new HttpHelper<CompanyDetailResponse>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<CompanyDetailAPDefaultsResponse> GetAPDfltsByID(string id)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetAPDfltsByID)}";
            var uri = new UriBuilder(url).AddQuery(nameof(CompanyDetailAPDefaultsResponse.CompID), id);

            var service = new HttpHelper<CompanyDetailAPDefaultsResponse>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<List<CompaniesByUserResponse>> GetByUserId(string id)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetByUserId)}";
            var uri = new UriBuilder(url).AddQuery(Constants.GuidParameter, id);

            var service = new HttpHelper<List<CompaniesByUserResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<List<CompaniesByUserResponse>> GetByUserIdMobileRestricted(string id)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetByUserIdMobileRestricted)}";
            var uri = new UriBuilder(url).AddQuery(Constants.GuidParameter, id);

            var service = new HttpHelper<List<CompaniesByUserResponse>>();
            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<string> GetMobileDownloadLink(string compId,string fileName)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileDownloadLink)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("CompId", compId);
            uri.AddQuery("FileName", fileName);
            

            var service = new HttpHelper<string>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            return data;
        }
    }
}
