using Xunit;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using System.ComponentModel.DataAnnotations;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.Gender;
using System;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.TelephoneType;

namespace CustomerManagement.UnitTests.Domain.Entities
{
    public class CustomerAggregateTests
    {
        [Fact]
        public void CreateCustomer_Succeeds()
        {
            var customer1 = new Customer("firstName", "middle", "lastName", GenderId.Male, 19, "test@mail.com");
            var customer2 = new Customer("firstName", null, "lastName", GenderId.Male, 19, "test@mail.com");

            Assert.IsAssignableFrom<Customer>(customer1);
            Assert.IsAssignableFrom<Customer>(customer2);
        }

        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [Theory]
        public void CreateCustomer_InvalidFirstName_Throws(string firstName)
            => Assert.Throws<ValidationException>(() => new Customer(firstName, "middle", "lastName", GenderId.Male, 19, "test@mail.com"));

        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [Theory]
        public void CreateCustomer_InvalidLastName_Throws(string lastName)
            => Assert.Throws<ValidationException>(() => new Customer("firstName", "middle", lastName, GenderId.Male, 19, "test@mail.com"));

        [InlineData("")]
        [InlineData("   ")]
        [Theory]
        public void CreateCustomer_InvalidMiddleName_Throws(string middleName)
            => Assert.Throws<ValidationException>(() => new Customer("firstName", middleName, "lastName", GenderId.Male, 19, "test@mail.com"));

        [InlineData("")]
        [InlineData("   ")]
        [InlineData("1122323")]
        [InlineData("test.com")]
        [Theory]
        public void CreateCustomer_InvalidEmail_Throws(string email)
            => Assert.Throws<ValidationException>(() => new Customer("firstName", "middleName", "lastName", GenderId.Male, 19, email));

        [Fact]
        public void AddAddress_Succeeds()
        {
            var customer = new Customer("firstName", "middle", "lastName", GenderId.Male, 19, "test@mail.com");
            var address1 = new Address(AddressTypeId.Other, "a street name", "number", "122333", "manilla", "CYP");
            var address2 = new Address(AddressTypeId.Home, "a street name", "number", "122333", "manilla", "CYP");

            customer.AddAddress(address1);
            Assert.Single(customer.Addresses);

            customer.AddAddress(address2);
            Assert.True(customer.Addresses.Count == 2);
        }

        [Fact]
        public void AddAddress_TypeAlreadyExist_Throws()
        {
            var customer = new Customer("firstName", "middle", "lastName", GenderId.Male, 19, "test@mail.com");
            var address1 = new Address(AddressTypeId.Other, "a street name", "number", "122333", "manilla", "CYP");
            var address2 = new Address(AddressTypeId.Other, "b street name", "111", "4444", "wow", "CYP");

            customer.AddAddress(address1);
            Assert.Throws<InvalidOperationException>(() => customer.AddAddress(address2));
        }

        [Fact]
        public void AddTelephone_Succeeds()
        {
            var customer = new Customer("firstName", "middle", "lastName", GenderId.Male, 19, "test@mail.com");
            var telephone1 = new Telephone(TelephoneTypeId.Home, "+333", "23232323");
            var telephone2 = new Telephone(TelephoneTypeId.Other, "+333", "23232323");

            customer.AddTelephone(telephone1);
            Assert.Single(customer.Telephones);

            customer.AddTelephone(telephone2);
            Assert.True(customer.Telephones.Count == 2);
        }

        [Fact]
        public void AddTelephone_TypeAlreadyExist_Throws()
        {
            var customer = new Customer("firstName", "middle", "lastName", GenderId.Male, 19, "test@mail.com");
            var telephone1 = new Telephone(TelephoneTypeId.Home, "+333", "23232323");
            var telephone2 = new Telephone(TelephoneTypeId.Home, "+444", "636363636");

            customer.AddTelephone(telephone1);
            Assert.Throws<InvalidOperationException>(() => customer.AddTelephone(telephone2));
        }
    }
}
