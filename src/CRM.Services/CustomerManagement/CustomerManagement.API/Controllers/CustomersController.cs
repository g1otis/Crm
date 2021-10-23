using CustomerManagement.Application.Commands;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        // GET api/v1/customers/create
        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateCustomerAsync([FromBody] CreateCustomerCommand command)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - ({@Command})",
                nameof(CreateCustomerCommand),
                command);

            return await _mediator.Send(command);
        }
    }
}
