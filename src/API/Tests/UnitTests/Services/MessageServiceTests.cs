namespace Tests.UnitTests.Services;

using Moq;
using Xunit;
using System.Collections.Generic;
using Application.Abstractions;
using Application.ApplicationModels;
using Application.Services;
using MapsterMapper;
using Application.Domain;

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
    public void GetMessages_ReturnsMappedMessageDtos()
    {
        // Arrange
        var messages = new List<Message>
        {
            new () { Content = "Content" },
            new () { Content = "Content" }
        };

        _messageRepositoryMock.Setup(repo => repo.GetMessages()).Returns(messages);

        var expectedMessageDtos = new List<MessageDto>
        {
            new () { Content = "Content" },
            new () { Content = "Content" }
        };

        _mapperMock.Setup(m => m.Map<IEnumerable<MessageDto>>(messages)).Returns(expectedMessageDtos);

        // Act
        var result = _messageService.GetMessages();

        // Assert
        Assert.Equal(expectedMessageDtos, result);
        _messageRepositoryMock.Verify(repo => repo.GetMessages(), Times.Once);
    }

    [Fact]
    public void GetLatestMessage_ReturnsMappedMessageDto()
    {
        // Arrange
        var message = new Message { Content = "Content" };

        _messageRepositoryMock.Setup(repo => repo.GetLatestMessage()).Returns(message);

        var expectedMessageDto = new MessageDto { Content = "Content" };

        _mapperMock.Setup(m => m.Map<MessageDto>(message)).Returns(expectedMessageDto);

        // Act
        var result = _messageService.GetLatestMessage();

        // Assert
        Assert.Equal(expectedMessageDto, result);
        _messageRepositoryMock.Verify(repo => repo.GetMessages(), Times.Once);
    }

    [Fact]
    public void SendMessage_CallsAddMessage()
    {
        // Arrange
        var messageDto = new MessageDto { /* set properties */ };
        var message = new Message { /* set properties based on messageDto */ };
        _mapperMock.Setup(m => m.Map<Message>(messageDto)).Returns(message);

        // Act
        _messageService.SendMessage(messageDto);

        // Assert
        _messageRepositoryMock.Verify(repo => repo.AddMessage(message), Times.Once);
    }
}

