namespace RetailStore.Server.Models.Specifications;

/// <summary>
/// Order implementation of the BaseSpecification criteria used to build a IQueryable for entity framework.
/// </summary>
public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<OrderEntity>
{
    /// <summary>
    /// Filters data by email of the user, includes order items and delivery method and orders by descending base don order date.
    /// </summary>
    /// <param name="email"></param>
    public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
        AddOrderByDescending(o => o.OrderDate);
    }

    /// <summary>
    /// Filters data by email and order id and includes orde ritems and devlierty method.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    public OrdersWithItemsAndOrderingSpecification(int id, string email) : base(o => o.Id == id && o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
    }
}