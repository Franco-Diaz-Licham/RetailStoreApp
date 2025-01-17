namespace RetailStore.Server.Data.Config;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.PictureUrl).IsRequired();
        builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
        builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
    }
}
