using ContactService.Api.Common.Exceptions;
using ContactService.Api.Feature.ContactItem;
using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;

namespace ContactService.Api.IntegrationTests.User;

using static Testing;

public class DeleteUserContactItemTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new DeleteUserContactItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDeleteUserContactItem()
    {
        //user
        var userId = await SendAsync(new CreateUserCommand()
        {
            Name = "Test User",
            Surname = "Test Surname",
            Company = "Test Company",
        });

        //contactItemType
        var contactItemTypeDtos = await SendAsync(new GetContactItemTypesQuery());

        var itemId = await SendAsync(new CreateUserContactItemCommand()
        {
            UserId = userId,
            ContactItemTypeId = contactItemTypeDtos[0].Id,
            Value = "Test Value",
        });

        await SendAsync(new DeleteUserContactItemCommand()
        {
            UserId = userId,
            ContactItemId = itemId
        });

        var item = await FindAsync<Core.Entities.ContactItem>(itemId);

        item.IsValid.Should().Be(0);
    }
}