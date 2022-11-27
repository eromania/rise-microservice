using ContactService.Api.Feature.ContactItem;
using ContactService.Api.Feature.User;
using FluentAssertions;
using NUnit.Framework;

namespace ContactService.Api.IntegrationTests.ReportApi;


using static Testing;

public class GetDataByLocationTests : TestBase
{
    [Test]
    public async Task ShouldReturnReportData()
    {
        //contactItemType
        var contactItemTypeDtos = await SendAsync( new GetContactItemTypesQuery());
        
        //add user
        //user
        var userId1 = await SendAsync(new CreateUserCommand()
        {
            Name = "user 1",
            Surname = "user 1 surname",
            Company = "Test Company",
        });
        
        var command = new CreateUserContactItemCommand()
        {
            UserId = userId1,
            ContactItemTypeId = contactItemTypeDtos.FirstOrDefault(c=>c.Type == "Telephone")!.Id,
            Value = "2222222222",
        };
        
        var itemId = await SendAsync(command);
        
        command = new CreateUserContactItemCommand()
        {
            UserId = userId1,
            ContactItemTypeId = contactItemTypeDtos.FirstOrDefault(c=>c.Type == "Location")!.Id,
            Value = "ankara",
        };
        
        itemId = await SendAsync(command);
        
        var result = await SendAsync(new GetDataByLocationQuery());

        result.Should().HaveCountGreaterThan(0);
    }
}
