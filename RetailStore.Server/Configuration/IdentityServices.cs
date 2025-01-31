namespace RetailStore.Server.Configuration;

public static class IdentityServices
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<UserEntity>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddUserManager<UserManager<UserEntity>>()
        .AddSignInManager<SignInManager<UserEntity>>()
        .AddEntityFrameworkStores<DataContext>();

        // authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            // client send token as auth header
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Token:Key")!)),
                ValidateIssuer = true,
                ValidIssuer = config.GetValue<string>("Token:Issuer")!,
                ValidateAudience = false
            };
        });

        services.AddAuthorization();
        
        return services;
    }
}
