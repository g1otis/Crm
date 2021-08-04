using System.Threading.Tasks;
using System.Collections.Generic;
using CRM.Data.Models;
using CRM.UI.BackEnd;
using CRM.UI.Models;

namespace CRM.UI.Services
{
    public class CustomersService
    {
        private readonly BackEndApiClient backEndApiClient;

        public CustomersService(BackEndApiClient backEndApiClient)
        {
            this.backEndApiClient = backEndApiClient;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await backEndApiClient.GetCustomersAsync();
        }

        public async Task RegisterAsync(CustomerRegistrationModel model)
        {
            await backEndApiClient.RegisterAsync(model);
        }

        public async Task<ServiceResult<List<Customer>>> GetAllResultAsync()
        {
            try
            {
                var customers = await backEndApiClient.GetCustomersAsync();

                return new ServiceResult<List<Customer>>
                {
                    IsSucceeded = true,
                    Data = customers,
                };
            }
            catch (System.Exception ex)
            {
                return new ServiceResult<List<Customer>>
                {
                    IsSucceeded = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
