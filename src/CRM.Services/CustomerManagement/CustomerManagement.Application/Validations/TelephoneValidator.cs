using CustomerManagement.Application.Commands;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using FluentValidation;

namespace CustomerManagement.Application.Validations
{
    public class TelephoneValidator : AbstractValidator<TelephoneDto>
    {
        public TelephoneValidator()
        {
            RuleFor(t => t.Extension).NotEmpty();
            RuleFor(t => t.Phone).NotEmpty();
            RuleFor(t => t.TelephoneType).SetValidator(new TelephoneTypeValidator());
        }

        class TelephoneTypeValidator : AbstractValidator<TelephoneType>
        {
            public TelephoneTypeValidator()
            {
                RuleFor(tt => tt.Id).IsInEnum();
            }
        }
    }
}
