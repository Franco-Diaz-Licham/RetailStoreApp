namespace RetailStore.Server.Models.Specifications;

/// <summary>
/// Order implementation of the BaseSpecification criteria used to build a IQueryable for entity framework.
/// </summary>
public class OrderByPaymentIntentIdSpecification : BaseSpecification<OrderEntity>
{
    /// <summary>
    /// Filters data by payment Intent Id.
    /// </summary>
    /// <param name="paymentIntentId"></param>
    public OrderByPaymentIntentIdSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId){}
}