using FluentAssertions;
using NUnit.Framework;
using ReportService.Api.Feature.Report;
using ServiceCommon.Exceptions;

namespace ReportService.Api.IntegrationTests.Report;

using static Testing;

public class UpdateReportDataTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdateReportDataCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateReportRequestData()
    {
        var command = new CreateReportRequestCommand();
        
        var reportId = await SendAsync(command);

        var updateCommand = new UpdateReportDataCommand()
        {
            Id = reportId,
            ReportData = "Update Report Data"
        };
        
        await SendAsync(updateCommand);
        
        var item = await FindAsync<Entities.Report>(reportId);

        item!.Status.Should().Be("completed");
        item.ReportData.Should().Be("Update Report Data");
        item.LastModified.Should().NotBeNull();
        item.LastModifiedBy.Should().NotBeNull();

    }
}