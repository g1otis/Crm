using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Infrastructure.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        private const int stringsMaxLength = 128;

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", CustomerManagementContext.DEFAULT_SCHEMA);

            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.DomainEvents);

            builder.Property(c => c.Id)
                .UseHiLo("CustomerSeq", CustomerManagementContext.DEFAULT_SCHEMA);

            builder.Property(c => c.Age);

            builder.Property(c => c.EmailAddress)
                .HasMaxLength(stringsMaxLength);
            builder.HasIndex(c => c.EmailAddress)
                .IsUnique(true);

            builder.Property(c => c.FirstName)
                .HasMaxLength(stringsMaxLength);

            builder.Property<Gender.GenderId>("_genderId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("GenderId")
                .IsRequired();

            builder.HasOne(c => c.Gender)
                .WithMany()
                .HasForeignKey("_genderId");

            builder.Property(c => c.LastName)
                .HasMaxLength(stringsMaxLength);

            builder.Property(c => c.MiddleName)
                .HasMaxLength(stringsMaxLength);

            //Addresses and Telephones value object persisted as owned entity type supported since EF Core 2.0
            builder.OwnsMany(c => c.Addresses, a =>
            {
                // Explicit configuration of the shadow key property in the owned type 
                // as a workaround for a documented issue in EF Core 5: https://github.com/dotnet/efcore/issues/20740
                a.Property<int>("CustomerId")
                    .UseHiLo("CustomerSeq", CustomerManagementContext.DEFAULT_SCHEMA);

                a.Property<AddressType.AddressTypeId>("AddressTypeId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName("AddressTypeId")
                    .IsRequired();

                a.HasOne(addr => addr.AddressType)
                    .WithOne()
                    .HasForeignKey("AddressTypeId");

                a.Property(addr => addr.City)
                    .HasMaxLength(stringsMaxLength);

                a.Property(addr => addr.CountryISO3)
                    .HasMaxLength(3);

                a.Property(addr => addr.PostalCode)
                    .HasMaxLength(16);

                a.Property(addr => addr.StreetName)
                    .HasMaxLength(stringsMaxLength);

                a.Property(addr => addr.StreetNumber)
                    .HasMaxLength(16);

                a.WithOwner();
            });

            builder.OwnsMany(c => c.Telephones, t =>
            {
                t.Property<int>("CustomerId")
                    .UseHiLo("CustomerSeq", CustomerManagementContext.DEFAULT_SCHEMA);

                t.Property<TelephoneType.TelephoneTypeId>("TelephoneTypeId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName("TelephoneTypeId")
                    .IsRequired();

                t.HasOne(tel => tel.TelephoneType)
                    .WithOne()
                    .HasForeignKey("TelephoneTypeId");

                t.Property(tel => tel.Extension)
                    .HasMaxLength(8);

                t.Property(tel => tel.Phone)
                    .HasMaxLength(32);

                t.WithOwner();
            });
        }
    }
}
