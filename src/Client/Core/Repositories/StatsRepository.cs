using Application.Abstractions;
using Core.Abstractions;
using Core.Repositories.IO;
using Newtonsoft.Json;

namespace Core.Repositories;

internal class StatsRepository(
        IHttpClientFactory _httpClientFactory,
        IWpfConfiguration _wpfConfiguration
    ) : IStatsRepository
{
    public async Task<GetStatsResponse> GetStats()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_wpfConfiguration.ChatControllerAddress);

            var response = await client.GetAsync(Route.GetMessages);

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetStatsResponse>(responseBody);

            return result;
        }
    }
}
