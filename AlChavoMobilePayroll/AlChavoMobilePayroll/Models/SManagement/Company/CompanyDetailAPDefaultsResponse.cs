using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.SManagement.Company
{
   public class CompanyDetailAPDefaultsResponse
    { 
        public string CompID { get; set; }
        public string DfltApBankId { get; set; }
        public string DfltApItemId { get; set; }
        public string DfltApGlControl { get; set; }
        public string DfltApGlCashAccount { get; set; }
        public string DfltApPayeeGlAcct { get; set; }
        public string DfltApGlDiscAccount { get; set; }
        public string DfltApGlProfServ { get; set; }
        public string DftApTaxAcct { get; set; }
        public string DfltApTermsId { get; set; }
        public string DfltApJobId { get; set; }
        public string DfltApPhase { get; set; }
        public int TwoSignatureRequire { get; set; }
        public int TwoSignatureRequireAP { get; set; }
        public string CompanyStorageDrive { get; set; }
    }

}
