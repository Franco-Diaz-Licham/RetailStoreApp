namespace RetailStore.Server.Models.Entities;

public class OrderItemEntity : BaseEntity
{
    public OrderItemEntity()
    {
    }

    public OrderItemEntity(ProductItemOrderedEntity itemOrdered, decimal price, int quantity)
    {
        ItemOrdered = itemOrdered;
        Price = price;
        Quantity = quantity;
    }

    public ProductItemOrderedEntity ItemOrdered { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
