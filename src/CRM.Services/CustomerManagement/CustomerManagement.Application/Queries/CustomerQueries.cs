using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.AddressType;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.Gender;
using static CustomerManagement.Domain.Aggregates.CustomerAggregate.TelephoneType;

namespace CustomerManagement.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueries(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerViewModel> GetCustomer(Guid id)
        {
            var customerQuery = await _customerRepository.GetAsync();
            var customer = await customerQuery.Include(c => c.Gender).FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null) throw new Exception($"Customer({id}) not found");

            return Map(customer);
        }

        private CustomerViewModel Map(Customer c) =>
            new CustomerViewModel(c.FirstName, c.MiddleName, c.LastName, c.EmailAddress, Map(c.Gender), Map(c.Addresses.FirstOrDefault()), Map(c.Telephones.FirstOrDefault()));

        private TelephoneViewModel? Map(Telephone? telephone) =>
            telephone == null
                ? null
                : new TelephoneViewModel(Map(telephone.TelephoneType), telephone.Extension, telephone.Phone);

        private AddressViewModel? Map(Address? address) =>
            address == null
                ? null
                : new AddressViewModel(Map(address.AddressType), address.StreetName, address.StreetNumber, address.PostalCode, address.City, address.CountryISO3);

        private EnumViewModel<TelephoneType> Map(Domain.Aggregates.CustomerAggregate.TelephoneType telephoneType) =>
            new EnumViewModel<TelephoneType>(Map(telephoneType.Id), telephoneType.Name);

        private EnumViewModel<AddressType> Map(Domain.Aggregates.CustomerAggregate.AddressType addressType) =>
            new EnumViewModel<AddressType>(Map(addressType.Id), addressType.Name);

        private TelephoneType Map(TelephoneTypeId id)
        {
            switch (id)
            {
                case TelephoneTypeId.Other:
                    return TelephoneType.Other;
                case TelephoneTypeId.Home:
                    return TelephoneType.Home;
                case TelephoneTypeId.Work:
                    return TelephoneType.Work;
                case TelephoneTypeId.Personal:
                    return TelephoneType.Personal;
                default:
                    throw new InvalidOperationException($"Cannot map value{id}, from {typeof(AddressTypeId)} to {typeof(AddressType)}");
            }
        }

        private AddressType Map(AddressTypeId id)
        {
            switch (id)
            {
                case AddressTypeId.Other:
                    return AddressType.Other;
                case AddressTypeId.Home:
                    return AddressType.Home;
                case AddressTypeId.Work:
                    return AddressType.Work;
                default:
                    throw new InvalidOperationException($"Cannot map value{id}, from {typeof(AddressTypeId)} to {typeof(AddressType)}");
            }
        }

        private EnumViewModel<GenderType> Map(Gender g) => new EnumViewModel<GenderType>(Map(g.Id), g.Name);

        private static GenderType Map(GenderId g)
        {
            switch (g)
            {
                case GenderId.NotSet:
                    return GenderType.NotSet;
                case GenderId.Male:
                    return GenderType.Male;
                case GenderId.Female:
                    return GenderType.Female;
                default:
                    throw new InvalidOperationException($"Cannot map value{g}, from {typeof(GenderId)} to {typeof(GenderType)}");
            }
        }
    }
}
