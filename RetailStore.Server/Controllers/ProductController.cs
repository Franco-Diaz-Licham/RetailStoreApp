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
    public async Task<ActionResult<List<ProductEntity>>> GetProducts([FromQuery] ProductSpecificationParams productParams)
    {
        var specs = new ProductsWithTypesAndBrandsSpecification(productParams);
        var output = await _repo.GetAllAsync(specs);

        if (output == null) return NotFound();
        return Ok(_mapper.Map<List<ProductDto>>(output));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        // var product = await _repo.GetByIdAsync(id);
        // var output = _mapper.Map<ProductDto>(product);
        // if (output == null) return NotFound();

        var output = await _repo.GetByIdAndMapAsync<ProductDto>(id);

        return Ok(output);
    }
}


