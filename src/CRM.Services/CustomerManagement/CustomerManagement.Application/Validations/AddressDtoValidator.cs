using CustomerManagement.Application.Commands;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using FluentValidation;

namespace CustomerManagement.Application.Validations
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(a => a.AddressType).SetValidator(new AddressTypeValidator());
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.CountryISO3).NotNull().Length(3);
            RuleFor(a => a.PostalCode).NotEmpty();
            RuleFor(a => a.StreetName).NotEmpty();
            RuleFor(a => a.StreetNumber).NotEmpty();

        }

        class AddressTypeValidator : AbstractValidator<AddressType>
        {
            public AddressTypeValidator()
            {
                RuleFor(at => at.Id).IsInEnum();
            }
        }
    }
}
