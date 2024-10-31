using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create-payment-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] decimal amount)
    {
        var paymentIntent = await _paymentService.CreatePaymentIntent(amount);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }
}
