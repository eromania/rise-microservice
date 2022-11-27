using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Api.Entities;

namespace ReportService.Api.Infrastructure.Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Report", "ReportModel");

        builder.Property(t => t.Status)
            .HasMaxLength(10)
            .HasDefaultValue("waiting")
            .IsRequired();
    }
}