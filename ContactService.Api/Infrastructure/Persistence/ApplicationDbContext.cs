using System.Reflection;
using ContactService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceCommon.BaseEntity;
using ServiceCommon.Interfaces;

namespace ContactService.Api.Infrastrcuture.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<ContactItemType> ContactItemTypes => Set<ContactItemType>();
    public DbSet<ContactItem> ContactItems => Set<ContactItem>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = 111;
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.IsValid = 1;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = 222;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}