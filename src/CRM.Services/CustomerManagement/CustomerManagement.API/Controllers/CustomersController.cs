// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CustomerManagement.Application.Commands;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using CustomerManagement.Application.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;
        private readonly ICustomerQueries _customerQueries;

        public CustomersController(ILogger<CustomersController> logger, IMediator mediator, ICustomerQueries customerQueries)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _customerQueries = customerQueries ?? throw new ArgumentNullException(nameof(customerQueries));
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

        [Route("customerId:Guid")]
        [HttpGet]
        public async Task<ActionResult> GetCustomerAsync(Guid customerId)
        {
            var c = await _customerQueries.GetCustomer(customerId);

            return Ok(c);
        }
    }
}
