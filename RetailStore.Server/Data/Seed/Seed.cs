using System.Text.Json;

namespace RetailStore.Server.Data.Seed;

public class Seed
{
    public static async Task SeedProducts(DataContext db)
    {
        if(await db.Products.AnyAsync()) return;
        var data = await File.ReadAllTextAsync("Data/Seed/ProductsSeedData.json");
        var products = JsonSerializer.Deserialize<List<ProductEntity>>(data);

        if(products == null) return;

        await db.Products.AddRangeAsync(products);
        await db.SaveChangesAsync();
    }
}
