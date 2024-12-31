namespace RetailStore.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductBrandEntity> ProductBrands { get; set; }
    public DbSet<ProductTypeEntity> ProductTypes { get; set; }
    public DbSet<DeliveryMethodEntity> DeliveryMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
