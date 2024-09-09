using FluentValidation;
using HMS.Models;

namespace HMS.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(p => p.Age)
                .NotEmpty().WithMessage("Age is required")
                .InclusiveBetween(0, 120).WithMessage("Age must be between 0 and 120.");

            RuleFor(p => p.ContactNumber)
                .NotEmpty().WithMessage("Contact number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Contact number must be between 10 and 15 digits.");

            RuleFor(p => p.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().When(p => !string.IsNullOrEmpty(p.Email))
                .WithMessage("Invalid email format.");

            RuleFor(p => p.Address)
                .NotNull().WithMessage("Address is required.")
                .SetValidator(new AddressValidator());

            RuleFor(p => p.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("DoctorId must be a valid GUID.");

            RuleFor(p => p.AdmissionDate)
                .NotEmpty().WithMessage("Admission date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Admission date cannot be in the future.");

            RuleFor(p => p.DischargeDate)
                .GreaterThan(p => p.AdmissionDate)
                .When(p => p.DischargeDate.HasValue)
                .WithMessage("Discharge date must be later than the admission date.");
        }
    }

}
