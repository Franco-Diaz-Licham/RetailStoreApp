namespace RetailStore.Server.Models.Entities;

[Table("ProductTypes")]
public class ProductTypeEntity: BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
