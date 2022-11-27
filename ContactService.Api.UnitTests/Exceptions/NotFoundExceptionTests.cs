using FluentAssertions;
using NUnit.Framework;
using ServiceCommon.Exceptions;

namespace ContactService.Api.UnitTests.Exceptions;

public class NotFoundExceptionTests
{
    [Test]
    public void DefaultConstructorShouldReturnDefaultMessage()
    {
        var actual = new NotFoundException();

        actual.Message.Should()
            .BeEquivalentTo("Exception of type 'ServiceCommon.Exceptions.NotFoundException' was thrown.");
    }

    [Test]
    public void ConstructorWithMessageShouldReturnExceptionMessage()
    {
        var message = "Test message";
        var actual = new NotFoundException(message);

        actual.Message.Should()
            .BeEquivalentTo(message);
    }
    
    [Test]
    public void ConstructorWithMessageAndInnerExceptionShouldReturnExceptionMessage()
    {
        var message = "Test message with inner exception";
        var innerMesage = "Inner exception message";
        var actual = new NotFoundException(message, new Exception(innerMesage));

        actual.Message.Should().BeEquivalentTo(message);
        actual.InnerException?.Message.Should().BeEquivalentTo(innerMesage);
    }
    
    [Test]
    public void ConstructorWithNameAndObjectShouldReturnEntityNotFoundMessage()
    {
        var name = "ContactItem";
        var key = "1";
        var actual = new NotFoundException(name, key);

        actual.Message.Should().BeEquivalentTo($"Entity '{name}' ({key}) was not found.");
    }
}