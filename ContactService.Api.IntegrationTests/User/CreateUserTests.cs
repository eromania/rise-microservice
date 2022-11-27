using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;
using ServiceCommon.Exceptions;

namespace ContactService.Api.IntegrationTests.User;

using static Testing;

public class CreateUserTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateUserCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTestUser()
    {
        var command = new CreateUserCommand()
        {
            Name = "Test User",
            Surname = "Test Surname",
            Company = "Test Company",
        };
        
        var userId = await SendAsync(command);

        var item = await FindAsync<Entities.User>(userId);

        item.Should().NotBeNull();
        item!.Name.Should().Be(command.Name);
        item.Surname.Should().Be(command.Surname);
        item.Company.Should().Be(command.Company);
        item.CreatedBy.Should().Be(111);
        item.LastModifiedBy.Should().BeNull();
        item.LastModified.Should().BeNull();
    }
}