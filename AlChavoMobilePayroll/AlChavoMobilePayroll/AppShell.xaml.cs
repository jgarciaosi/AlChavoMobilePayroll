using AlChavoMobilePayroll.ViewModels;
using System;
using System.Collections.Generic;
using AlChavoMobilePayroll.Views.SManagement;
using AlChavoMobilePayroll.Views.Company;
using AlChavoMobilePayroll.Views.User;
using AlChavoMobilePayroll.Views.Attendance;
using AlChavoMobilePayroll.Views;
using AlChavoMobilePayroll.Views.CheckRegister;
using Xamarin.Forms;
using AlChavoMobilePayroll.Views.DownloadDocuments;

namespace AlChavoMobilePayroll
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            //Login
            Routing.RegisterRoute(nameof(Login), typeof(Login));

            //User
            Routing.RegisterRoute(nameof(UserIndex), typeof(UserIndex));
            Routing.RegisterRoute(nameof(UserDetail), typeof(UserDetail));
            Routing.RegisterRoute(nameof(Punch), typeof(Punch));

            //Company
            Routing.RegisterRoute(nameof(CompanyDetail), typeof(CompanyDetail));

            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            //Routing.RegisterRoute(nameof(Home), typeof(Home));

            Routing.RegisterRoute(nameof(CheckRegisterIndex), typeof(CheckRegisterIndex));
            Routing.RegisterRoute(nameof(CheckRegisterDetail), typeof(CheckRegisterDetail));
            Routing.RegisterRoute(nameof(CheckRegisterReport), typeof(CheckRegisterReport));

            //TimeOff - License Request
            Routing.RegisterRoute(nameof(TimeOff), typeof(TimeOff));
            Routing.RegisterRoute(nameof(TimeOffReport), typeof(TimeOffReport));
            Routing.RegisterRoute(nameof(TimeOffReportDetail), typeof(TimeOffReportDetail));
            Routing.RegisterRoute(nameof(DocumentsIndex), typeof(DocumentsIndex));

            //Attendance
            Routing.RegisterRoute(nameof(TimeCardEdit), typeof(TimeCardEdit));
            Routing.RegisterRoute(nameof(Schedule), typeof(Schedule));
        }

    }
}
