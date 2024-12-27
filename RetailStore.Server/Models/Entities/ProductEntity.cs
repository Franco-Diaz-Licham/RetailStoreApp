namespace RetailStore.Server.Models.Entities;

[Table("Products")]
public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

