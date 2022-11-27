using NUnit.Framework;

namespace ReportService.Api.IntegrationTests;

using static Testing;

public class TestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        // await ResetState();
        await Task.Delay(1000);
    } 
}