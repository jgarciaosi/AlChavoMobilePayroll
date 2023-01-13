using System;

namespace AlChavoMobilePayroll.Models.BillRegister
{
    public class GetBillPaymentsResponse
    {
        public long EntryNum { get; set; }
        public string CompId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public long PmtId { get; set; }
        public string Check_Date { get; set; }
        public string Check_Number { get; set; }
        public string Payee_Id { get; set; }
        public string Payee_Name { get; set; }
        public decimal? Gross_Amount { get; set; }
        public decimal? P_S__Discount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax_Amount { get; set; }
        public decimal? Net_Amount { get; set; }
        public int Void { get; set; }
        public string Bank_Id { get; set; }
        public string Invoice_Number { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public string View_Bill { get; set; }
        public int Invoice_Count { get; set; }
        public long Trans_Id { get; set; }
    }
}
