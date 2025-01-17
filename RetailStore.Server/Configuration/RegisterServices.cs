namespace RetailStore.Server.Configuration;

public static class RegisterServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        // basic services
        services.AddAppServices();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddIdentityServices(config);
        services.AddSwaggerService();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

        // redis
        services.AddSingleton<IConnectionMultiplexer>(opt => {
            var configure = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!, true);
            return ConnectionMultiplexer.Connect(configure);
        });

        // model validation api response.
        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.InvalidModelStateResponseFactory = actionContext =>
            {
                // make all validations errors into array
                var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value?.Errors!)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse(errors);
                return new BadRequestObjectResult(errorResponse);
            };
        });
    }
}
