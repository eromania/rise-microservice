using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;
using ServiceCommon.Exceptions;

namespace ContactService.Api.IntegrationTests.User;

using static Testing;

public class DeleteUserTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new DeleteUserCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDeleteTestUser()
    {
        var command = new CreateUserCommand()
        {
            Name = "Delete Test User",
            Surname = "Delete Test Surname",
            Company = "Delete Test Company",
        };
        
        var userId = await SendAsync(command);

        var deleteCommand = new DeleteUserCommand()
        {
            Id = userId
        };
        
        await SendAsync(deleteCommand);
        
        var item = await FindAsync<Entities.User>(userId);

        item!.IsValid.Should().Be(0);
        item.LastModified.Should().NotBeNull();
        item.LastModifiedBy.Should().NotBeNull();

    }
}