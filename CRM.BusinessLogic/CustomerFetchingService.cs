using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CRM.BusinessLogic.Exceptions;
using CRM.Data.Models;
using CRM.DataProviders;
using Microsoft.EntityFrameworkCore;

namespace CRM.BusinessLogic
{
    /// <summary>
    ///
    /// </summary>
    public class CustomerFetchingService
    {
        private static readonly Expression<Func<Customer, bool>> DefaultFilter = c => true;
        private static readonly Expression<Func<Customer, Guid>> DefaultOrder = c => c.Id;

        private readonly ICustomerRepository customerRepository;

        public CustomerFetchingService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<Customer> GetAsync(Guid customerId)
        {
            var customer = await customerRepository.GetAsync(customerId);
            if (customer == null)
            {
                throw new CustomerNotFoundException(customerId);
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAsync(int pageSize = 20, int page = 1, Expression<Func<Customer, bool>>? filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var customersQuery = await customerRepository.GetAsync();
            var customers = customersQuery
                .Where(filter ?? DefaultFilter);
#pragma warning restore CS8604 // Possible null reference argument.

            if (orderBy != null)
            {
                customers = orderBy(customers);
            }

            return await customers.ToListAsync();
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer = await customerRepository.AddAsync(customer);

            await customerRepository.SaveChangesAsync();

            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            await customerRepository.UpdateAsync(customer);

            var objectsUpdated = await customerRepository.SaveChangesAsync();

            if (objectsUpdated != 1)
            {
                throw new CustomerUpdateException(customer.Id);
            }
        }
    }
}
