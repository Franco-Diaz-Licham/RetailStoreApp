namespace RetailStore.Server.Controllers;

public class ProductTypeController : BaseApiController
{
    private readonly IGenericRepository<ProductTypeEntity> _repo;

    public ProductTypeController(IGenericRepository<ProductTypeEntity> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ProductTypeEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductTypeEntity>>> GetAllAsync()
    {
        var output = await _repo.GetAllAsync();
        if (output == null) return NotFound();
        return Ok(output);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductTypeEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductTypeEntity>> GetAsync(int id)
    {
        var output = await _repo.GetByIdAsync(id);
        if (output == null) return NotFound();
        return Ok(output);
    }
}
