using Core.Abstractions;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services, IWpfConfiguration config)
    {
        var baseAddress = new Uri(config.ChatControllerAddress);

        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IStatsRepository, StatsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddHttpClient<IUserRepository, UserRepository>(client => client.BaseAddress = baseAddress);
        services.AddHttpClient<IMessageRepository, MessageRepository>(client => client.BaseAddress = baseAddress);
        services.AddHttpClient<IStatsRepository, StatsRepository>(client => client.BaseAddress = baseAddress);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IStatsService, StatsService>();

        return services;
    }
}
