using ContactService.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Api.Infrastrcuture.Persistence.Configurations;

public class ContactItemConfiguration : IEntityTypeConfiguration<ContactItem>
{
    public void Configure(EntityTypeBuilder<ContactItem> builder)
    {
        builder.ToTable("ContactItem", "ContactModel");

        builder.Property(t => t.Value)
            .HasMaxLength(200)
            .IsRequired();
    }
}