using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Application.CommandHandlers;
using CustomerManagement.Application.Commands;
using CustomerManagement.Application.IntegrationEvents;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CustomerManagement.UnitTests.Application.CommandHandlers
{
    public class CreateCustomerCommandHandlerTests : IClassFixture<DependencyRegistrationFixture>
    {
        private static Guid Id = Guid.Parse("dd3695a5-4bea-4b25-96eb-23559b71b3d9");

        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ICustomerIntegrationEventService> _customerIntegrationEventServiceMock;
        private readonly CreateCustomerCommandHandler _createCustomerCommandHandler;

        public CreateCustomerCommandHandlerTests(DependencyRegistrationFixture fixture)
        {
            _customerRepositoryMock = fixture.ServiceProvider.GetRequiredService<Mock<ICustomerRepository>>();
            _customerIntegrationEventServiceMock = fixture.ServiceProvider.GetRequiredService<Mock<ICustomerIntegrationEventService>>();
            _createCustomerCommandHandler = fixture.ServiceProvider.GetRequiredService<CreateCustomerCommandHandler>();
        }

        [Fact]
        public async Task Handle_Succeeds()
        {
            _customerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(GetFakeCustomer());
            _customerRepositoryMock.Setup(repo => repo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            var command = new CreateCustomerCommand
            {
                Age = 18,
                EmailAddress = "emma@sdsd.com",
                FirstName = "Panos",
                GenderId = Gender.Male.Id,
                LastName = "Foullis",
                Addresses = new List<AddressDto> { },
                Telephones = new List<TelephoneDto> { }
            };

            var cltToken = new CancellationToken();

            var id = await _createCustomerCommandHandler.Handle(command, cltToken);

            Assert.Equal(Id, id);
            _customerRepositoryMock.Verify(s => s.AddAsync(It.IsAny<Customer>()), Times.Once);
            _customerRepositoryMock.Verify(s => s.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _customerIntegrationEventServiceMock.Verify(s => s.PublishAsync(It.IsAny<IntegrationEvent>()), Times.Once);
        }

        private class FakeCustomer : Customer
        {
            public FakeCustomer(Guid id, string firstName, string middleName, string lastName, Gender.GenderId gender, int age, string emailAddress) : base(firstName, middleName, lastName, gender, age, emailAddress)
            {
                Id = id;
            }
        }

        private static FakeCustomer GetFakeCustomer()
        {
            return new FakeCustomer(Id, "emma", null, "last", Gender.GenderId.Male, 20, "test@asjjd.com");
        }
    }
}
