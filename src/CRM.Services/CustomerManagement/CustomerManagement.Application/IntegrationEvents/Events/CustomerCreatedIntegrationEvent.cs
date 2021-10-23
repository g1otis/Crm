using System;
using EventBus.Events;
using MediatR;

namespace CustomerManagement.Application.IntegrationEvents.Events
{
    public record CustomerCreatedIntegrationEvent(Guid customerId) : IntegrationEvent;
}
