using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using FluentValidation;

namespace CustomerManagement.Application.Validations
{
    public class GenderValidator : AbstractValidator<Gender>
    {
        public GenderValidator()
        {
            RuleFor(g => g.Id).IsInEnum();
        }
    }
}
