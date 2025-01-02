namespace RetailStore.Server.Data;

public class DataContext : DbContext
{
    private const string SQLITE_DB_NAME = "Microsoft.EntityFrameworkCore.Sqlite";
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductBrandEntity> ProductBrands { get; set; }
    public DbSet<ProductTypeEntity> ProductTypes { get; set; }
    public DbSet<DeliveryMethodEntity> DeliveryMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName == SQLITE_DB_NAME)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                foreach (var property in properties)
                {
                    builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }
            }
        }
    }
}
