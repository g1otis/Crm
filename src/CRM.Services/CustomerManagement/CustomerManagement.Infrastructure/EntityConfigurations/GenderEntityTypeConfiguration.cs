using System;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Infrastructure.EntityConfigurations
{
    public class GenderEntityTypeConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders", CustomerManagementContext.DEFAULT_SCHEMA);

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasDefaultValue(Gender.GenderId.NotSet)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(g => g.Name)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
