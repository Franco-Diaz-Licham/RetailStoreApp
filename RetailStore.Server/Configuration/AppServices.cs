namespace RetailStore.Server.Configuration;

public static class AppServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        return services;
    }
}
