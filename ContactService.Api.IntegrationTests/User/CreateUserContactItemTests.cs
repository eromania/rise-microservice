using ContactService.Api.Common.Exceptions;
using ContactService.Api.Feature.ContactItem;
using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;

namespace ContactService.Api.IntegrationTests.User;

using static Testing;

public class CreateUserContactItemTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateUserContactItemCommand();
        
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    [Test]
    public async Task ShouldCreateContactItemForTestUser()
    {
        //user
        var userId = await SendAsync(new CreateUserCommand()
        {
            Name = "Test User",
            Surname = "Test Surname",
            Company = "Test Company",
        });
        
        //contactItemType
        var contactItemTypeDtos = await SendAsync( new GetContactItemTypesQuery());
        
        var command = new CreateUserContactItemCommand()
        {
            UserId = userId,
            ContactItemTypeId = contactItemTypeDtos[0].Id,
            Value = "Test Value",
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Core.Entities.ContactItem>(itemId);

        item.Should().NotBeNull();
        item!.UserId.Should().Be(command.UserId);
        item.ContactItemTypeId.Should().Be(command.ContactItemTypeId);
        item.Value.Should().Be(command.Value);
        item.CreatedBy.Should().Be(111);
        item.LastModifiedBy.Should().BeNull();
        item.LastModified.Should().BeNull();
    }
}