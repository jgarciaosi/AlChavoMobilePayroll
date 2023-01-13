using Microcharts;
using System;
using AlChavoMobilePayroll.Models.CheckRegister;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.ObjectModel;
using AlChavoMobilePayroll.Views.CheckRegister;

namespace AlChavoMobilePayroll.ViewModels.CheckRegister
{
    [QueryProperty(nameof(content), nameof(content))]
    public class CheckRegisterDetailViewModel : Common.BaseViewModel


    {

    

        public string CurrentCheckTitle { get; set; }
        public string currentCheckTitle { get => CurrentCheckTitle; set { CurrentCheckTitle = value; OnPropertyChanged(); } }
        public string YTDCheckTitle { get; set; }
        public string ytdCheckTitle { get => YTDCheckTitle; set { YTDCheckTitle = value; OnPropertyChanged(); } }
        public GetCheckRegisterDetailResponse checkRegisterDetailResponse { get => CheckRegisterDetailResponse; set { CheckRegisterDetailResponse = value; OnPropertyChanged(); } }
        private GetCheckRegisterDetailResponse CheckRegisterDetailResponse { get; set; }
        public GetCheckRegisterDetailResponse checkRegisterDetailResponseYTD { get => CheckRegisterDetailResponseYTD; set { CheckRegisterDetailResponseYTD = value; OnPropertyChanged(); } }
        private GetCheckRegisterDetailResponse CheckRegisterDetailResponseYTD { get; set; }
        //Detail
        public ObservableCollection<GetCheckRegisterListDetailResponse> currentCheckTaxesList { get => CurrentCheckTaxesList; set { CurrentCheckTaxesList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterListDetailResponse> CurrentCheckTaxesList { get; set; }

        public ObservableCollection<GetCheckRegisterListDetailResponse> ytdCheckTaxesList { get => YTDCheckTaxesList; set { YTDCheckTaxesList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterListDetailResponse> YTDCheckTaxesList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailOtherCompResponse> currentOtherCompCheckList { get => CurrentOtherCompCheckList; set { CurrentOtherCompCheckList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailOtherCompResponse> CurrentOtherCompCheckList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailOtherCompResponse> ytdOtherCompCheckList { get => YtdOtherCompCheckList; set { YtdOtherCompCheckList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailOtherCompResponse> YtdOtherCompCheckList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailGrossResponse> currentGrossCheckList { get => CurrentGrossCheckList; set { CurrentGrossCheckList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailGrossResponse> CurrentGrossCheckList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailGrossResponse> ytdGrossCheckList { get => YtdGrossCheckList; set { YtdGrossCheckList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailGrossResponse> YtdGrossCheckList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailYTDTotalsResponse> currentAndYtdCheckTotalsList { get => CurrentAndYtdCheckTotalsList; set { CurrentAndYtdCheckTotalsList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailYTDTotalsResponse> CurrentAndYtdCheckTotalsList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailOtherVolResponse> currentOtherVolCheckList { get => CurrentOtherVolCheckList; set { CurrentOtherVolCheckList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailOtherVolResponse> CurrentOtherVolCheckList { get; set; }

        public ObservableCollection<GetCheckRegisterDetailOtherVolResponse> ytdOtherVolCheckList { get => YtdOtherVolCheckList; set { YtdOtherVolCheckList = value; OnPropertyChanged(); } }
        private ObservableCollection<GetCheckRegisterDetailOtherVolResponse> YtdOtherVolCheckList { get; set; }
        //Detail

        public string gross { get => Gross; set { Gross = value; OnPropertyChanged(); } }
        private string Gross { get; set; }
        private Uri PACheckRegisterReport { get; set; }
        public Uri paCheckRegisterReport { get => PACheckRegisterReport; set { PACheckRegisterReport = value; OnPropertyChanged(); } }
        public IAsyncCommand<object> GoToCheckRegisterReportCommand { get; set; }


        public CheckRegisterDetailViewModel()
        {
            Title = "Pay Detail";
            ytdCheckTitle = "YTD";
            currentCheckTitle = "Current";
            GoToCheckRegisterReportCommand = new AsyncCommand<object>((item) => GoCheckRegisterReport());

        }

        private GetAllResponse GetAllResponse { get; set; }

        public GetAllResponse getAllResponse
        {
            get => GetAllResponse;
            set
            {
                GetAllResponse = value;
                OnPropertyChanged();
            }
        }
        

        public async Task GoCheckRegisterReport()
        {
            GetCheckRegisterReportRequest selectedCheckRegisterReport = new GetCheckRegisterReportRequest();

            selectedCheckRegisterReport.Compid = checkRegisterDetailResponse.CompId;
            selectedCheckRegisterReport.EmployeeId = checkRegisterDetailResponse.EmpId;
            selectedCheckRegisterReport.CheckDate = checkRegisterDetailResponse.CheckDate;
            selectedCheckRegisterReport.NextCheckOrder = checkRegisterDetailResponse.NextCheckOrder;
            selectedCheckRegisterReport.CheckNumber = checkRegisterDetailResponse.CheckNumber;
            selectedCheckRegisterReport.BankId = checkRegisterDetailResponse.BankId;
           
            var model = JsonConvert.SerializeObject(selectedCheckRegisterReport);
            
            await Shell.Current.GoToAsync($"/{nameof(CheckRegisterReport)}?{nameof(CheckRegisterReportViewModel.content)}={model}");


        }
   
        public async Task<GetCheckRegisterDetailResponse> GetCheckRegisterDetail(GetCheckRegisterDetailRequest checkRegisterDetailRequest)
        {
            var data = await CheckRegisterService.GetCheckRegisterDetail(checkRegisterDetailRequest).ConfigureAwait(false);

            return data;
        }

        public async Task<GetCheckRegisterDetailResponse> GetCheckRegisterYTDDetail(GetCheckRegisterDetailRequest checkRegisterDetailRequest)
        {
            var data = await CheckRegisterService.GetSSPCheckRegisterYTDDetail(checkRegisterDetailRequest).ConfigureAwait(false);

            return data;
        }

        //Detail
        public async Task<ObservableCollection<GetCheckRegisterListDetailResponse>> GetMobileCheckRegisterTaxes(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterTaxes(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterListDetailResponse>> GetMobileCheckRegisterYTDTaxes(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterYTDTaxes(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailOtherCompResponse>> GetMobileCheckRegisterYTDOtherComp(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterYTDOtherComp(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailOtherCompResponse>> GetMobileCheckRegisterOtherComp(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterOtherComp(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }

        public async Task<ObservableCollection<GetCheckRegisterDetailGrossResponse>> GetMobileCheckRegisterYTDGross(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterYTDGross(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailGrossResponse>> GetMobileCheckRegisterGross(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterGross(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailYTDTotalsResponse>> GetMobileCheckRegisterTotalCurrentAndYTD(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterTotalCurrentAndYTD(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailOtherVolResponse>> GetMobileCheckRegisterOtherVol(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterOtherVol(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }
        public async Task<ObservableCollection<GetCheckRegisterDetailOtherVolResponse>> GetMobileCheckRegisterYTDOtherVol(string compId, string empId, string checkNumber)
        {
            var data = await CheckRegisterService.GetMobileCheckRegisterYTDOtherVol(compId, empId, checkNumber).ConfigureAwait(false);

            return data;
        }

        //Detail

        private string Content { get; set; }
        public string content
        {
            get => Content;
            set
            {
                Content = value;
                GetCheckRegisterDetailByData(value);
                OnPropertyChanged();
            }
        }

        public async void GetCheckRegisterDetailByData(string data)
        {
            getAllResponse = JsonConvert.DeserializeObject<GetAllResponse>(data);

            if (getAllResponse == null)
                return;

            GetCheckRegisterDetailRequest checkRegisterDetailRequest = new GetCheckRegisterDetailRequest();
            checkRegisterDetailRequest.Compid = getAllResponse.CompId;
            checkRegisterDetailRequest.EmployeeId = getAllResponse.EmpId;
            checkRegisterDetailRequest.CheckNumber = getAllResponse.CheckNumber;

            checkRegisterDetailResponse = await GetCheckRegisterDetail(checkRegisterDetailRequest).ConfigureAwait(false);

            //YTD
            GetCheckRegisterDetailRequest checkRegisterDetailRequestYTD = new GetCheckRegisterDetailRequest();
            checkRegisterDetailRequestYTD.Compid = getAllResponse.CompId;
            checkRegisterDetailRequestYTD.EmployeeId = getAllResponse.EmpId;
            checkRegisterDetailRequestYTD.CheckNumber = getAllResponse.CheckNumber;

            checkRegisterDetailResponseYTD = await GetCheckRegisterYTDDetail(checkRegisterDetailRequestYTD).ConfigureAwait(false);



            //Detail
            currentCheckTaxesList = await GetMobileCheckRegisterTaxes(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            currentAndYtdCheckTotalsList = await GetMobileCheckRegisterTotalCurrentAndYTD(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            currentOtherVolCheckList = await GetMobileCheckRegisterOtherVol(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            //currentOtherCompCheckList = await GetMobileCheckRegisterOtherComp(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            currentGrossCheckList = await GetMobileCheckRegisterGross(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);


            ytdCheckTaxesList = await GetMobileCheckRegisterYTDTaxes(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            ytdOtherVolCheckList = await GetMobileCheckRegisterYTDOtherVol(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            //ytdOtherCompCheckList = await GetMobileCheckRegisterYTDOtherComp(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);
            ytdGrossCheckList = await GetMobileCheckRegisterYTDGross(GetAllResponse.CompId, getAllResponse.EmpId, getAllResponse.CheckNumber).ConfigureAwait(false);

            //Detail
        }

    }
}
 


