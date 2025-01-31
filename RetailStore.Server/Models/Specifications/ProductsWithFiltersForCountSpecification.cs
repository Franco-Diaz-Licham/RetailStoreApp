namespace RetailStore.Server.Models.Specifications;

/// <summary>
/// Product implementation of the BaseSpecification criteria used to build a IQueryable for entity framework.
/// </summary>
public class ProductsWithFiltersForCountSpecification : BaseSpecification<ProductEntity>
{
    /// <summary>
    /// Filters data specified by the query parameters from the API.
    /// </summary>
    /// <param name="productParams"></param>
    public ProductsWithFiltersForCountSpecification(ProductSpecificationParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)){ }
}
