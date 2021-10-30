using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Application.Commands;
using CustomerManagement.Application.IntegrationEvents;
using CustomerManagement.Application.IntegrationEvents.Events;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerIntegrationEventService _eventService;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, ICustomerRepository customerRepository, ICustomerIntegrationEventService eventService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding customer...");

            var customer = new Customer(request.FirstName, request.MiddleName, request.LastName, request.Gender.Id, request.Age, request.EmailAddress);

            request.Addresses?.ForEach(a => customer.AddAddress(new Address(a.AddressType.Id, a.StreetName, a.StreetNumber, a.PostalCode, a.City, a.CountryISO3)));
            request.Telephones?.ForEach(t => customer.AddTelephone(new Telephone(t.TelephoneType.Id, t.Extension, t.Phone)));

            var created = await _customerRepository.AddAsync(customer);

            await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Customer({CustomerId}) added!", created.Id);

            await _eventService.PublishAsync(new CustomerCreatedIntegrationEvent(created.Id));

            return created.Id;
        }
    }
}
