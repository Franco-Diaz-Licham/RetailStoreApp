namespace RetailStore.Server.Models.Specifications;

/// <summary>
/// Product implementation of the BaseSpecification criteria used to build a IQueryable for entity framework.
/// </summary>
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<ProductEntity>
{
    /// <summary>
    /// Filters data specified by the query parameters from the API, includes product type and brand, orders by product name,
    /// applies paging and sorts data if specified.
    /// </summary>
    /// <param name="productParams"></param>
    public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
    {
        AddInclude(x => x.ProductType!);
        AddInclude(x => x.ProductBrand!);
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

    /// <summary>
    /// Filters data by product id and includes product type and product brand.
    /// </summary>
    /// <param name="id"></param>
    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType!);
        AddInclude(x => x.ProductBrand!);
    }
}
