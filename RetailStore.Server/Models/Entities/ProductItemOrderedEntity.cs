namespace RetailStore.Server.Models.Entities;

public class ProductItemOrderedEntity
{
    public ProductItemOrderedEntity()
    {
    }

    public ProductItemOrderedEntity(int productItemId, string productName, string pictureUrl)
    {
        ProductItemId = productItemId;
        ProductName = productName;
        PictureUrl = pictureUrl;
    }

    public int ProductItemId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
}
