using FluentValidation;
using HMS.Models;

namespace HMS.Validators
{
    public class BillingValidator : AbstractValidator<Billing>
    {
        public BillingValidator()
        {
            
            RuleFor(b => b.PatientId)
                .NotEmpty().WithMessage("Patient Id is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Patient Id must be a valid GUID.");

            
            RuleFor(b => b.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Doctor Id must be a valid GUID.");

            
            RuleFor(b => b.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("Amount must be a non-negative value.")
                .NotEmpty().WithMessage("Amount is required.");

            
            RuleFor(b => b.BillingDate)
                .NotEmpty().WithMessage("Billing Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Billing Date cannot be in the future.");

            
        }
    }

}
