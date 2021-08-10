using System.Linq;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Xunit;

namespace CustomerManagement.UnitTests.Domain.Entities
{
    public class GenderEnumerationTests
    {
        [Theory]
        [InlineData(Gender.GenderId.NotSet, "Not set")]
        [InlineData(Gender.GenderId.Male, "Male")]
        [InlineData(Gender.GenderId.Female, "Female")]
        public void Create_Succeeds(Gender.GenderId genderOptions, string expectedName)
        {
            var a = new Gender(genderOptions);

            Assert.Equal(genderOptions, a.Id);
            Assert.Equal(expectedName, a.Name);
        }

        [Fact]
        public void All_Succeeds()
        {
            var all = Gender.GetAll();
            Assert.Equal(3, all.Count());
        }

        [Fact]
        public void Comparisons_Succeeds()
        {
            var femaleCreatedNow = new Gender(Gender.GenderId.Female);

            Assert.True(Gender.Female.CompareTo(Gender.Female) == 0);
            Assert.True(Gender.Female.CompareTo(femaleCreatedNow) == 0);

            Assert.True(Gender.Female.CompareTo(Gender.Male) > 0);
            Assert.True(femaleCreatedNow.CompareTo(Gender.Male) > 0);

            Assert.True(Gender.Female.Equals(Gender.Female));
            Assert.True(Gender.Female.Equals(femaleCreatedNow));

            Assert.False(Gender.Female.Equals(Gender.Male));
            Assert.False(Gender.Female.Equals(null));
            Assert.False(Gender.Female.Equals(string.Empty));

            Assert.False(femaleCreatedNow.Equals(Gender.Male));
            Assert.False(femaleCreatedNow.Equals(null));
            Assert.False(femaleCreatedNow.Equals(string.Empty));
        }
    }
}
