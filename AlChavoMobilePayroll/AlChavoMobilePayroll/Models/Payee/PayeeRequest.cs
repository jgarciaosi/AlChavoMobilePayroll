using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.Payee
{
    public class PayeeRequest
    {

        public string CompId { get; set; }
        public string PayeeId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Memo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? Ten99YN { get; set; }
        public string EIN { get; set; }
        public int? DeliveryInstructions { get; set; }
        public string DeliveryInstructionName { get; set; }
        public float OpenBalance { get; set; }
        public string GlAcct { get; set; }
        public string WaiverNum { get; set; }
        public int? TermsId { get; set; }
        public string TermsDescription { get; set; }
        public string GlAcctMaskCombination { get; set; }
        public int? Ten99FormCode { get; set; }
        public string Ten99FormCodeName { get; set; }
        public int? WithholdPct { get; set; }
    }

}
