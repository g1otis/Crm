using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomerManagement.Domain.SeedWork;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.Gender;

namespace CustomerManagement.Domain.Aggregates.CustomerAggregate
{
    public class Customer : EntityBase, IAggregateRoot, IValidatableObject
    {
        #region Properties
        [Required]
        public string FirstName { get; } = null!;

        public string? MiddleName { get; }

        [Required]
        public string LastName { get; } = null!;

        public Gender Gender { get; }
        private GenderId _genderId;

        [Range(18, 120)]
        public int Age { get; set; }

        [EmailAddress]
        public string EmailAddress { get; } = null!;

        private readonly List<Address> _addresses;

        private readonly List<Telephone> _telephones;
        #endregion

        #region Calculated Properties
        public string FullName => string.Join(" ", new List<string> { FirstName, MiddleName!, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));
        public IReadOnlyList<Address> Addresses => _addresses;
        public IReadOnlyList<Telephone> Telephones => _telephones;
        #endregion

        private Customer()
        {
            _addresses = new List<Address>();
            _telephones = new List<Telephone>();
        }

        public Customer(string firstName, string? middleName, string lastName, GenderId gender, int age, string emailAddress) : this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            _genderId = gender;
            Age = age;
            EmailAddress = emailAddress;

            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

        public Address AddAddress(Address address)
        {
            if (_addresses.SingleOrDefault(a => a.AddressTypeId == address.AddressTypeId) is { })
            {
                throw new InvalidOperationException($"Could not add address, address type({new AddressType(address.AddressTypeId).Name}) already exists");
            }

            _addresses.Add(address);

            //AddDomainEvent()

            return address;
        }

        public Telephone AddTelephone(Telephone telephone)
        {
            if (_telephones.SingleOrDefault(a => a.TelephoneTypeId == telephone.TelephoneTypeId) is { })
            {
                throw new InvalidOperationException($"Could not add telephone, telephone type({new TelephoneType(telephone.TelephoneTypeId).Name}) already exists");
            }

            _telephones.Add(telephone);

            //AddDomainEvent()

            return telephone;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MiddleName != null && MiddleName.ToCharArray().All(char.IsWhiteSpace))
            {
                yield return new ValidationResult($"{nameof(MiddleName)} could not be white space");
            }
        }
    }
}
