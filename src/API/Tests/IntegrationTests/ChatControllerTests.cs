namespace Tests.IntegrationTests;

using Application.Domain;
using Newtonsoft.Json;
using Presentation.PresentationModels;
using Presentation.PresentationModels.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class ChatControllerTests : IClassFixture<ChatApplication>
{
    private readonly HttpClient _client;
    private readonly DummyDataContext _context;
    private MessageApiModel _existingMessage;
    private UserApiModel _existingUser;

    public ChatControllerTests(ChatApplication factory)
    {
        _client = factory.CreateClient();
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
            CreatedAt = existing.CreatedAt,
            User = _existingUser
        };
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
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
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
        Assert.Equal(result, expected);
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
        Assert.NotNull(result);
        Assert.Equal(result, expected);
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
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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

    [Fact]
    public async Task GetStats_ReturnsStats()
    {
        // Arrange
        var expected = new ResponseGetStats
        {
            Stats = new StatsApiModel { IsActiveCount = 1, ActiveUsers = [ _existingUser ], InActiveUsers = [] }
        };

        // Act
        var response = await _client.GetAsync("api/chat/GetStats");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetStats>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal(result, expected);
    }
}