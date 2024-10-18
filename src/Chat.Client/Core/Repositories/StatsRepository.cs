using Core.Abstractions;
using Core.Models;
using Core.Repositories.ApiModels;
using Newtonsoft.Json;

namespace Core.Repositories;

internal class StatsRepository(
        IHttpClientFactory _httpClientFactory,
        IWpfConfiguration _wpfConfiguration
    ) : IStatsRepository
{
    public async Task<Stats> GetStats()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(_wpfConfiguration.ChatControllerAddress);

            var response = await client.GetAsync(Route.GetStats);

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetStatsResponse>(responseBody);

            var stats = result.Stats;

            return stats;
        }
    }
}
