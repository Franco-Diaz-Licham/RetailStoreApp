namespace RetailStore.Server.Controllers;

public class ProductBrandController : BaseApiController
{
    private readonly IGenericRepository<ProductBrandEntity> _repo;

    public ProductBrandController(IGenericRepository<ProductBrandEntity> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ProductBrandEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductBrandEntity>>> GetAllAsync()
    {
        var output = await _repo.GetAllAsync();
        if (output == null) return NotFound();
        return Ok(output);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductBrandEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductBrandEntity>> GetAsync(int id)
    {
        var output = await _repo.GetByIdAsync(id);
        if (output == null) return NotFound();
        return Ok(output);
    }
}
