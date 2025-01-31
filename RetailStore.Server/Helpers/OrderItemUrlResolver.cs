namespace RetailStore.Server.Helpers;

public class OrderItemUrlResolver : IValueResolver<OrderItemEntity, OrderItemDto, string?>
{
    private readonly IConfiguration _config;
    public OrderItemUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string? Resolve(OrderItemEntity source, OrderItemDto destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl)) return _config.GetValue<string>("ApiUrl") + source.ItemOrdered.PictureUrl;
        return null;
    }
}
