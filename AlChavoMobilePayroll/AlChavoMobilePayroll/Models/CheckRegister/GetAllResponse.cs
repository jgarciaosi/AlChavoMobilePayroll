using System;
using System.Collections.ObjectModel;
using Microcharts;
using SkiaSharp;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class GetAllResponse
    {
        public string CompId { get; set; }
        public string EmpId { get; set; }
        public DateTime CheckDate { get; set; }
        public string CheckNumber { get; set; }
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal? CheckHours { get; set; }
        public float NetPct { get; set; }
        public float GrossPct { get; set; }
        public DonutChart donutChart { get; set; }
        public ObservableCollection<ModelCheckRegisterLastPie> CheckRegisterIndexPieData { get; set; }
    }
}
