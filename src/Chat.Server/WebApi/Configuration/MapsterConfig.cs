using Mapster;
using MapsterMapper;
using System.Reflection;

namespace WebApi.Configuration;

public static class MapsterConfig
{
    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        config.Scan(Assembly.GetExecutingAssembly());

        var mapperConfig = new Mapper(config);

        services.AddSingleton<IMapper>(mapperConfig);

        return services;
    }
}
