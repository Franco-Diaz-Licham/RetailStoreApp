namespace RetailStore.Server.Models.Dtos;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string? PictureUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
