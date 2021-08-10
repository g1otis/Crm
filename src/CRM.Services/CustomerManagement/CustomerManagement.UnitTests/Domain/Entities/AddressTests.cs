using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Xunit;

namespace CustomerManagement.UnitTests.Domain.Entities
{
    public class AddressTests
    {
        [Fact]
        public void CreateAddress_Succeeds()
        {
            var address = new Address(AddressType.AddressTypeId.Other, "street name", "street number", "postal", "city", "GRE");

            Assert.IsAssignableFrom<Address>(address);
        }

        [Fact]
        public void AddressType_GetAll_Succeeds()
        {
            Assert.Equal(3, AddressType.GetAll().Count());
        }

        [Fact]
        public void Equals_Succeeds()
        {
            var address1 = new Address(AddressType.AddressTypeId.Other, "street name", "street number", "postal", "city", "GRE");
            var address2 = new Address(AddressType.AddressTypeId.Other, "street name", "street number", "postal", "city", "GRE");
            var address3 = new Address(AddressType.AddressTypeId.Home, "street", "number", "postal", "city", "GRE");

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(address1 == address1);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.False(address1 == address2);
            Assert.True(address1.Equals(address2));
            Assert.True(address1 != address3);
            Assert.False(address1.Equals(address3));
        }

        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [Theory]
        public void CreateAddress_InvalidValue_Throws(string invalidValue)
        {
            Assert.Throws<ValidationException>(() => new Address(AddressType.AddressTypeId.Other, invalidValue, "test", "test", "test", "CYP"));
            Assert.Throws<ValidationException>(() => new Address(AddressType.AddressTypeId.Other, "test", invalidValue, "test", "test", "CYP"));
            Assert.Throws<ValidationException>(() => new Address(AddressType.AddressTypeId.Other, "test", "test", invalidValue, "test", "CYP"));
            Assert.Throws<ValidationException>(() => new Address(AddressType.AddressTypeId.Other, "test", "test", "test", invalidValue, "CYP"));
            Assert.Throws<ValidationException>(() => new Address(AddressType.AddressTypeId.Other, "test", "test", "test", "test", invalidValue));
        }

        [InlineData("ENGL")]
        [InlineData("EN")]
        [Theory]
        public void CreateAddress_InvalidCountryLength_Throws(string invalidCountry)
        {
            Assert.Throws<ValidationException>(() => new Address(AddressType.AddressTypeId.Other, "test", "test", "test", "test", invalidCountry));
        }
    }
}
