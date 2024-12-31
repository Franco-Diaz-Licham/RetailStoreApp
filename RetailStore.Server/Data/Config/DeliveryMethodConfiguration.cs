namespace RetailStore.Server.Data.Config;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethodEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryMethodEntity> builder)
    {
        builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
    }
}