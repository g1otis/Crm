using System;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.IntegrationEvents
{
    public class CustomerIntegrationEventService : ICustomerIntegrationEventService
    {
        private readonly ILogger<CustomerIntegrationEventService> _logger;

        private readonly IEventBus _eventBus;

        public CustomerIntegrationEventService(ILogger<CustomerIntegrationEventService> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }


        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            //_logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

            try
            {
                _eventBus.Publish(integrationEvent);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);

            }
        }
    }
}
