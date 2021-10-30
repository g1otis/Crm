using System;
using System.Threading.Tasks;
using CustomerManagement.Infrastructure;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;

namespace CustomerManagement.API.Infrastructure
{
    public class CustomerManagementContextSeed
    {
        public CustomerManagementContextSeed()
        {
        }

        public async Task SeedAsync(CustomerManagementContext context)
        {
            await context.AddressTypes.AddRangeAsync(new AddressType[]
            {
                new AddressType(AddressType.AddressTypeId.Home),
                new AddressType(AddressType.AddressTypeId.Work),
                new AddressType(AddressType.AddressTypeId.Other),
            });

            await context.TelephoneTypes.AddRangeAsync(new TelephoneType[]
            {
                new TelephoneType(TelephoneType.TelephoneTypeId.Home),
                new TelephoneType(TelephoneType.TelephoneTypeId.Work),
                new TelephoneType(TelephoneType.TelephoneTypeId.Personal),
                new TelephoneType(TelephoneType.TelephoneTypeId.Other)
            });
        }
    }
}
