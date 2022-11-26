namespace ContactService.Api.Core.Base;

/// <summary>
/// Defines common properties for domain entities.
/// </summary>
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public int? LastModifiedBy { get; set; }
    public int IsValid { get; set; }
}