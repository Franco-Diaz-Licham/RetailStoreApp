namespace RetailStore.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IGenericRepository<ProductEntity> _repo;
    private readonly IMapper _mapper;

    public ProductController(IGenericRepository<ProductEntity> repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] ProductSpecificationParams productParams)
    {
        var specs = new ProductsWithTypesAndBrandsSpecification(productParams);
        var output = await _repo.GetAllAsync(specs);

        if (output == null) return NotFound();
        return Ok(_mapper.Map<List<ProductDto>>(output));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var specs = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _repo.GetEntityWithSpec(specs);
        var output = _mapper.Map<ProductDto>(product);
        
        if (output == null) return NotFound();
        return Ok(output);
    }
}


