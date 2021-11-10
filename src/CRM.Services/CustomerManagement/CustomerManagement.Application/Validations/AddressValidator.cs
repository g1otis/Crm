using CustomerManagement.Application.Commands;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using FluentValidation;

namespace CustomerManagement.Application.Validations
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(a => a.AddressType).SetValidator(new AddressTypeValidator());
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.CountryIso3).NotNull().Length(3);
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
