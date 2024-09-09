using FluentValidation;
using HMS.Models;

namespace HMS.Validators
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(d => d.Specialization)
                .NotNull().WithMessage("Speciallization is required");
            RuleFor(d => d.Experience)
                .NotEmpty().WithMessage("Experience is required")
                .GreaterThanOrEqualTo(0).WithMessage("Experience must be a non-negative integer.");

            
            RuleFor(d => d.ContactNumber)
                .NotEmpty().WithMessage("Contact number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Contact number must be between 10 and 15 digits.");

            
            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().When(d => !string.IsNullOrEmpty(d.Email))
                .WithMessage("Invalid email format.");

            
            RuleFor(d => d.DepartmentId)
                .NotEmpty().WithMessage("Department Id is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Department Id must be a valid GUID.");
        }
    }

}
