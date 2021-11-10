using System.Linq;
using CustomerManagement.Infrastructure;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace CustomerManagement.UnitTests.Infrastructure
{
    public class CustomerManagementContextTests
    {
        private readonly CustomerManagementContext context;

        public CustomerManagementContextTests()
        {
            var options = new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;

            context = new CustomerManagementContext(options);
        }

        [Fact]
        public void EnsureCreated_Succeeds()
        {
            context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddCustomer_Succeeds()
        {
            context.Database.EnsureCreated();

            await context.Genders.AddRangeAsync(new Gender[]
            {
                new Gender(Gender.GenderId.NotSet),
                new Gender(Gender.GenderId.Male),
                new Gender(Gender.GenderId.Female),
            });

            context.Customers.Add(new Customer("emma", null, "esmna", Gender.GenderId.Male, 88, "mailemma@sfsf.com"));
            var count = context.SaveChanges();

            var c = context.Customers.Include(c => c.Gender).First();
            Assert.NotEqual(default, c.Id);
        }

        [Fact]
        public void AddAddressType_Succeeds()
        {
            context.AddressTypes.Add(new AddressType(AddressType.AddressTypeId.Other));
            context.SaveChanges();

            var addressType = context.AddressTypes.First();
            Assert.Equal(AddressType.AddressTypeId.Other, addressType.Id);
            Assert.Equal("Other", addressType.Name);
        }

        [Fact]
        public void AddTelephoneType_Succeeds()
        {
            context.TelephoneTypes.Add(new TelephoneType(TelephoneType.TelephoneTypeId.Other));
            context.SaveChanges();

            var telephoneType = context.TelephoneTypes.First();
            Assert.Equal(TelephoneType.TelephoneTypeId.Other, telephoneType.Id);
            Assert.Equal("Other", telephoneType.Name);
        }
    }
}
