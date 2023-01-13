using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Payee
{
    public class PayeeGetDefaultsResponse
    {
        public string HeaderDesc { get; set; }
        public Nullable<int> TermsCode { get; set; }
        public string TermsCodeDesc { get; set; }
        public int DiscDays { get; set; }
        public int NetDueDays { get; set; }
        public string GLControl { get; set; }
        public string GLControlMask { get; set; }
        public string TaxState { get; set; }
        public string ItemId { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> PricePerUnit { get; set; }
        public string GlExpenseAcct { get; set; }
        public string GlExpenseMask { get; set; }
        public string GlDiscountAcct { get; set; }
        public string GlDiscountMask { get; set; }
        public Nullable<int> PS { get; set; }
        public string GlProfServAcct { get; set; }
        public string GlProfServMask { get; set; }
        public string JobId { get; set; }
        public string PhaseId { get; set; }
        public Nullable<int> Period { get; set; }
        public Nullable<int> FiscalYear { get; set; }
        public string UnitDescription { get; set; }
        public string PayeeId { get; set; }
        public string UsePayeGlAcctYN { get; set; }
        public string GlDescr { get; set; }
    }
}
