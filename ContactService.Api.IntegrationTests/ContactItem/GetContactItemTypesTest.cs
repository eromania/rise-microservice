using ContactService.Api.Core.Entities;
using ContactService.Api.Feature.ContactItem;
using FluentAssertions;
using NUnit.Framework;

namespace ContactService.Api.IntegrationTests.ContactItem;

using static Testing;

public class GetContactItemTypesTest : TestBase

{
    [Test]
    public async Task ShouldReturnContactItemTypes()
    {
        var query = new GetContactItemTypesQuery();

        var result = await SendAsync(query);

        result.Should().HaveCountGreaterThan(0);
    }

    [Test]
    public async Task ShouldAddNewContactItemType()
    {
        var query = new GetContactItemTypesQuery();

        var resultBefore = await SendAsync(query);

        await AddAsync(new ContactItemType()
        {
            Created = DateTime.UtcNow,
            CreatedBy = 111,
            Type = "Add-Test",
            IsValid = 1
        });

        var resultAfter = await SendAsync(query);

        resultAfter.Count.Should().BeGreaterThan(resultBefore.Count);
    }
}