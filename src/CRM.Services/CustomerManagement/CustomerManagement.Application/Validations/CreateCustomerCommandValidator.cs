using CustomerManagement.Application.Commands;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerManagement.Application.Validations
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            var addressValidator = new AddressDtoValidator();
            var telephoneValidator = new TelephoneValidator();

            RuleFor(c => c.Addresses).ForEach(a => a.SetValidator(addressValidator));
            RuleFor(c => c.Age).GreaterThan(0);
            RuleFor(c => c.EmailAddress).EmailAddress();
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.Gender).SetValidator(new GenderValidator());
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Telephones).ForEach(t => t.SetValidator(telephoneValidator));
        }
    }
}
