namespace RetailStore.Server.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _uow;
    public OrderService(IBasketRepository basketRepo, IUnitOfWork uow)
    {
        _uow = uow;
        _basketRepo = basketRepo;
    }

    public async Task<OrderEntity?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, AddressOwnedEntity shippingAddress)
    {
        var basket = await _basketRepo.GetBasketAsync(basketId);
        var items = new List<OrderItemEntity>();

        foreach (var item in basket.Items)
        {
            var productItem = await _uow.Repository<ProductEntity>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrderedOwnedEntity(productItem!.Id, productItem.Name, productItem.PictureUrl);
            var OrderItemEntity = new OrderItemEntity(itemOrdered, productItem.Price, item.Quantity);
            items.Add(OrderItemEntity);
        }

        var deliveryMethod = await _uow.Repository<DeliveryMethodEntity>().GetByIdAsync(deliveryMethodId);
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
        var order = await _uow.Repository<OrderEntity>().GetEntityWithSpec(spec);

        if (order != null)
        {
            order.ShipToAddress = shippingAddress;
            order.DeliveryMethod = deliveryMethod;
            order.Subtotal = subtotal;
            _uow.Repository<OrderEntity>().Update(order);
        }
        else
        {
            order = new OrderEntity(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);
            _uow.Repository<OrderEntity>().Add(order);
        }

        var result = await _uow.Complete();
        if (result <= 0) return null;
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethodEntity>> GetDeliveryMethodsAsync()
    {
        var output = await _uow.Repository<DeliveryMethodEntity>().GetAllAsync();
        return output;
    }

    public async Task<OrderEntity?> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
        var output = await _uow.Repository<OrderEntity>().GetEntityWithSpec(spec);
        return output;
    }

    public async Task<IReadOnlyList<OrderEntity>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
        var output = await _uow.Repository<OrderEntity>().GetAllAsync(spec);
        return output;
    }
}
