using Core.Abstractions;
using Core.Models;
using Core.Repositories.ApiModels;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Core.Repositories;

internal class UserRepository(
        HttpClient _httpClient
    ) : IUserRepository
{
    public async Task<bool> JoinChat(User user)
    {
        var request = new JoinChatRequest
        {
            User = user,
        };

        var jsonObject = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(Route.Join, content);

        if (response.StatusCode == HttpStatusCode.OK) 
        {
            return true;
        }

        return false;
    }

    public async Task LeaveChat(User user)
    {
        var request = new LeaveChatRequest
        {
            User = user
        };

        var jsonObject = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(Route.Leave, content);
    }
}
