using System;
using CustomerManagement.Domain.SeedWork;

namespace CustomerManagement.Domain.Aggregates.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
}
