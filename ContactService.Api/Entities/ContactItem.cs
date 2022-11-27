
using ServiceCommon.BaseEntity;

namespace ContactService.Api.Entities;

public class ContactItem : Entity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int ContactItemTypeId { get; set; }
    public ContactItemType ContactItemType { get; set; }
    public string Value { get; set; }
}