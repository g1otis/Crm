using System;
using System.Threading.Tasks;
using CustomerManagement.Infrastructure;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.API.Infrastructure
{
    public class CustomerManagementContextSeed
    {
        public CustomerManagementContextSeed()
        {
        }

        public async Task SeedAsync(CustomerManagementContext context)
        {
            if (!await context.Genders.AnyAsync())
            {
                await context.Genders.AddRangeAsync(new Gender[]
                {
                    new Gender(Gender.GenderId.NotSet),
                    new Gender(Gender.GenderId.Male),
                    new Gender(Gender.GenderId.Female),
                });
            }

            if (!await context.AddressTypes.AnyAsync())
            {
                await context.AddressTypes.AddRangeAsync(new AddressType[]
                {
                    new AddressType(AddressType.AddressTypeId.Home),
                    new AddressType(AddressType.AddressTypeId.Work),
                    new AddressType(AddressType.AddressTypeId.Other),
                });
            }

            if (!await context.TelephoneTypes.AnyAsync())
            {
                await context.TelephoneTypes.AddRangeAsync(new TelephoneType[]
                {
                    new TelephoneType(TelephoneType.TelephoneTypeId.Home),
                    new TelephoneType(TelephoneType.TelephoneTypeId.Work),
                    new TelephoneType(TelephoneType.TelephoneTypeId.Personal),
                    new TelephoneType(TelephoneType.TelephoneTypeId.Other)
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
