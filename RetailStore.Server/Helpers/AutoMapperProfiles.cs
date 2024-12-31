namespace RetailStore.Server.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.ProductBrand, act => act.MapFrom(src => src.ProductBrand!.Name))
            .ForMember(dest => dest.ProductType, act => act.MapFrom(src => src.ProductType!.Name))
            .ForMember(dest => dest.PictureUrl, act => act.MapFrom<ProductUrlResolver>());
            // .ForMember(dest => dest.PictureUrl, act => act.MapFrom(src => src.PictureUrl + "test"));
    }
}
