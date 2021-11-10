using System;
using System.Collections.Generic;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using MediatR;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.Gender;

namespace CustomerManagement.Application.Commands
{
    public record CreateCustomerCommand : IRequest<Guid>
    {
        public string FirstName { get; init; }

        public string? MiddleName { get; init; }

        public string LastName { get; init; }

        public GenderId GenderId { get; init; }

        public int Age { get; init; }

        public string EmailAddress { get; init; }

        public List<AddressDto> Addresses { get; init; }

        public List<TelephoneDto> Telephones { get; init; }
    }

    public record AddressDto
    {
        public AddressType AddressType { get; init; }

        public string StreetName { get; init; }

        public string StreetNumber { get; init; }

        public string PostalCode { get; init; }

        public string City { get; init; }

        public string CountryIso3 { get; init; }
    }

    public record TelephoneDto
    {
        public TelephoneType TelephoneType { get; init; }

        public string Extension { get; init; }

        public string Phone { get; init; }
    }
}
