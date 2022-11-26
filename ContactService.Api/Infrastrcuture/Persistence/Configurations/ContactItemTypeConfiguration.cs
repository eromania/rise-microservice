using ContactService.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Api.Infrastrcuture.Persistence.Configurations;

public class ContactItemTypeConfiguration : IEntityTypeConfiguration<ContactItemType>
{
    public void Configure(EntityTypeBuilder<ContactItemType> builder)
    {
        builder.ToTable("ContactItemType", "ContactModel");

        builder.Property(t => t.Type)
            .HasMaxLength(200)
            .IsRequired();
    }
}