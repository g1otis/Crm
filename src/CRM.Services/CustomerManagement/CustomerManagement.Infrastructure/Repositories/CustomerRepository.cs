using System;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using CustomerManagement.Domain.SeedWork;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly CustomerManagementContext _context;

        public CustomerRepository(CustomerManagementContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;

        }

        public override IUnitOfWork UnitOfWork => _context;
    }
}
