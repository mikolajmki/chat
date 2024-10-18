using Core.Abstractions;
using Core.Models;
using Core.Repositories.ApiModels;
using Newtonsoft.Json;
using System.Text;

namespace Core.Repositories;

internal class MessageRepository(
        IHttpClientFactory _httpClientFactory,
        IWpfConfiguration _configuration
    ) : IMessageRepository
{

    public async Task<Message> GetLatestMessage()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var response = await client.GetAsync(Route.GetLatestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetLatestMessageResponse>(responseBody);

            var message = result.LatestMessage;

            return message;
        }
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var response = await client.GetAsync(Route.GetMessages);

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetMessagesResponse>(responseBody);

            var messages = result.Messages;

            return messages;
        }
    }

    public async Task<bool> SendMessage(Message message)
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var request = new SendMessageRequest
            {
                Message = message
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(Route.SendMessage, content);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
