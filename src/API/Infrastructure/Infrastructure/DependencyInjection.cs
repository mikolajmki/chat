using Application.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddSingleton<DataContext>();

        return services;
    }
}
