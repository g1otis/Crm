using System.Linq;
using CustomerManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CustomerManagement.UnitTests.Infrastructure
{
    public class CustomerManagementContextTests
    {
        private readonly CustomerManagementContext context;

        public CustomerManagementContextTests()
        {
            var options = new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                    //.UseSqlite("Filename=Test.db")
                    .Options;

            context = new CustomerManagementContext(options);
        }

        [Fact]
        public void EnsureCreated_Succeeds()
        {
            context.Database.EnsureCreated();

            context.AddressTypes.Add(new CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType(CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType.AddressTypeId.Other));
            context.SaveChanges();
            Assert.Equal(1, context.AddressTypes.Count());
        }

        [Fact]
        public void MyTest()
        {
            context.AddressTypes.Add(new CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType(CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType.AddressTypeId.Other));
            context.SaveChanges();

            Assert.Equal(1, context.AddressTypes.Count());
        }
    }
}
