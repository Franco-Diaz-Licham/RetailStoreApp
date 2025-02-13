namespace RetailStore.Server.Controllers;

public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _mapper = mapper;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        var email = User.RetrieveEmailFromPrincipal()!;
        var address = _mapper.Map<AddressDto, AddressOwnedEntity>(orderDto.ShipToAddress);
        var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
        if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var email = User.RetrieveEmailFromPrincipal();
        var orders = await _orderService.GetOrdersForUserAsync(email);
        return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
    {
        var email = User.RetrieveEmailFromPrincipal();
        var order = await _orderService.GetOrderByIdAsync(id, email);
        if (order == null) return NotFound(new ApiResponse(404));
        return _mapper.Map<OrderToReturnDto>(order);
    }
}
