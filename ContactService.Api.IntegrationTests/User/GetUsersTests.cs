using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;

namespace ContactService.Api.IntegrationTests.User;

using static Testing;

public class GetUsersTests : TestBase
{
    [Test]
    public async Task ShouldReturnUsers()
    {
        var command = new CreateUserCommand()
        {
            Name = "Test User",
            Surname = "Test Surname",
            Company = "Test Company",
        };
        
        var userId = await SendAsync(command);
        
        var query = new GetUsersQuery();

        var result = await SendAsync(query);

        result.Should().HaveCountGreaterThan(0);
    }
}