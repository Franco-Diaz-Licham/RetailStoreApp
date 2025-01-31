namespace RetailStore.Server.Data.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.OwnsOne(i => i.ItemOrdered, io => io.WithOwner() );
        builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
    }
}