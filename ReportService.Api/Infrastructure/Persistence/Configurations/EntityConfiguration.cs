using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCommon.BaseEntity;

namespace ReportService.Api.Infrastructure.Persistence.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .UseIdentityColumn()
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.IsValid)
            .IsRequired();

        builder.Property(c => c.Created)
            .IsRequired();

        builder.Property(c => c.CreatedBy)
            .IsRequired();
    }
}