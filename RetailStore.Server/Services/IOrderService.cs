namespace RetailStore.Server.Services;

public interface IOrderService
{
    Task<OrderEntity?> CreateOrderAsync(string buyerEmail, int delieveryMethod, string basketId, AddressOwnedEntity shippingAddress);
    Task<IReadOnlyList<OrderEntity>> GetOrdersForUserAsync(string buyerEmail);
    Task<OrderEntity?> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethodEntity>> GetDeliveryMethodsAsync();
}