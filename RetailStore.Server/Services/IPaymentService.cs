namespace RetailStore.Server.Services;

public interface IPaymentService
{
    Task<CustomerBasketModel?> CreateOrUpdatePaymentIntent(string basketId);
    Task<OrderEntity?> UpdateOrderPaymentSucceeded(string paymentIntentId);
    Task<OrderEntity?> UpdateOrderPaymentFailed(string paymentIntentId);
}
