namespace Tests.IntegrationTests;

using Application.Domain;
using FluentAssertions;
using Newtonsoft.Json;
using Presentation.PresentationModels;
using Presentation.PresentationModels.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

public class ChatControllerTests : IClassFixture<ChatApplication>, IAsyncLifetime
{
    private HttpClient _client;
    private DummyDataContext _context;
    private MessageApiModel _existingMessage;
    private UserApiModel _existingUser;


    public Task InitializeAsync()
    {
        _client = new ChatApplication().CreateClient();
        _context = new DummyDataContext();

        var existing = _context.Messages.First();

        _existingUser = new UserApiModel
        {
            Id = DummyDataContext.TestUserGuid,
            ConnectionId = existing.User.ConnectionId,
            Name = existing.User.Name,
            IsActive = existing.User.IsActive,
        };

        _existingMessage = new MessageApiModel
        {
            Id = DummyDataContext.TestMessageGuid,
            Content = existing.Content,
            CreatedAt = DummyDataContext.TestMessageCreatedAt,
            User = _existingUser
        };

        return Task.CompletedTask;
    }
    [Fact]
    public async Task SendMessage_ValidMessage_ReturnsOk()
    {
        // Arrange
        var request = new RequestSendMessage
        {
            Message = _existingMessage
        };

        // Act
        var response = await _client.PostAsync("api/chat/SendMessage",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SendMessage_InvalidMessage_ReturnsBadRequest()
    {
        // Arrange
        var request = new RequestSendMessage
        {
            Message = new MessageApiModel { Content = new string('x', 201) }
        };

        // Act
        var response = await _client.PostAsync("api/chat/SendMessage",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetMessages_ReturnsMessages()
    {
        // Arrange
        var expected = new ResponseGetMessages { Messages = [ _existingMessage ] };

        // Act
        var response = await _client.GetAsync("api/chat/GetMessages");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetMessages>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetLatestMessage_ReturnsLatestMessage()
    {
        var expected = new ResponseGetLatestMessage { LatestMessage = _existingMessage };

        // Act
        var response = await _client.GetAsync("api/chat/GetLatestMessage");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetLatestMessage>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetStats_ReturnsStats()
    {
        // Arrange
        var expected = new ResponseGetStats
        {
            Stats = new StatsApiModel { IsActiveCount = 0, ActiveUsers = [], InActiveUsers = [_existingUser] }
        };

        // Act
        var response = await _client.GetAsync("api/chat/GetStats");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetStats>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Join_ValidUser_ReturnsOk()
    {
        // Arrange
        var request = new RequestJoinChat
        {
            User = new UserApiModel { Name = "Name", ConnectionId = "123" }
        };

        // Act
        var response = await _client.PostAsync("api/chat/Join",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Join_InvalidUser_ReturnsBadRequest()
    {
        // Arrange
        var request = new RequestJoinChat
        {
            User = new UserApiModel { Name = new string('x', 20) }
        };

        // Act
        var response = await _client.PostAsync("api/chat/Join",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Join_WithExistingUserName_ReturnsBadRequest()
    {
        // Arrange
        var request = new RequestJoinChat
        {
            User = new UserApiModel { Name = "Bob" }
        };

        // Act
        var response = await _client.PostAsync("api/chat/Join",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Join_WithExistingUserNameOfInActiveUser_ReturnsOk()
    {
        // Arrange
        var request = new RequestJoinChat
        {
            User = new UserApiModel { Name = "Bob", ConnectionId = "123" }
        };

        // Act
        var response = await _client.PostAsync("api/chat/Join",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Leave_UserSuccessfullyLeaves_ReturnsOk()
    {
        // Arrange

        var request = new RequestLeaveChat
        {
            User = new UserApiModel { ConnectionId = _existingUser.ConnectionId }
        };

        // Act
        var response = await _client.PutAsync("api/chat/Leave",
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
    }

    public Task DisposeAsync()
    {
        _client.Dispose();

        return Task.CompletedTask;
    }
}