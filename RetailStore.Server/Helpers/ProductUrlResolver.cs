namespace RetailStore.Server.Helpers;

public class ProductUrlResolver : IValueResolver<ProductEntity, ProductDto, string?>
{
    private readonly IConfiguration _config;
    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string? Resolve(ProductEntity source, ProductDto destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl)) return _config.GetValue<string>("ApiUrl") + source.PictureUrl;
        return null;
    }
}
 