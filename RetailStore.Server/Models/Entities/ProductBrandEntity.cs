namespace RetailStore.Server.Models.Entities;

[Table("ProductBrands")]
public class ProductBrandEntity: BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
