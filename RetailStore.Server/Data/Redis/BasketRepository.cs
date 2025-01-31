namespace RetailStore.Server.Data.Redis;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _db;
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _db.KeyDeleteAsync(basketId);
    }

    public async Task<CustomerBasketModel?> GetBasketAsync(string basketId)
    {
        var data = await _db.StringGetAsync(basketId);
        var output = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasketModel>(data!);
        return output;
    }

    public async Task<CustomerBasketModel?> UpdateBasketAsync(CustomerBasketModel basket)
    {
        var time = TimeSpan.FromDays(30);
        string content = JsonSerializer.Serialize(basket);
        var created = await _db.StringSetAsync(basket.Id, content, time);

        if (!created) return null;
        return await GetBasketAsync(basket.Id);
    }
}
