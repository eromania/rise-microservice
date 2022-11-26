using ContactService.Api.Core.Base;

namespace ContactService.Api.Core.Entities;

public class ContactItem : Entity
{
    public int UserId { get; set; }
    public int ContactItemTypeId { get; set; }
    public ContactItemType ContactItemType { get; set; }
    public string Value { get; set; }
}