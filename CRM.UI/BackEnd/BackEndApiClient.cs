using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CRM.Data.Models;
using CRM.UI.Models;

namespace CRM.UI.BackEnd
{
    public class BackEndApiClient
    {
        private readonly HttpClient httpClient;

        public BackEndApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5003/api/");
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customersPath = "customers";

            var response = await httpClient.GetAsync(customersPath);

            List<Customer> customers = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<List<Customer>>() ?? Enumerable.Empty<Customer>().ToList();
            return customers;
        }

        public async Task RegisterAsync(CustomerRegistrationModel model)
        {
            var customersPath = "customers/register";

            var response = await httpClient.PostAsJsonAsync(customersPath, model);

            response.EnsureSuccessStatusCode();
        }
    }
}
