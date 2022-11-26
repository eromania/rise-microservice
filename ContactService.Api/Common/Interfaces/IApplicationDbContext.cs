namespace ContactService.Api.Common.Interfaces;

/// <summary>
/// Interface for ApplicationDbContext
/// </summary>
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}