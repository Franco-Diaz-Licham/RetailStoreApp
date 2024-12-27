namespace RetailStore.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly DataContext _db;
    public ProductController(DataContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductEntity>>> GetProducts()
    {
        var output = await _db.Products.ToListAsync();

        if(output == null) return NotFound();
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductEntity>> GetProduct(int id)
    {
        var output = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        
        if(output == null) return NotFound();
        return Ok(output);
    }
}


