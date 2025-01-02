namespace RetailStore.Server.Models.Specifications;

public class ProductsWithFiltersForCountSpecification : BaseSpecification<ProductEntity>
{
    /// <summary>
    /// Specification which counts all items int he db based on the criteria passed.
    /// </summary>
    /// <param name="productParams"></param>
    public ProductsWithFiltersForCountSpecification(ProductSpecificationParams productParams) : base(x =>
    (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
    (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
    (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
    {

    }
}
