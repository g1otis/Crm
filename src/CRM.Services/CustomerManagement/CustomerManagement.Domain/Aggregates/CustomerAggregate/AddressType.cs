using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomerManagement.Domain.Common;
using CustomerManagement.Domain.SeedWork;

namespace CustomerManagement.Domain.Aggregates.CustomerAggregate
{
    public class AddressType : Enumeration<AddressType.AddressTypeId>
    {
        public enum AddressTypeId
        {
            [Display(Name = "Other")]
            Other,
            [Display(Name = "Home")]
            Home,
            [Display(Name = "Work")]
            Work
        }

        public static AddressType Other = new AddressType(AddressTypeId.Other);
        public static AddressType Home = new AddressType(AddressTypeId.Home);
        public static AddressType Work = new AddressType(AddressTypeId.Work);

        public static IEnumerable<AddressType> GetAll() => GetAll<AddressType>();

        public AddressType(AddressTypeId id) : base(id, EnumHelper<AddressTypeId>.GetDisplayValue(id))
        {
        }
    }
}
