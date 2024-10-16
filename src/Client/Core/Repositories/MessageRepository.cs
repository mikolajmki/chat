using Application.Abstractions;
using Core.Abstractions;
using Core.Repositories.IO;
using Newtonsoft.Json;
using System.Text;

namespace Core.Repositories;

internal class MessageRepository(
        IHttpClientFactory _httpClientFactory,
        IWpfConfiguration _configuration
    ) : IMessageRepository
{

    public async Task<GetLatestMessageResponse> GetLatestMessage()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var response = await client.GetAsync(Route.GetLatestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetLatestMessageResponse>(responseBody);

            return result;
        }
    }

    public async Task<GetMessagesResponse> GetMessages()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var response = await client.GetAsync(Route.GetMessages);

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetMessagesResponse>(responseBody);

            return result;
        }
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Route.SendMessage, content);
        }
    }
}
