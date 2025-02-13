namespace RetailStore.Server.Models.Entities;

public class CustomerBasketEntity : BaseEntity
{
    public CustomerBasketEntity() { }

    public CustomerBasketEntity(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
    public List<BasketItemEntity> Items { get; set; } = new();
    public int? DeliveryMethodId { get; set; }
    public string ClientSecret { get; set; }
    public string PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
}
