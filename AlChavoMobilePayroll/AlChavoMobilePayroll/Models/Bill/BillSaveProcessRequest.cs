using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace AlChavoMobilePayroll.Models.Bill
{
    public class BillSaveProcessRequest : INotifyPropertyChanged
    {
        public string CompId { get; set; }
        public string UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string PayeeId { get; set; }
        public string BillNumber { get; set; }
        public DateTime  BillDate { get; set; }
        public string Description { get; set; }
        public Decimal  BillAmount { get; set; }
        public int TaxId { get; set; }
        public string GLExpense { get; set; }
        public bool STW { get; set; }
        public bool ReimburseEXP { get; set; }
        public bool ReviewBillYN { get; set; }
        public bool ApproveBillYN { get; set; }
        public bool PostBillYN { get; set; }
        public bool ScheduleBillYN { get; set; }
        public bool PayBillYN { get; set; }
        public string PaymentBankId { get; set; }
        public DateTime PaymentProcessDate { get; set; }
        public bool SendEmail { get; set; }
        public string EmailContact { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
