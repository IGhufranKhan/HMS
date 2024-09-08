using FluentValidation;
using HMS.Models;

namespace HMS.Validators
{
    public class AddressValidator : AbstractValidator<Address?>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("Street is required.");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("City is required.");
            RuleFor(a => a.State)
                .NotEmpty().WithMessage("State is required.");
            RuleFor(a => a.Country)
               .NotEmpty().WithMessage("Country is required.");
            RuleFor(a => a.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .Matches(@"^\d{5,10}$").WithMessage("ZipCode must be between 5 and 10 digits.");
        }
    }

}
