using System;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Queries
{
    public interface ICustomerQueries
    {
        Task<CustomerViewModel> GetCustomer(Guid id);
    }
}
