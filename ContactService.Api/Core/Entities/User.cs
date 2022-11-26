using ContactService.Api.Core.Base;

namespace ContactService.Api.Core.Entities;

public class User : Entity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Company { get; set; }

    public IList<ContactItem> ContactItems { get; private set; } = new List<ContactItem>();
}