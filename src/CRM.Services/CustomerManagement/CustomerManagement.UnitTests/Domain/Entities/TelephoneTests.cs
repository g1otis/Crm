using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Xunit;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.TelephoneType;

namespace CustomerManagement.UnitTests.Domain.Entities
{
    public class TelephoneTests
    {
        [Fact]
        public void CreateTelephone_Succeeds()
        {
            var telephone = new Telephone(TelephoneTypeId.Other, "+555", "587585");

            Assert.IsAssignableFrom<Telephone>(telephone);
        }

        [Fact]
        public void TelephoneType_GetAll_Succeeds()
        {
            Assert.Equal(4, TelephoneType.GetAll().Count());
        }

        [Fact]
        public void Equals_Succeeds()
        {
            var telephone1 = new Telephone(TelephoneTypeId.Other, "+555", "587585");
            var telephone2 = new Telephone(TelephoneTypeId.Other, "+555", "587585");
            var telephone3 = new Telephone(TelephoneTypeId.Home, "+1111", "888999");

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(telephone1 == telephone1);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.False(telephone1 == telephone2);
            Assert.True(telephone1.Equals(telephone2));
            Assert.True(telephone1 != telephone3);
            Assert.False(telephone1.Equals(telephone3));
        }

        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [Theory]
        public void CreateTelephone_InvalidValue_Throws(string invalidValue)
        {
            Assert.Throws<ValidationException>(() => new Telephone(TelephoneTypeId.Other, invalidValue, "587585"));
            Assert.Throws<ValidationException>(() => new Telephone(TelephoneTypeId.Other, "+555", invalidValue));
        }

        [InlineData("TexT")]
        [InlineData("te2e2te")]
        [Theory]
        public void CreateTelephone_InvalidPhoneFormat_Throws(string invalidPhone)
        {
            Assert.Throws<ValidationException>(() => new Telephone(TelephoneTypeId.Other, "+555", invalidPhone));
        }
    }
}
