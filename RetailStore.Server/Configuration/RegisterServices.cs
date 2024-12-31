namespace RetailStore.Server.Configuration;

public static class RegisterServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

        // add data
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
