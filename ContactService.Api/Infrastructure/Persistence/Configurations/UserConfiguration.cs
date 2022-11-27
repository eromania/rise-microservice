using ContactService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Api.Infrastrcuture.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", "PersonModel");

        builder.Property(t => t.Company)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Surname)
            .HasMaxLength(200)
            .IsRequired();
    }
}