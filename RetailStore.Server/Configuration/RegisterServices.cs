namespace RetailStore.Server.Configuration;

public static class RegisterServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAppServices();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo{ Title = "Retail Store API v1"});
        });
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

        // add data
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

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
