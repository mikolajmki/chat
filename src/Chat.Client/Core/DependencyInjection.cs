using Core.Abstractions;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddHttpClient<IUserRepository, UserRepository>();
        services.AddHttpClient<IMessageRepository, MessageRepository>();
        services.AddHttpClient<IStatsRepository, StatsRepository>();

        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IStatsRepository, StatsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IStatsService, StatsService>();

        return services;
    }
}
