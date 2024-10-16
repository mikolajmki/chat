using Application.Abstractions;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IStatsRepository, StatsRepository>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IStatsService, StatsService>();

        return services;
    }
}
