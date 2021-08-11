using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomerManagement.Domain.SeedWork;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.TelephoneType;

namespace CustomerManagement.Domain.Aggregates.CustomerAggregate
{
    public class Telephone : ValueObject
    {
        public TelephoneType TelephoneType { get; }
        protected internal TelephoneTypeId TelephoneTypeId { get; }

        [Required]
        public string Extension { get; private set; }

        [Required]
        [Phone]
        public string Phone { get; private set; }

        public Telephone(TelephoneTypeId type, string extension, string phone)
        {
            TelephoneTypeId = type;
            Extension = extension;
            Phone = phone;

            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TelephoneTypeId;
            yield return Extension;
            yield return Phone;
        }
    }
}
