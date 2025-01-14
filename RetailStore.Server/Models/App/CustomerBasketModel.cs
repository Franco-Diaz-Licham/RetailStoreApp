namespace RetailStore.Server.Models.App;

public class CustomerBasketModel
{
    public CustomerBasketModel(){ }

    public CustomerBasketModel(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
    public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
    public int? DeliveryMethodId { get; set; }
    public string ClientSecret { get; set; }
    public string PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
}
