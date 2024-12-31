namespace RetailStore.Server.Data.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _db;
    
    public ProductRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<ProductEntity?> GetProductByIdAsync(int id)
    {
        return await _db.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsAsync()
    {
        return await _db.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).ToListAsync();
    }
}
