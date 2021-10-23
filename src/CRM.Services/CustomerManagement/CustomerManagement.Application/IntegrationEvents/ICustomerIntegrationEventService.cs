using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace CustomerManagement.Application.IntegrationEvents
{
    public interface ICustomerIntegrationEventService
    {
        //Task PublishAsync(Guid transactionId);
        Task PublishAsync(IntegrationEvent integrationEvent);

        //Task AddAsync(IntegrationEvent integrationEvent);
    }
}
