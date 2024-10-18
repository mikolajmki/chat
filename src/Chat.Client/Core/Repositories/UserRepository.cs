using Core.Abstractions;
using Core.Models;
using Core.Repositories.ApiModels;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Core.Repositories;

internal class UserRepository(
        IWpfConfiguration _configuration,
        IHttpClientFactory _httpClientFactory
    ) : IUserRepository
{
    public async Task<bool> JoinChat(User user)
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var request = new JoinChatRequest
            {
                User = user,
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Route.Join, content);

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return true;
            }

            return false;
        }
    }

    public async Task LeaveChat(User user)
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_configuration.ChatControllerAddress);

            var request = new LeaveChatRequest
            {
                User = user
            };

            var jsonObject = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(Route.Leave, content);
        }
    }
}
