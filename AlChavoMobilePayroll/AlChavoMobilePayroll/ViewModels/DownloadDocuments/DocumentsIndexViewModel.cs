using AlChavoMobilePayroll.Helpers;
using AlChavoMobilePayroll.Models.DownloadDocuments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net.Http;
using System.IO;
using AlChavoMobilePayroll.Models.Attendance;
using AlChavoMobilePayroll.Models.Employee;
using AlChavoMobilePayroll.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Syncfusion.DataSource.Extensions;

namespace AlChavoMobilePayroll.ViewModels.DownloadDocuments
{
    public class DocumentsIndexViewModel : Common.BaseViewModel
    {

        //private ObservableCollection<Grouping<string, DocumentResponse>> gDocumentList;
        //public ObservableCollection<Grouping<string, DocumentResponse>> GDocumentList { get => gDocumentList; set { gDocumentList = value; OnPropertyChanged(); } }

        private List<GetMobileOptionalFieldsResponse> mobileOptionalFieldsResponseList { get; set; }
        private List<GetMobileOptionalFieldsResponse> documentsList;
        private List<GetMobileOptionalFieldsResponse> optionalFieldList;

        private bool isVisibleOptionalFields;
        public bool IsVisibleOptionalFields { get => isVisibleOptionalFields; set { isVisibleOptionalFields = value; OnPropertyChanged(); } }

        public List<GetMobileOptionalFieldsResponse> MobileOptionalFieldsResponseList { get => MobileOptionalFieldsResponseList; set { MobileOptionalFieldsResponseList = value; OnPropertyChanged(); } }

        public List<GetMobileOptionalFieldsResponse> DocumentsList { get => documentsList; set { documentsList = value; OnPropertyChanged(); } }

        public List<GetMobileOptionalFieldsResponse> OptionalFieldList { get => optionalFieldList; set { optionalFieldList = value; OnPropertyChanged(); } }

        public IAsyncCommand<object> SelectedDocumentCommand { get; set; }


        public DocumentsIndexViewModel()
        {

            Title = "Download Documents";
            SelectedDocumentCommand = new AsyncCommand<object>((item) => GetSelectedDocument(item));

        }




        public async Task LoadDocuments()
        {
            isBusy = true;
            string compId = await SecureStorage.GetAsync("CompId");
            string empId = await SecureStorage.GetAsync("EmpId");


            var IsVisibleOptional = await SecureStorage.GetAsync("PAMobileOptionalFieldsYN");
            if (IsVisibleOptional != null)
            {
                IsVisibleOptionalFields = (Int32.Parse(IsVisibleOptional) == 1) ? true : false;
            }


            if (IsVisibleOptionalFields)
                mobileOptionalFieldsResponseList = await ATGetMobileEmployeeSchedules(compId, empId);


            //  GetGroupedDocumentList();
            GetDocumentsperTypeList(mobileOptionalFieldsResponseList);
            isBusy = false;
        }

        public async Task<List<GetMobileOptionalFieldsResponse>> ATGetMobileEmployeeSchedules(string compId, string empID)
        {
            return await EmployeeService.PAGetMobileOptionalFields(compId, empID).ConfigureAwait(false);
        }

        private void GetDocumentsperTypeList(List<GetMobileOptionalFieldsResponse> mobileOptionalFieldsResponseList2)
        {
            List<GetMobileOptionalFieldsResponse> responseList = new List<GetMobileOptionalFieldsResponse>();
            responseList.Add(new GetMobileOptionalFieldsResponse() { DocumentCategoryID = 1, Field = "Formulario W2", FileName = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
            responseList.Add(new GetMobileOptionalFieldsResponse() { DocumentCategoryID = 1, Field = "Formulario 499", FileName = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
            responseList.Add(new GetMobileOptionalFieldsResponse() { DocumentCategoryID = 1, Field = "Cambio de Plan Medico", FileName = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
            responseList.Add(new GetMobileOptionalFieldsResponse() { DocumentCategoryID = 1, Field = "Cambio de Plan de Retiro", FileName = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
            responseList.Add(new GetMobileOptionalFieldsResponse() { DocumentCategoryID = 2, Field = "Copy Birth Certificate", FileName = "Copy_Birth_Certificate.pdf" });


            var FilteredDocumentList = responseList.Where(x => x.DocumentCategoryID.Equals(1)).ToList();
            DocumentsList = new List<GetMobileOptionalFieldsResponse>();
            DocumentsList = FilteredDocumentList;


            var FilteredOptionalFieldList = responseList.Where(x => x.DocumentCategoryID.Equals(2)).ToList();
            OptionalFieldList = new List<GetMobileOptionalFieldsResponse>();
            OptionalFieldList = FilteredOptionalFieldList;

        }


        private async Task GetSelectedDocument(object item)
        {

            var downloadedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "file2.pdf");
            string CompID = await SecureStorage.GetAsync("CompId");
            var labelItem = item as Label;
            string linkNavigate;


            isBusy = true;

            linkNavigate = await GetMobileDownloadLink(CompID, labelItem.Text).ConfigureAwait(false);

            if (!String.IsNullOrEmpty(linkNavigate))
                await Launcher.OpenAsync(linkNavigate);
            else
                Device.BeginInvokeOnMainThread(async () => { ShowACToastMessage("File Not Found", true); });




            isBusy = false;



        }

        public async Task<string> GetMobileDownloadLink(string compId, string fileName)
        {
            var data = await CompanyService.GetMobileDownloadLink(compId, fileName).ConfigureAwait(false);
            return data;

        }



        //public ObservableCollection<Grouping<string, DocumentResponse>> GetGroupedDocumentList()
        //{
        //    List<DocumentResponse> MyDocuments = new List<DocumentResponse>();

        //    ///============ Esto de Reemplaza por un GET del REST============
        //    MyDocuments.Add(new DocumentResponse() { DocumentCategoryID = 1, DocumentCategoryName = "Documents", DocumentName = "Formulario W2", DocumentPath = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
        //    MyDocuments.Add(new DocumentResponse() { DocumentCategoryID = 1, DocumentCategoryName = "Documents", DocumentName = "Formulario 499", DocumentPath = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
        //    //  MyDocuments.Add(new DocumentResponse() { DocumentCategoryID = 1, DocumentCategoryName = "Documents", DocumentName = "Cambio de Plan Medico", DocumentPath = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });
        //    MyDocuments.Add(new DocumentResponse() { DocumentCategoryID = 1, DocumentCategoryName = "Documents", DocumentName = "Cambio de Plan de Retiro", DocumentPath = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });

        //    foreach (var itemDocumentResponse in mobileOptionalFieldsResponseList)
        //    {
        //        MyDocuments.Add(new DocumentResponse() { DocumentCategoryID = 2, DocumentCategoryName = "Optional Fields", DocumentName = itemDocumentResponse.Field, DocumentPath = itemDocumentResponse.FileName });
        //    }

        //    //MyDocuments.Add(new DocumentResponse() { DocumentCategoryID = 2, DocumentCategoryName = "Other", DocumentName = "Custom Name 1", DocumentPath = "PA_OSI_Field1_EmpId_RosarioAnabelleA.pdf" });

        //    //================================================================


        //    var sorted = from f in MyDocuments
        //                 orderby f.DocumentCategoryName
        //                 group f by f.DocumentCategoryName.ToString()
        //                 into TheGroup
        //                 select new Grouping<string, DocumentResponse>(TheGroup.Key, TheGroup);

        //    return new ObservableCollection<Grouping<string, DocumentResponse>>(sorted);
        //}
    }
}



