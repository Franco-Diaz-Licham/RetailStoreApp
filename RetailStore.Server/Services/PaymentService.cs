namespace RetailStore.Server.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepository;
    private readonly IConfiguration _config;
    public PaymentService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IConfiguration config)
    {
        _config = config;
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerBasketModel?> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _config.GetValue<string>("StripeSettings:SecretKey");
        var basket = await _basketRepository.GetBasketAsync(basketId);

        if (basket == null) return null;
        var shippingPrice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethodEntity>().GetByIdAsync((int)basket.DeliveryMethodId);
            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.Repository<ProductEntity>().GetByIdAsync(item.Id);
            if (item.Price != productItem.Price) item.Price = productItem.Price;
        }

        var service = new PaymentIntentService();
        PaymentIntent intent;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                Currency = "AUD",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketRepository.UpdateBasketAsync(basket);
        return basket;
    }

    public async Task<OrderEntity?> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unitOfWork.Repository<OrderEntity>().GetEntityWithSpec(spec);
        if (order == null) return null;
        order.Status = OrderStatusEnum.PaymentFailed;
        await _unitOfWork.Complete();
        return order;
    }

    public async Task<OrderEntity?> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unitOfWork.Repository<OrderEntity>().GetEntityWithSpec(spec);
        if (order == null) return null;
        order.Status = OrderStatusEnum.PaymentReceived;
        await _unitOfWork.Complete();
        return order;
    }
}
