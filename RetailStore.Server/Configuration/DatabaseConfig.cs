namespace RetailStore.Server.Configuration;

public static class DatabaseConfig
{
    public static async Task ConfigureDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var db = services.GetRequiredService<DataContext>();
            await db.Database.MigrateAsync();

            // seed data
            await Seed.ProductBrands(db);
            await Seed.DeliveryMethods(db);
            await Seed.ProductTypes(db);
            await Seed.Products(db);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}
