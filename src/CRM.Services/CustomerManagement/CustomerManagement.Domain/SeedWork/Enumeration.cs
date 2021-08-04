using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomerManagement.Domain.SeedWork
{
    public abstract class Enumeration<TEnum> : IComparable where TEnum : Enum
    {
        public string Name { get; private set; }

        public TEnum Id { get; private set; }

        protected Enumeration(TEnum id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        protected static IEnumerable<T> GetAll<T>() where T : Enumeration<TEnum> =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration<TEnum> otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        // https://makolyte.com/csharp-enum-generic-type-constraint/
        protected static Enumeration<TEnum> FromValue<T>(int value, IEnumerable<Enumeration<TEnum>> enumValues)
        {
            var isDefined = Enum.IsDefined(typeof(TEnum), value);
            var converted = isDefined ? (TEnum)Enum.Parse(typeof(TEnum), $"{value}") : default;

            var matchingItem = Parse(value, enumValues, "value", item => isDefined ? item.Id.CompareTo(converted) == 0 : false);

            return matchingItem;
        }

        protected static Enumeration<TEnum> FromDisplayName<T>(string name, IEnumerable<Enumeration<TEnum>> enumValues)
        {
            var matchingItem = Parse(name, enumValues, "display name", item => item.Name == name);
            return matchingItem;
        }

        private static Enumeration<TEnum> Parse<K>(K value, IEnumerable<Enumeration<TEnum>> enumValues, string description, Func<Enumeration<TEnum>, bool> predicate)
        {
            var matchingItem = enumValues.FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(Enumeration<TEnum>)}");

            return matchingItem;
        }

        public int CompareTo(object? other) => other is Enumeration<TEnum> otherNum ? Id.CompareTo(otherNum.Id) : -1;


        #region EnumHelper

        #endregion

    }
}
