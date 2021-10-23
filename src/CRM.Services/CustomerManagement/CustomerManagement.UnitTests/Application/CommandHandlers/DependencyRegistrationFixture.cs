using System;
using CustomerManagement.Application.CommandHandlers;
using CustomerManagement.Application.IntegrationEvents;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CustomerManagement.UnitTests.Application.CommandHandlers
{
    public class DependencyRegistrationFixture
    {
        public IServiceProvider ServiceProvider { get; }
        public DependencyRegistrationFixture()
        {
            var services = new ServiceCollection();

            var customerIntegrationEventServiceMock = new Mock<ICustomerIntegrationEventService>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            services.AddScoped(_ => customerRepositoryMock);
            services.AddScoped(_ => customerIntegrationEventServiceMock);

            services.AddScoped<ILogger<CreateCustomerCommandHandler>, NullLogger<CreateCustomerCommandHandler>>();
            services.AddScoped(_ => customerRepositoryMock.Object);
            services.AddScoped(_ => customerIntegrationEventServiceMock.Object);

            services.AddScoped<CreateCustomerCommandHandler>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
