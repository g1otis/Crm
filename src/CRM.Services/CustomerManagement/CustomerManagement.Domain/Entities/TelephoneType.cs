using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomerManagement.Domain.Common;
using CustomerManagement.Domain.SeedWork;

namespace CustomerManagement.Domain.Entities
{
    public class TelephoneType : Enumeration<TelephoneType.TelephoneTypeId>
    {
        public enum TelephoneTypeId
        {
            [Display(Name = "Other")]
            Other,

            [Display(Name = "Primary")]
            Primary,

            [Display(Name = "Home")]
            Home,

            [Display(Name = "Work")]
            Work
        }

        public static TelephoneType Other = new TelephoneType(TelephoneTypeId.Other);
        public static TelephoneType Primary = new TelephoneType(TelephoneTypeId.Primary);
        public static TelephoneType Home = new TelephoneType(TelephoneTypeId.Home);
        public static TelephoneType Work = new TelephoneType(TelephoneTypeId.Work);

        public static IEnumerable<TelephoneType> GetAll() => GetAll<TelephoneType>();

        public TelephoneType(TelephoneTypeId id) : base(id, EnumHelper<TelephoneTypeId>.GetDisplayValue(id))
        {
        }
    }
}
