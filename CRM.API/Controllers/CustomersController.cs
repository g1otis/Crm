using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BusinessLogic;
using CRM.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly CustomerFetchingService customerFetchingService;

        public CustomersController(CustomerFetchingService customerFetchingService)
        {
            this.customerFetchingService = customerFetchingService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await customerFetchingService.GetAsync();
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<Customer> GetAsync(Guid id)
        {
            var customer = await customerFetchingService.GetAsync(id);

            return customer;
        }

        // POST api/values
        [HttpPost]
        [Route("CreateFake")]
        public async Task<IActionResult> CreateFakeAsync([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var customer = await customerFetchingService.CreateAsync(new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = "g1otis.foo@gmail.com",
                Age = 5
            });

            return Ok(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
