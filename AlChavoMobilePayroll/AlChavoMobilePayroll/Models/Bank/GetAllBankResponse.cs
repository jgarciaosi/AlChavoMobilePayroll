using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Bank
{
   public class GetAllBankResponse
    {
        public int EntryNum { get; set; }
        public string CompId { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string BankContactName { get; set; }
        public string BankPhone1 { get; set; }
        public string BankPhone2 { get; set; }
        public string BankAddress1 { get; set; }
        public string BankAddress2 { get; set; }
        public string BankCity { get; set; }
        public string BankState { get; set; }
        public string BankZipCode { get; set; }
        public string AcctNumber { get; set; }
        public string RTNumber { get; set; }
        public int SortOrder { get; set; }
        public string GlCashAcct { get; set; }
        public int ElectPmtApprove { get; set; }
        public int ElectCollectionApprove { get; set; }
        public int ApCheckNumSeq { get; set; }
        public int PaCheckNumSeq { get; set; }
        public int AfiliateBankYN { get; set; }
        public int InactiveAP { get; set; }
        public int InactiveAr { get; set; }
        public int InactivePa { get; set; }
        public int InactiveGl { get; set; }
        public int InactiveBr { get; set; }
        public int FactAcctYN { get; set; }
        public string AccountDescription { get; set; }
        public int LastBlankCheck { get; set; }
        public string ApCheckBankName { get; set; }
        public string ApCheckBankAddress1 { get; set; }
        public string ApCheckBankAddress2 { get; set; }
        public string ApCheckBankCity { get; set; }
        public string ApCheckBankState { get; set; }
        public string ApCheckBankZipCode { get; set; }
        public int DfltPaDDFileOffsetYN { get; set; }
        public int PaDDBankProcessCompletedYN { get; set; }
        public string EIN { get; set; }
        public int ApEDIOffsetYN { get; set; }
        public int SupposedToBeReconciledYN { get; set; }
        public int ArUseAccountForPaytech { get; set; }
        public int ShowAccountInDropdown { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Guid CreatedUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedUserID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FullDesc { get; set; }
        public string AcctMask { get; set; }
    }
}
