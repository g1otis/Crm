using FluentValidation;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.Gender;

namespace CustomerManagement.Application.Validations
{
    public class GenderValidator : AbstractValidator<GenderId>
    {
        public GenderValidator()
        {
            RuleFor(gId => gId).IsInEnum();
        }
    }
}
