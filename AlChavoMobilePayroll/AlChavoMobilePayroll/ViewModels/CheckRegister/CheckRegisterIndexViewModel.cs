
using AlChavoMobilePayroll.Models;
using AlChavoMobilePayroll.Models.CheckRegister;
using AlChavoMobilePayroll.Models.SManagement.Company;
using AlChavoMobilePayroll.Views.CheckRegister;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.BehaviorsPack;

namespace AlChavoMobilePayroll.ViewModels.CheckRegister
{
    public class CheckRegisterIndexViewModel : Common.BaseViewModel
    {
        private GetAllRequest GetCheckRequest = new GetAllRequest { };

        public GetAllRequest getCheckRequest { get => GetCheckRequest; set { GetCheckRequest = value; OnPropertyChanged(); } }

        public DateTime checkDateFrom { get => CheckDateFrom; set { CheckDateFrom = value; OnPropertyChanged(); } }

        private DateTime CheckDateFrom { get; set; }

        public DateTime checkDateTo { get => CheckDateTo; set { CheckDateTo = value; OnPropertyChanged(); } }

        private DateTime CheckDateTo { get; set; }


        public List<CompaniesByUserResponse> companyList { get => CompanyList; set { CompanyList = value; OnPropertyChanged(); } }

        private List<CompaniesByUserResponse> CompanyList { get; set; }

        public List<GetAllResponse> checkRegisterList { get => CheckRegisterList; set { CheckRegisterList = value; OnPropertyChanged(); } }

        private List<GetAllResponse> CheckRegisterList { get; set; }

        public ChartEntry[] entriesChart { get; set; }

              public IAsyncCommand GetCheckRegisterListCommand { get; set; }

        public IAsyncCommand<object> GoCheckRegisterDetailCommand { get; set; }

        private GetAllResponse selectedCheckRegister;
        public GetAllResponse SelectedCheckRegister { get => selectedCheckRegister; set { selectedCheckRegister = value; OnPropertyChanged(); } }

        public async Task InitializeData()
        {

            isBusy = true;
            //Device.BeginInvokeOnMainThread(async () =>
            //           {
                           checkDateFrom = DateTime.Now.AddDays(-90);
                           checkDateTo = DateTime.Now;

                           var compId = await SecureStorage.GetAsync("CompId");
                           GetAllRequest checkRegisterRequest = new GetAllRequest();

                           checkRegisterRequest.Compid = compId;
                           checkRegisterRequest.EmployeeId = await SecureStorage.GetAsync("EmpId");
                           checkRegisterRequest.CheckDateFrom = checkDateFrom;
                           checkRegisterRequest.CheckDateTo = checkDateTo;


                           checkRegisterList = await CheckRegisterService.GetCheckRegister(checkRegisterRequest).ConfigureAwait(false);
                 //      });

            isBusy = false;
        }



        public CheckRegisterIndexViewModel()
        {
            Title = "Check Register";
            GetCheckRegisterListCommand = new AsyncCommand(GetCheckRegisterList);
            GoCheckRegisterDetailCommand = new AsyncCommand<object>((item) => GoCheckRegisterDetail());
            //InitializeData();
        }

        public async Task GetCheckRegisterList()
        {
            isBusy = true;


            getCheckRequest.Compid = await SecureStorage.GetAsync("CompId");
            getCheckRequest.EmployeeId = await SecureStorage.GetAsync("EmpId");
            getCheckRequest.CheckDateFrom = checkDateFrom;
            getCheckRequest.CheckDateTo = checkDateTo;

            checkRegisterList = await CheckRegisterService.GetCheckRegister(getCheckRequest).ConfigureAwait(false);

            isBusy = false;

        }

        public async Task GoCheckRegisterDetail()
        {
            GetAllResponse checkRegisterDetailData = selectedCheckRegister;
            //if (!string.IsNullOrEmpty(billRegister.SeparatedCheckNumber) && billRegister.SeparatedCheckNumber.ToLower() == "various")
            //{
            //    var model = JsonConvert.SerializeObject(billRegister);
            //    await Shell.Current.GoToAsync($"/{nameof(BillRegisterPaymentsDetail)}?{nameof(BillRegisterPaymentsDetailViewModel.content)}={model}");
            //}
            //else if (!string.IsNullOrEmpty(billRegister.SeparatedCheckNumber) && billRegister.SeparatedCheckNumber.ToLower() != "N/A")
            //{
            //    var getBillPaymentsResponse = await GetBillPaymentsResponse(billRegister).ConfigureAwait(true);
            //    if (getBillPaymentsResponse != null)
            //    {
            //        var getAllResponse = new GetAllResponse
            //        {
            //            Address = getBillPaymentsResponse.Address,
            //            Bank_Id = getBillPaymentsResponse.Bank_Id,
            //            Check_Date = getBillPaymentsResponse.Check_Date,
            //            Check_Number = getBillPaymentsResponse.Check_Number,
            //            CompId = getBillPaymentsResponse.CompId,
            //            Payee_Name = getBillPaymentsResponse.Payee_Name,
            //            PostalCode = getBillPaymentsResponse.PostalCode,
            //            Net_Amount = getBillPaymentsResponse.Net_Amount,
            //            EntryNum = getBillPaymentsResponse.EntryNum
            //        };

            var model = JsonConvert.SerializeObject(checkRegisterDetailData);
            await Shell.Current.GoToAsync($"/{nameof(CheckRegisterDetail)}?{nameof(CheckRegisterDetailViewModel.content)}={model}");


        }

    }
}