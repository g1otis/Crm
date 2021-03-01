using CRM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Telephone> Telephones { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
        {
        }
    }
}
