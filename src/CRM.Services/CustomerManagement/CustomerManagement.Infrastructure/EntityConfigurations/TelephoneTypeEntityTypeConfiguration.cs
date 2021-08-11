using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Infrastructure.EntityConfigurations
{
    public class TelephoneTypeEntityTypeConfiguration : IEntityTypeConfiguration<TelephoneType>
    {
        public void Configure(EntityTypeBuilder<TelephoneType> builder)
        {
            builder.ToTable("TelephoneTypes", CustomerManagementContext.DEFAULT_SCHEMA);

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasDefaultValue(TelephoneType.TelephoneTypeId.Other)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(g => g.Name)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
