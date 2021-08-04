using System;
namespace CRM.API.Models
{
    public class CustomerRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
}
