using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataProviders
{
    public class CustomerRepository : RepositoryBase<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Customer>> GetAsync(List<Guid> ids)
        {
            return await _dbSet.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
