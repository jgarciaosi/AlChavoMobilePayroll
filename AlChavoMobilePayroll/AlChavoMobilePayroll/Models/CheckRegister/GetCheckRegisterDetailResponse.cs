using Microcharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetCheckRegisterDetailResponse
    {
        public string CompId { get; set; }
        public string EmpId { get; set; }
        public string CheckNumber { get; set; }
        public DateTime CheckDate { get; set; }
        public string BankId { get; set; }
        public int NextCheckOrder { get; set; }
        public float Net { get; set; }
        public float NetPct { get; set; }
        public float Gross { get; set; }
        public float GrossPct { get; set; }
        public float TaxesAmount { get; set; }
        public float TaxesPct { get; set; }
        public float OtherVolWithholdPct { get; set; }
        public float OtherVolWithholdAmt { get; set; }
        public float OtherCompensationPct { get; set; }
        public float OtherCompensationAmt { get; set; }
        public DonutChart donutChartCheckRegisterDetail { get; set; }
        public ObservableCollection<ModelCheckRegisterLastPie> CheckPieData { get; set; }
        public float? CheckHours { get; set; }

        public string HomeGross { get; set; }

        public string HomeNet { get; set; }
    }
}
