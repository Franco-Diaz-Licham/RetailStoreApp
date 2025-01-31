namespace RetailStore.Server.Models.Entities;

public class OrderEntity : BaseEntity
{
    public OrderEntity(){ }
    
    public OrderEntity(
        IReadOnlyList<OrderItemEntity> orderItems, 
        string buyerEmail, 
        AddressOwnedEntity shipToAddress,
        DeliveryMethodEntity deliveryMethod, 
        decimal subtotal, 
        string paymentIntentId)
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
        PaymentIntentId = paymentIntentId;
    }

    public string BuyerEmail { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public AddressOwnedEntity ShipToAddress { get; set; }
    public DeliveryMethodEntity DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItemEntity> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public string PaymentIntentId { get; set; }

    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }
}