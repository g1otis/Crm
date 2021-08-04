using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomerManagement.Domain.SeedWork;
using static CustomerManagement.Domain.Entities.TelephoneType;

namespace CustomerManagement.Domain.Entities
{
    public class Telephone : ValueObject
    {
        public TelephoneType TelephoneType { get; }
        public TelephoneTypeId TelephoneTypeId { get; }

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
