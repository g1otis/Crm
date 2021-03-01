using System;
using System.ComponentModel.DataAnnotations;
using CRM.Data.Common;
using CRM.Data.Enums;
using static CRM.Data.Constants.AddressConstants;

namespace CRM.Data.Models
{
    public class Address : ModelBase<Guid>
    {
        private string countryISO3;
        private string postalCode;
        private string city;

        public AddressType Type { get; set; }

        [Required, MinLength(StreetNameMinSize), MaxLength(StreetNameMaxSize)]
        public string StreetName { get; set; }

        [Required, MinLength(StreetNumberMinSize), MaxLength(StreetNameMaxSize)]
        public string StreetNumber { get; set; }

        [Required, MinLength(PostalCodeMinSize), MaxLength(PostalCodeMaxSize)]
        public string PostalCode { get => postalCode; set => postalCode = value ?? throw new ArgumentNullException(nameof(PostalCode)); }

        [Required, MinLength(CityMinSize), MaxLength(CityMaxSize)]
        public string City { get => city; set => city = value ?? throw new ArgumentNullException(nameof(City)); }

        [Required, MinLength(CountryISO3MinSize), MaxLength(CountryISO3MaxSize)]
        public string CountryISO3 { get => countryISO3; set => countryISO3 = value ?? throw new ArgumentNullException(nameof(CountryISO3)); }

        #region Relations
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        #endregion
    }
}
