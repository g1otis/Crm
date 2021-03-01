using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Data.Models;
using Libraries.RepositoryPattern;

namespace CRM.DataProviders
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<List<Customer>> GetAsync(List<Guid> ids);
    }
}
