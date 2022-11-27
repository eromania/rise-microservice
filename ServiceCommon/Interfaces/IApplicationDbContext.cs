namespace ServiceCommon.Interfaces;

/// <summary>
/// Interface for ApplicationDbContext
/// </summary>
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}