using NUnit.Framework;

namespace ContactService.Api.IntegrationTests;

using static Testing;

public class TestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        // await ResetState();
    } 
}