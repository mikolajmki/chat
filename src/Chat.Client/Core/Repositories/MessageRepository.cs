using Core.Abstractions;
using Core.Models;
using Core.Repositories.ApiModels;
using Newtonsoft.Json;
using System.Text;

namespace Core.Repositories;

internal class MessageRepository(
        HttpClient _httpClient
    ) : IMessageRepository
{

    public async Task<Message> GetLatestMessage()
    {
        var response = await _httpClient.GetAsync(Route.GetLatestMessage);

        var responseBody = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetLatestMessageResponse>(responseBody);

        var message = result.LatestMessage;

        return message;
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
        var response = await _httpClient.GetAsync(Route.GetMessages);

        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GetMessagesResponse>(responseBody);

        var messages = result.Messages;

        return messages;
    }

    public async Task<bool> SendMessage(Message message)
    {
        var request = new SendMessageRequest
        {
            Message = message
        };

        var jsonObject = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

        var result = await _httpClient.PostAsync(Route.SendMessage, content);

        if (result.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}
