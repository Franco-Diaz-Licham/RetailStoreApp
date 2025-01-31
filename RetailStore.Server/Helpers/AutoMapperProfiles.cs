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
        CreateMap<AddressDto, AddressOwnedEntity>();
        CreateMap<AddressEntity, AddressOwnedEntity>();
        CreateMap<OrderEntity, OrderToReturnDto>()
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(s => s.DeliveryMethod.Price));
        CreateMap<OrderItemEntity, OrderItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemUrlResolver>());
    }
}