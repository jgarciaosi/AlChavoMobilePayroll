using FluentValidation;

namespace AlChavoMobilePayroll.Models.Attendance
{
    public class LicenseRequestValidator : AbstractValidator<LicenseRequest>
    {
        public LicenseRequestValidator()
        {
            RuleFor(x => x.CompId).NotEmpty().WithMessage("CompId is Required");
            RuleFor(x => x.EmpId).NotEmpty().WithMessage("EmpId is Required");
            RuleFor(x => x.EmployeeEntryNum).NotEmpty().WithMessage("Employee Entry Num is Required");
            RuleFor(x => x.DepartmentID).NotEmpty().WithMessage("Department ID is Required");
            RuleFor(x => x.RequestDateFrom).NotEmpty().WithMessage("Date From is Required");
            RuleFor(x => x.RequestDateTo).NotEmpty().WithMessage("Date To is Required");
            RuleFor(x => x.RequestHours).NotEmpty().WithMessage("Hours are Required").GreaterThan(0).WithMessage ("Hours cannot be 0");
            RuleFor(x => x.RequestLicenseEarning).NotEmpty().WithMessage("License Earning are Required");
            RuleFor(x => x.RequestComments).NotEmpty().WithMessage("Comments are Required");
            RuleFor(x => x.RequestDateRequested).NotEmpty().WithMessage("Date Requested is Required");

        }
    }
    }
