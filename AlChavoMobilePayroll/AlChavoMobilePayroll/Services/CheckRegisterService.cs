using AlChavoMobilePayroll.ExtensionMethods;
using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.CheckRegister;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AlChavoMobilePayroll.Services
{
    public class CheckRegisterService : BaseService
    {
        public ChartEntry[] EntriesChart;

        const string Module = "CheckRegister";
        // private readonly string apiUrl = ConfigurationManager.AppSettings["ApiKey"];
        private string apiUrl = GetAPIKEY("ApiPAKey");
        public async Task<List<GetAllResponse>> GetCheckRegister(GetAllRequest checkRegisterRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetCheckRegister)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", checkRegisterRequest.Compid);
            uri.AddQuery("EmployeeId", checkRegisterRequest.EmployeeId);
            uri.AddQuery("CheckDateFrom", checkRegisterRequest.CheckDateFrom.ToString());
            uri.AddQuery("CheckDateTo", checkRegisterRequest.CheckDateTo.ToString());
            var service = new HttpHelper<List<GetAllResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

           

            for (int i = 0; i < data.Count; i++)
            {               

                data[i].CheckRegisterIndexPieData = new System.Collections.ObjectModel.ObservableCollection<ModelCheckRegisterLastPie>()
            {
                new ModelCheckRegisterLastPie("Net",data[i].NetPct),
                new ModelCheckRegisterLastPie("Gross",data[i].GrossPct)
            };
            }

            return data;
        }


        public async Task<GetCheckRegisterDetailResponse> GetCheckRegisterDetail(GetCheckRegisterDetailRequest checkRegisterDetailRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetCheckRegisterDetail)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", checkRegisterDetailRequest.Compid);
            uri.AddQuery("EmployeeId", checkRegisterDetailRequest.EmployeeId);
            uri.AddQuery("checkNumber", checkRegisterDetailRequest.CheckNumber.ToString());
            var service = new HttpHelper<GetCheckRegisterDetailResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);


            data.CheckPieData = new System.Collections.ObjectModel.ObservableCollection<ModelCheckRegisterLastPie>()
            {
                new ModelCheckRegisterLastPie("Taxes",data.TaxesPct),
                  new ModelCheckRegisterLastPie("Net",data.NetPct),
                    new ModelCheckRegisterLastPie("OtherWithholding",data.OtherVolWithholdPct),
                      //new ModelCheckRegisterLastPie("Gross",data.GrossPct),   El gross por ser el total no va en el gráfico
            };

            return data;
        }

        public async Task<GetCheckRegisterDetailResponse> GetSSPCheckRegisterYTDDetail(GetCheckRegisterDetailRequest checkRegisterDetailRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetSSPCheckRegisterYTDDetail)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", checkRegisterDetailRequest.Compid);
            uri.AddQuery("EmployeeId", checkRegisterDetailRequest.EmployeeId);
            uri.AddQuery("CheckNumber", checkRegisterDetailRequest.CheckNumber.ToString());
            var service = new HttpHelper<GetCheckRegisterDetailResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            data.CheckPieData = new System.Collections.ObjectModel.ObservableCollection<ModelCheckRegisterLastPie>()
            {
                new ModelCheckRegisterLastPie("Taxes",data.TaxesPct),
                  new ModelCheckRegisterLastPie("Net",data.NetPct),
                    new ModelCheckRegisterLastPie("OtherWithholding",data.OtherVolWithholdPct)
                      //new ModelCheckRegisterLastPie("Gross",data.GrossPct ),  El gross por ser el total no va en el gráfico
            };

            return data;
        }

        public async Task<GetCheckRegisterDetailResponse> GetSSPCheckRegisterLast(GetCheckRegisterDetailRequest checkRegisterLastRequest)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetSSPCheckRegisterLast)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", checkRegisterLastRequest.Compid);
            uri.AddQuery("EmployeeId", checkRegisterLastRequest.EmployeeId);
            var service = new HttpHelper<GetCheckRegisterDetailResponse>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);

            data.CheckPieData = new System.Collections.ObjectModel.ObservableCollection<ModelCheckRegisterLastPie>()
            {
                new ModelCheckRegisterLastPie("Taxes",data.TaxesPct),
                  new ModelCheckRegisterLastPie("Net",data.NetPct),
                    new ModelCheckRegisterLastPie("OtherWithholding",data.OtherVolWithholdPct)
                      //new ModelCheckRegisterLastPie("OtherCompensation",data.OtherCompensationPct), esto ya va en el neto por eso no se incluye
            };
                      
         
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterListDetailResponse>> GetMobileCheckRegisterTaxes(string compId, string empId, string checkNumber)
        {            
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterTaxes)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterListDetailResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterListDetailResponse>> GetMobileCheckRegisterYTDTaxes(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterYTDTaxes)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterListDetailResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailOtherCompResponse>> GetMobileCheckRegisterYTDOtherComp(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterYTDOtherComp)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailOtherCompResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailOtherCompResponse>> GetMobileCheckRegisterOtherComp(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterOtherComp)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailOtherCompResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailGrossResponse>> GetMobileCheckRegisterYTDGross(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterYTDGross)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailGrossResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailGrossResponse>> GetMobileCheckRegisterGross(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterGross)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailGrossResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailYTDTotalsResponse>> GetMobileCheckRegisterTotalCurrentAndYTD(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterTotalCurrentAndYTD)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailYTDTotalsResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailOtherVolResponse>> GetMobileCheckRegisterOtherVol(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterOtherVol)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailOtherVolResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailOtherVolResponse>> GetMobileCheckRegisterYTDOtherVol(string compId, string empId, string checkNumber)
        {
            var url = $"{apiUrl}{Module}/{nameof(GetMobileCheckRegisterYTDOtherVol)}";
            var uri = new UriBuilder(url);
            uri.AddQuery("Compid", compId);
            uri.AddQuery("EmployeeId", empId);
            uri.AddQuery("CheckNumber", checkNumber);
            var service = new HttpHelper<ObservableCollection<GetCheckRegisterDetailOtherVolResponse>>();

            var data = await service.GetRestServiceDataAsync(uri.ToString()).ConfigureAwait(false);
            return data;
        }


        public async Task<Uri> GetCheckRegisterReport(string compId, string empId, int payrollSequence, DateTime checkDate, string checkNumber, string bankId, int nextCheckOrder)
        {
            apiUrl = GetAPIKEY("ApiPAKey");
            var url = $"{apiUrl}{Module}/{nameof(GetCheckRegisterReport)}";
            var model = new { CompId = compId, EmpId = empId, PayrollSequence = payrollSequence, CheckDate = checkDate, CheckNumber = checkNumber, BankId = bankId, NextCheckOrder = nextCheckOrder };
            var service = new HttpHelper<Uri>();
            var data = await service.PostRestServiceDataAsync(url, model).ConfigureAwait(false);
            return data;
        }

    }
}

