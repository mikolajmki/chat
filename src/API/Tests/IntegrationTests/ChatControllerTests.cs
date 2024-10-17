namespace Tests.IntegrationTests;

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Presentation.PresentationModels;
using Presentation.PresentationModels.IO;
using Xunit;

public class ChatControllerTests : IClassFixture<ChatApplication>
{
    private readonly HttpClient _client;
    public ChatControllerTests(ChatApplication factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SendMessage_ReturnsOk_WhenMessageIsSent()
    {
        // Arrange
        var user = new UserApiModel
        {
            Name = "Bob",
        };

        var request = new RequestSendMessage
        {
            Message = new MessageApiModel { Content = "Hello, World!", User = user }
        };

        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("api/chat/SendMessage", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMessages_ReturnsMessages()
    {
        // Act
        var response = await _client.GetAsync("api/chat/GetMessages");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetMessages>(responseBody);

        Assert.NotNull(result);
        Assert.IsType<ResponseGetMessages>(result);
    }

    [Fact]
    public async Task GetLatestMessage_ReturnsOkAndMessage()
    {
        // Act
        var response = await _client.GetAsync("api/chat/GetLatestMessage");

        var expected = new MessageApiModel
        {
            Content = DummyDataContext.Message.Content,
            User = new UserApiModel { Name = DummyDataContext.User.Name }
        };

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetLatestMessage>(responseBody);

        Assert.NotNull(result);
        Assert.IsType<ResponseGetLatestMessage>(result);
        Assert.Equal(result.LatestMessage, expected);
    }

    [Fact]
    public async Task Join_ReturnsOk_WhenUserIsAdded()
    {
        // Arrange
        var request = new RequestJoinChat
        {
            User = new UserApiModel { Name = "Alice" }
        };
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("api/chat/Join", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStats_ReturnsStats()
    {
        // Act
        var response = await _client.GetAsync("api/chat/GetActiveStats");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseGetStats>(responseBody);

        Assert.NotNull(result);
        Assert.IsType<ResponseGetStats>(result);
    }
}