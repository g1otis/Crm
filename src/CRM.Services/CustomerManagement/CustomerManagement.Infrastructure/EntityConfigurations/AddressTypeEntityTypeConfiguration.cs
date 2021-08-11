using System;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Infrastructure.EntityConfigurations
{
    public class AddressTypeEntityTypeConfiguration : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.ToTable("AddressTypes", CustomerManagementContext.DEFAULT_SCHEMA);

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasDefaultValue(AddressType.AddressTypeId.Other)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(g => g.Name)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
