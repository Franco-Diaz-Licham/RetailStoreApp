namespace RetailStore.Server.Controllers;

public class PaymentsController : BaseApiController
{
    private readonly string _whSecret;
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;
    public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger, IConfiguration config)
    {
        _logger = logger;
        _paymentService = paymentService;
        _whSecret = config.GetValue<string>("StripeSettings:WhSecret")!;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasketModel>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));
        return basket;
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

        PaymentIntent? intent;
        OrderEntity? order;

        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment succeeded: {intent.Id}", intent.Id);
                order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                _logger.LogInformation("Order updated to payment received: {0}", order!.Id);
                break;
            case "payment_intent.payment_failed":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment failed: {0}", intent.Id);
                order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                _logger.LogInformation("Order updated to payment failed: {0}", order!.Id);
                break;
        }

        return new EmptyResult();
    }
}