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
                    .UseSqlite("Filename=Test.db")
                    .Options;

            context = new CustomerManagementContext(options);
        }

        [Fact]
        public void EnsureCreated_Succeeds()
        {
            context.Database.EnsureCreated();
        }
    }
}
