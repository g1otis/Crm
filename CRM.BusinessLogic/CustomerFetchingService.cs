using System;
using System.Threading.Tasks;
using CRM.BusinessLogic.Exceptions;
using CRM.Data.Models;
using CRM.DataProviders;

namespace CRM.BusinessLogic
{
    /// <summary>
    ///
    /// </summary>
    public class CustomerFetchingService
    {
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
