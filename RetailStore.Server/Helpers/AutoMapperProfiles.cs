namespace RetailStore.Server.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand!.Name))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType!.Name))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
        CreateMap<CustomerBasketDto, CustomerBasketModel>();
        CreateMap<BasketItemDto, BasketItemModel>();
        CreateMap<AddressEntity, AddressDto>().ReverseMap();
    }
}
