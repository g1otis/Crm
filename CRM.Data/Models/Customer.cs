using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Data.Common;
using CRM.Data.Models.Enums;
using static CRM.Data.Constants.CustomerConstants;

namespace CRM.Data.Models
{
    public class Customer : ModelBase<Guid>
    {
        private string firstName;
        private string lastName;

        [Required, MinLength(FirstNameMinSize), MaxLength(FirstNameMaxSize)]
        public string FirstName { get => firstName; set => firstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }

        [MinLength(MiddleNameMinSize), MaxLength(MiddleNameMaxSize)]
        public string MiddleName { get; set; }

        [Required, MinLength(LastNameMinSize), MaxLength(LastNameMaxSize)]
        public string LastName { get => lastName; set => lastName = value ?? throw new ArgumentNullException(nameof(LastName)); }

        public GenderOptions Gender { get; set; }

        [Range(AgeMinSize, AgeMaxSize)]
        public int Age { get; set; }

        [MinLength(EmailAddressMinSize), MaxLength(EmailAddressMaxSize)]
        public string EmailAddress { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Telephone> Telephones { get; set; }
    }
}
