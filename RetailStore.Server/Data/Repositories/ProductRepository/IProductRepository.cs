namespace RetailStore.Server.Data.Repositories.ProductRepository;

public interface IProductRepository
{
    Task<ProductEntity?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<ProductEntity>> GetProductsAsync();
}
