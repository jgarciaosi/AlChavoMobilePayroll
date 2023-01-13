using FluentValidation;
using System;

namespace AlChavoMobilePayroll.Models.Bill
{
    public class BillSaveProcessRequestValidator : AbstractValidator<BillSaveProcessRequest>
    {




        public BillSaveProcessRequestValidator()
        {
            RuleFor(x => x.CompId).NotEmpty().WithMessage("CompId is Required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is Required");
            RuleFor(x => x.UserGuid).NotEmpty().WithMessage("UserGuid is Required");
            RuleFor(x => x.EmailContact).NotEmpty().When(x => x.SendEmail).WithMessage("PayeeEmailContact is Required");
            RuleFor(x => x.PayeeId).NotEmpty().WithMessage("PayeeId is Required");
            RuleFor(x => x.BillNumber).Must(IsValidName).WithMessage("Bill Number Cannot contain spaces").NotEmpty().WithMessage("Bill Number is Required").Length(1, 12).WithMessage("Bill Number Lenght is invalid");
            RuleFor(x => x.BillDate).NotEmpty().WithMessage("BillDate is Required").GreaterThan(DateTime.Today.AddDays(-1)).WithMessage("BillDate cannot be lower than today");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(x => x.BillAmount).NotEmpty().WithMessage("BillAmount is Required").NotEqual(0).WithMessage("BillAmount cannot be 0.00");
            RuleFor(x => x.TaxId).NotEmpty().WithMessage("Tax is Required");
            RuleFor(x => x.GLExpense).NotEmpty().WithMessage("Gl Expense is Required");

            // RuleFor(x => x.STW).NotEmpty().WithMessage("STW is Required");
            // RuleFor(x => x.ReimburseEXP).NotEmpty().WithMessage("ReimburseEXP is Required");
            // RuleFor(x => x.ReviewBillYN).NotEmpty().WithMessage("ReviewBillYN is Required");
            // RuleFor(x => x.ApproveBillYN).NotEmpty().WithMessage("ApproveBillYN is Required");
            // RuleFor(x => x.PostBillYN).NotEmpty().WithMessage("PostBillYN is Required");
            // RuleFor(x => x.PayBillYN ).NotEmpty().WithMessage("PayBillYN is Required");


            RuleFor(x => x.PaymentBankId).NotEmpty().When(x => x.PayBillYN).WithMessage("Payment Bank Id is Required");
            RuleFor(x => x.PaymentProcessDate).NotEmpty().When(x => x.PayBillYN).WithMessage("Payment Process Date is Required");

        }


        protected bool IsValidName(string Name)
        {
            return Name.Contains(" ") ? false : true;
        }
    }




}
