using ContactService.Api.Feature.ContactItem;
using ContactService.Api.Feature.User;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ServiceCommon.Behaviours;

namespace ContactService.Api.UnitTests.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<GetContactItemTypesQuery>> _loggerQuery = null!;
    private Mock<ILogger<CreateUserContactItemCommand>> _loggerCommand = null!;
    
    [SetUp]
    public void Setup()
    {
        _loggerQuery = new Mock<ILogger<GetContactItemTypesQuery>>();
        _loggerCommand = new Mock<ILogger<CreateUserContactItemCommand>>();
    }
    
    [Test]
    public async Task ShouldLogBehaviourProcessOnQueryCall()
    {
    
        var requestLogger = new LoggingBehaviour<GetContactItemTypesQuery>(_loggerQuery.Object);
    
        await requestLogger.Process(new GetContactItemTypesQuery(), new CancellationToken());
    }
    
    [Test]
    public async Task ShouldLogBehaviourProcessOnCommandCall()
    {
    
        var requestLogger = new LoggingBehaviour<CreateUserContactItemCommand>(_loggerCommand.Object);
    
        await requestLogger.Process(new CreateUserContactItemCommand { UserId = 1, ContactItemTypeId = 5, Value = "5555"}, new CancellationToken());
    }
}