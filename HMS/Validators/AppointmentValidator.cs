using FluentValidation;
using HMS.Models;

namespace HMS.Validators
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.PatientId)
                .NotEmpty().WithMessage("PatientId is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("PatientId must be a valid GUID.");

            RuleFor(a => a.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("DoctorId must be a valid GUID.");
            
            RuleFor(a => a.AppointmentDate)
                .NotEmpty().WithMessage("AppointmentDate is required.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("AppointmentDate cannot be in the past.");

            RuleFor(a => a.Purpose)
                .NotEmpty().WithMessage("Purpose is required.")
                .MaximumLength(250).WithMessage("Purpose cannot exceed 250 characters.");

        }
    }

}
