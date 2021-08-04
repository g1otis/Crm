using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CRM.UI.Models
{

    public class CustomerRegistrationModel : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ela re paikti den exeis epi8eto??")]
        public string LastName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [MinLength(8)]
        public string Password { get; set; }

        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var anyNumberIncluded = Enumerable.Range(0, 9).Select(number => number.ToString()).Any(Password.Contains);

            if (!anyNumberIncluded)
            {
                yield return new ValidationResult("Password should contain numbers", new List<string>{ nameof(Password) });
            }
        }
    }
}
