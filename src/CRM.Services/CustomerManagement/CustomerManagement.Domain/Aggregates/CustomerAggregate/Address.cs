using System.ComponentModel.DataAnnotations;
using CustomerManagement.Domain.SeedWork;
using System.Collections.Generic;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType;

namespace CustomerManagement.Domain.Aggregates.CustomerAggregate
{
    public class Address : ValueObject
    {
        public AddressType AddressType { get; }
        protected internal AddressTypeId AddressTypeId { get; }

        [Required]
        public string StreetName { get; }

        [Required]
        public string StreetNumber { get; }

        [Required]
        public string PostalCode { get; }

        [Required]
        public string City { get; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string CountryISO3 { get; }

        public Address(AddressTypeId addressTypeId, string streetName, string streetNumber, string postalCode, string city, string countryISO3)
        {
            AddressTypeId = addressTypeId;
            StreetName = streetName;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            CountryISO3 = countryISO3;

            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AddressTypeId;
            yield return StreetName;
            yield return StreetNumber;
            yield return PostalCode;
            yield return City;
            yield return CountryISO3;
        }
    }
}
