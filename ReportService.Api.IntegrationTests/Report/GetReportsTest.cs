using FluentAssertions;
using NUnit.Framework;
using ReportService.Api.Feature.Report;

namespace ReportService.Api.IntegrationTests.Report;

using static Testing;

public class GetReportsTest : TestBase

{
    [Test]
    public async Task ShouldAddNewContactItemType()
    {
        var query = new GetReportsQuery();

        var resultBefore = await SendAsync(query);

        await AddAsync(new Entities.Report()
        {
            Created = DateTime.UtcNow,
            CreatedBy = 111,
            IsValid = 1,
            Status = "waiting"
        });

        var resultAfter = await SendAsync(query);

        resultAfter.Count.Should().BeGreaterThan(resultBefore.Count);
    }
}