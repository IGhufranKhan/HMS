using FluentValidation;
using HMS.Models;

namespace HMS.Validators
{
    internal sealed class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(d => d.HeadOfDepartment)
                .NotEmpty().WithMessage("Head Of Department is required.")
                .MaximumLength(100).WithMessage("HeadOfDepartment cannot exceed 100 characters.");

            RuleFor(d => d.ContactNumber)
                .NotEmpty().WithMessage("Contact Number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

            RuleForEach(d => d.Doctors)
                .SetValidator(new DoctorValidator())
                .When(d => d.Doctors != null && d.Doctors.Count > 0);
        }
    }
}
