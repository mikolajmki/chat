using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using Application.Validation;
using Application.Services;
using MapsterMapper;
using Moq;
using Xunit;

namespace Tests.UnitTests.Services;

public class MessageServiceTests
{
    private readonly Mock<IMessageRepository> _messageRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly MessageService _messageService;

    public MessageServiceTests()
    {
        _messageRepositoryMock = new Mock<IMessageRepository>();
        _mapperMock = new Mock<IMapper>();
        _messageService = new MessageService(_messageRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void GetLatestMessage_ShouldReturnMappedMessageDto()
    {
        // Arrange
        var message = new Message { Content = "Latest Message" };
        var messageDto = new MessageDto { Content = "Latest Message" };

        _messageRepositoryMock.Setup(r => r.GetLatestMessage()).Returns(message);
        _mapperMock.Setup(m => m.Map<MessageDto>(message)).Returns(messageDto);

        // Act
        var result = _messageService.GetLatestMessage();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(messageDto.Content, result.Content);
        _messageRepositoryMock.Verify(r => r.GetLatestMessage(), Times.Once);
    }

    [Fact]
    public void GetMessages_ShouldReturnMappedMessageDtos()
    {
        // Arrange
        var messages = new List<Message>
        {
            new Message { Content = "Message 1" },
            new Message { Content = "Message 2" }
        };
        var messageDtos = new List<MessageDto>
        {
            new MessageDto { Content = "Message 1" },
            new MessageDto { Content = "Message 2" }
        };

        _messageRepositoryMock.Setup(r => r.GetMessages()).Returns(messages);
        _mapperMock.Setup(m => m.Map<IEnumerable<MessageDto>>(messages)).Returns(messageDtos);

        // Act
        var result = _messageService.GetMessages();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(messageDtos[0].Content, result.ElementAt(0).Content);
        _messageRepositoryMock.Verify(r => r.GetMessages(), Times.Once);
    }

    [Fact]
    public void SendMessage_ValidMessage_ShouldAddMessage()
    {
        // Arrange
        var messageDto = new MessageDto { Content = "Hello" };
        var message = new Message { Content = "Hello" };
        var messageValidator = new MessageValidator();

        _mapperMock.Setup(m => m.Map<Message>(messageDto)).Returns(message);
        _messageRepositoryMock.Setup(r => r.AddMessage(message));

        // Act
        var result = _messageService.SendMessage(messageDto);

        // Assert
        Assert.True(result);
        _messageRepositoryMock.Verify(r => r.AddMessage(message), Times.Once);
    }

    [Fact]
    public void SendMessage_InvalidMessage_ShouldReturnFalse()
    {
        // Arrange
        var messageDto = new MessageDto { Content = "" };
        var message = new Message { Content = "" };

        var messageValidator = new MessageValidator();
        _mapperMock.Setup(m => m.Map<Message>(messageDto)).Returns(message);
        var validationResult = messageValidator.Validate(message);
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Content", "Content cannot be empty"));

        // Act
        var result = _messageService.SendMessage(messageDto);

        // Assert
        Assert.False(result);
        _messageRepositoryMock.Verify(r => r.AddMessage(It.IsAny<Message>()), Times.Never);
    }
}
