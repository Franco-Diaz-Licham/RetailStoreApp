namespace RetailStore.Server.Models.Entities;

[Table("Products")]
public class ProductEntity: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public int ProductTypeId { get; set; }
    public ProductTypeEntity? ProductType { get; set; }
    public int ProductBrandId { get; set; }
    public ProductBrandEntity? ProductBrand { get; set; }
}

