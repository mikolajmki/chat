using Application.ApplicationModels;
using Mapster;
using MapsterMapper;
using Presentation.PresentationModels;
using System.Reflection;
namespace WebApi.Configuration;

public static class MapsterConfig
{
    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        config
            .NewConfig<UserApiModel, UserDto>()
            .Map(dest => dest.Id, src => src.Id == string.Empty ? Guid.Empty : Guid.Parse(src.Id));

        config.Scan(Assembly.GetExecutingAssembly());

        var mapperConfig = new Mapper(config);

        services.AddSingleton<IMapper>(mapperConfig);

        return services;
    }
}
