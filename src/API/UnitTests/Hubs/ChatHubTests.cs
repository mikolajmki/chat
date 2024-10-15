namespace UnitTests.Hubs;

using Moq;
using Xunit;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Application.ApplicationModels;
using Application.Services;
using MapsterMapper;
using Presentation.Hubs;
using Presentation.PresentationModels.IO;
using Presentation.PresentationModels;

using Xunit.Sdk;

public class ChatHubTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ChatHub _chatHub;
    private readonly Mock<IClientProxy> _clientProxyMock;
    private readonly Mock<IHubCallerClients> _clientsMock;
    private readonly Mock<HubCallerContext> _hubCallerContextMock;

    public ChatHubTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _mapperMock = new Mock<IMapper>();
        _clientProxyMock = new Mock<IClientProxy>();
        _clientsMock = new Mock<IHubCallerClients>();

        _hubCallerContextMock = new Mock<HubCallerContext>();

        _clientsMock.Setup(clients => clients.All).Returns(_clientProxyMock.Object);

        // Create a new instance of ChatHub
        _chatHub = new ChatHub(_userServiceMock.Object, _mapperMock.Object)
        {
            Clients = _clientsMock.Object,
            Context = _hubCallerContextMock.Object
        };
    }

    [Fact]
    public void Connect_CallsAddUserToChat()
    {
        // Arrange
        var request = new RequestConnectUser
        {
            User = new UserApiModel { Name = "Bob" }
        };


        var userDto = new UserDto { Name = "Bob" };

        _mapperMock.Setup(x => x.Map<UserDto>(request.User)).Returns(userDto);

        userDto.SetConnectionId("test-connection-id");

        _userServiceMock.Setup(service => service.AddUserToChat(userDto)).Returns(true);

        // Act
        var result = _chatHub.Connect(request);

        // Assert
        Assert.True(result);
        _userServiceMock.Verify(service => service.AddUserToChat(userDto), Times.Once);
    }

    [Fact]
    public async Task OnConnectedAsync_SendsUserJoinedNotification()
    {
        // Act
        await _chatHub.OnConnectedAsync();

        // Assert
        _clientsMock.Verify(clients => clients.All, Times.Once);
    }

    [Fact]
    public async Task OnDisconnectedAsync_CallsDeactivateUserByConnectionId()
    {
        // Arrange
        var connectionId = _chatHub.Context.ConnectionId;

        // Act
        await _chatHub.OnDisconnectedAsync(NullException.ForNonNullValue("Exception"));

        // Assert
        _userServiceMock.Verify(service => service.DeactivateUserByConnectionId(connectionId), Times.Once);
    }
}