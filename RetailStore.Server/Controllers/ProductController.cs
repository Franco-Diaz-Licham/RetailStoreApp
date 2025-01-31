namespace RetailStore.Server.Controllers;


public class ProductController : BaseApiController
{
    private readonly IGenericRepository<ProductEntity> _repo;
    private readonly IMapper _mapper;

    public ProductController(IGenericRepository<ProductEntity> repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] ProductSpecificationParams productParams)
    {
        var specs = new ProductsWithTypesAndBrandsSpecification(productParams);
        var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
        
        var products = await _repo.GetAllAsync(specs);
        if (products == null) return NotFound();

        var data = _mapper.Map<List<ProductDto>>(products);
        var totalItems = await _repo.CountAsync(countSpec);
        var output = new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data);
        
        return Ok(output);
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


