using Core.Abstractions;
using Core.Models;
using Core.Repositories.ApiModels;
using Newtonsoft.Json;

namespace Core.Repositories;

internal class StatsRepository(
        HttpClient _httpClient
    ) : IStatsRepository
{
    public async Task<Stats> GetStats()
    {
        var response = await _httpClient.GetAsync(Route.GetStats);

        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GetStatsResponse>(responseBody);

        var stats = result.Stats;

        return stats;
    }
}
