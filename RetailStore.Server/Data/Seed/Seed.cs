namespace RetailStore.Server.Data.Seed;

public class Seed
{
    private const string BASE_PATH = "Data/Seed/";
    private const string PRODUCTS = BASE_PATH + "ProductsData.json";
    private const string PRODUCT_BRANDS = BASE_PATH + "ProductBrandsData.json";
    private const string DELIVERY_METHODS = BASE_PATH + "DeliveryMethodsData.json";
    private const string PRODUCT_TYPES = BASE_PATH + "ProductTypesData.json";
    private const string USERS = BASE_PATH + "UsersData.json";

    public static async Task Products(DataContext db)
    {
        if (await db.Products.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(PRODUCTS);
        var models = JsonSerializer.Deserialize<List<ProductEntity>>(data);
        if (models == null) return;
        await db.Products.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    public static async Task ProductBrands(DataContext db)
    {
        if (await db.ProductBrands.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(PRODUCT_BRANDS);
        var models = JsonSerializer.Deserialize<List<ProductBrandEntity>>(data);
        if (models == null) return;
        await db.ProductBrands.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    public static async Task DeliveryMethods(DataContext db)
    {
        if (await db.DeliveryMethods.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(DELIVERY_METHODS);
        var models = JsonSerializer.Deserialize<List<DeliveryMethodEntity>>(data);
        if (models == null) return;
        await db.DeliveryMethods.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    public static async Task ProductTypes(DataContext db)
    {
        if (await db.ProductTypes.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(PRODUCT_TYPES);
        var models = JsonSerializer.Deserialize<List<ProductTypeEntity>>(data);
        if (models == null) return;
        await db.ProductTypes.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    public static async Task SeedUsersAsync(UserManager<UserEntity> manager)
    {    
        if (await manager.Users.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(USERS);
        var models = JsonSerializer.Deserialize<List<UserEntity>>(data);
        if (models == null) return;
        foreach(var m in models) await manager.CreateAsync(m, "Pa$$w0rd");
    }
}
