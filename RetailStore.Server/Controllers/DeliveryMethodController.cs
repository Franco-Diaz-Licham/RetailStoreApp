namespace RetailStore.Server.Controllers;

public class DeliveryMethodController : BaseApiController
{
    private readonly IGenericRepository<DeliveryMethodEntity> _repo;

    public DeliveryMethodController(IGenericRepository<DeliveryMethodEntity> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pagination<DeliveryMethodEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<DeliveryMethodEntity>>> GetAllAsync()
    {
        var output = await _repo.GetAllAsync();
        if (output == null) return NotFound();
        return Ok(output);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DeliveryMethodEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryMethodEntity>> GetAsync(int id)
    {
        var output = await _repo.GetByIdAsync(id);
        if (output == null) return NotFound();
        return Ok(output);
    }
}
