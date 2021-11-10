using System;

namespace CustomerManagement.Application.Queries
{
    public record CustomerViewModel(
        string FirstName,
        string MiddleName,
        string LastName,
        string Email,
        EnumViewModel<GenderType> Gender,
        AddressViewModel? Address,
        TelephoneViewModel? Telephone);

    public record EnumViewModel<T>(T type, string Descriotion) where T : Enum;

    public record AddressViewModel(EnumViewModel<AddressType> Type, string StreetName, string StreetNumber, string PostalCode, string City, string CountryIso3);

    public record TelephoneViewModel(EnumViewModel<TelephoneType> Type, string Extension, string Phone);

    public enum TelephoneType
    {
        Other,
        Personal,
        Home,
        Work
    }

    public enum AddressType
    {
        Other,
        Home,
        Work
    }

    public enum GenderType
    {
        NotSet,
        Male,
        Female
    }
}
