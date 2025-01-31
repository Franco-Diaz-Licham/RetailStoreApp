namespace RetailStore.Server.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.OwnsOne(o => o.ShipToAddress, a => a.WithOwner());
        builder.Navigation(a => a.ShipToAddress).IsRequired();
        builder.Property(s => s.Status).HasConversion(
            o => o.ToString(),
            o => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum),
            o)
        );
        builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
