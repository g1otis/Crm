using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.IntegrationEvents.EventHandling
{
    public class CustomerCreatedIntegrationEventHandler : IIntegrationEventHandler<CustomerCreatedIntegrationEvent>
    {
        private readonly ILogger<CustomerCreatedIntegrationEventHandler> _logger;
        private readonly IMediator _mediator;

        public CustomerCreatedIntegrationEventHandler(ILogger<CustomerCreatedIntegrationEventHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(CustomerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //todo: create notificationService that will receive a new customerCreatedNotification command in order to notify about the customer creation
            //using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            //{
            //    _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            //    var customerId = @event.customerId;

            //    var command = new SetStockRejectedOrderStatusCommand(@event.OrderId, orderStockRejectedItems);

            //    _logger.LogInformation(
            //        "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            //        command.GetGenericTypeName(),
            //        nameof(command.OrderNumber),
            //        command.OrderNumber,
            //        command);

            //    await _mediator.Send(command);
            //}
        }

        public async Task HandleAsync(CustomerCreatedIntegrationEvent @event)
        {
            await Task.CompletedTask;

            //todo: create notificationService that will receive a new customerCreatedNotification command in order to notify about the customer creation
            //using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            //{
            //    _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            //    var customerId = @event.customerId;

            //    var command = new SetStockRejectedOrderStatusCommand(@event.OrderId, orderStockRejectedItems);

            //    _logger.LogInformation(
            //        "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            //        command.GetGenericTypeName(),
            //        nameof(command.OrderNumber),
            //        command.OrderNumber,
            //        command);

            //    await _mediator.Send(command);
            //}
        }
    }
}
