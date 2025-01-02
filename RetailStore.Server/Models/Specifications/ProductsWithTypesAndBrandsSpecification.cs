namespace RetailStore.Server.Models.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<ProductEntity>
{
    /// <summary>
    /// Specification which gets all items that comply with the speficiation but returns the number of items according to paging.
    /// </summary>
    /// <param name="productParams"></param>
    public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(x => x.Name);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc": AddOrderBy(p => p.Price); break;
                case "priceDesc": AddOrderByDescending(p => p.Price); break;
                default: AddOrderBy(n => n.Name); break;
            }
        }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
