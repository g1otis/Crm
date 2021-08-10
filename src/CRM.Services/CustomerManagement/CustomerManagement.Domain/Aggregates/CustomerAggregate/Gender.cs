using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomerManagement.Domain.Common;
using CustomerManagement.Domain.SeedWork;

namespace CustomerManagement.Domain.Aggregates.CustomerAggregate
{
    public class Gender : Enumeration<Gender.GenderId>
    {
        public enum GenderId
        {
            [Display(Name = "Not set")]
            NotSet,
            [Display(Name = "Male")]
            Male,
            [Display(Name = "Female")]
            Female
        }

        public static readonly Gender NotSet = new Gender(GenderId.NotSet);
        public static readonly Gender Male = new Gender(GenderId.Male);
        public static readonly Gender Female = new Gender(GenderId.Female);

        public static IEnumerable<Gender> GetAll() => GetAll<Gender>();

        public Gender(GenderId id) : base(id, EnumHelper<GenderId>.GetDisplayValue(id))
        {
        }

    }
}
