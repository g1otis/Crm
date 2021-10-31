using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            RuleFor(t => t.Phone).NotEmpty().Must((dto, phone, valCtx) =>
            {
                var phoneAttribute = new PhoneAttribute();
                if (phoneAttribute.IsValid(phone))
                {
                    return true;
                }

                const string PropertyName = nameof(dto.Phone);

                valCtx.AddFailure(new FluentValidation.Results.ValidationFailure(PropertyName, phoneAttribute.FormatErrorMessage(PropertyName), phone));

                return false;
            });
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
