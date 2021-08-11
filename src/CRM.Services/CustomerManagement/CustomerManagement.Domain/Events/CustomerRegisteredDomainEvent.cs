using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using MediatR;

namespace CustomerManagement.Domain.Events
{
    public class CustomerRegisteredDomainEvent : INotification
    {
        public Customer Customer { get; private set; }

        public CustomerRegisteredDomainEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
