using FluentAssertions;
using NUnit.Framework;
using ReportService.Api.Feature.Report;
using ServiceCommon.Exceptions;

namespace ReportService.Api.IntegrationTests.Report;

using static Testing;

public class CreateReportRequestTests : TestBase
{
    [Test]
    public async Task ShouldCreateReportRequestAndPublishToTheMessageBroker()
    {
        //report
        var reportId = await SendAsync(new CreateReportRequestCommand());
        
        var item = await FindAsync<Entities.Report>(reportId);

        item.Should().NotBeNull();
        item!.Status.Should().Be("waiting");
        item.ReportData.Should().BeNull();
        item.CreatedBy.Should().Be(111);
        item.LastModifiedBy.Should().BeNull();
        item.LastModified.Should().BeNull();
    }
}