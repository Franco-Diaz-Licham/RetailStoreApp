namespace RetailStore.Server.Data.Redis;

public interface IBasketRepository
{
    Task<bool> DeleteBasketAsync(string basketId);
    Task<CustomerBasketModel?> GetBasketAsync(string basketId);
    Task<CustomerBasketModel?> UpdateBasketAsync(CustomerBasketModel basket);
}
