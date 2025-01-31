namespace RetailStore.Server.Models.Entities.OwnedEntities;

public class ProductItemOrderedOwnedEntity
{
    public ProductItemOrderedOwnedEntity()
    {
    }

    public ProductItemOrderedOwnedEntity(int productItemId, string productName, string pictureUrl)
    {
        ProductItemId = productItemId;
        ProductName = productName;
        PictureUrl = pictureUrl;
    }

    public int ProductItemId { get; set; }
    public string ProductName { get; set; } 
    public string PictureUrl { get; set; } = string.Empty;
}
