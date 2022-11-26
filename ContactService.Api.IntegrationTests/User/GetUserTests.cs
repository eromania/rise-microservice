using ContactService.Api.Feature.ContactItem;
using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;

namespace ContactService.Api.IntegrationTests.User;

using static Testing;

public class GetUserTests : TestBase
{
    [Test]
    public async Task ShouldReturnUserVm()
    {
        //user
        var createUserCommand = new CreateUserCommand()
        {
            Name = "Test User Vm",
            Surname = "Test Surname Vm",
            Company = "Test Company Vm",

        };
        
        var userId = await SendAsync(createUserCommand);
        
        //contactItemType
        var contactItemTypeDtos = await SendAsync( new GetContactItemTypesQuery());
        var contactItemType = contactItemTypeDtos.FirstOrDefault(c => c.Type == "Telephone");
        
        var createUserContactItemCommand = new CreateUserContactItemCommand()
        {
            UserId = userId,
            ContactItemTypeId = contactItemType!.Id,
            Value = "55555Test",
        };

        var itemId = await SendAsync(createUserContactItemCommand);

        
        var query = new GetUserQuery()
        {
            Id = userId
        };

        var result = await SendAsync(query);
        
        result.Should().NotBeNull();
        result.User.Name.Should().Be(createUserCommand.Name);
        result.User.Surname.Should().Be(createUserCommand.Surname);
        result.User.Company.Should().Be(createUserCommand.Company);
        result.ContactItems.Should().HaveCount(1);
        result.ContactItems[0].ContactItemType.Should().Be(contactItemType.Type);
        result.ContactItems[0].Value.Should().Be(createUserContactItemCommand.Value);

    }
}