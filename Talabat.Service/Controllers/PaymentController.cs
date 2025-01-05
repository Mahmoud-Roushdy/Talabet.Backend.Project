using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Services;
using Talabat.Service.DTOs;

namespace Talabat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _PaymentService { get; }
        public PaymentController(IPaymentService paymentService)
        {
            _PaymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePayment (string BasketId)
        {
            var basket = await _PaymentService.CreateOrUpdatePaymentIntent(BasketId);
            if (basket is null) return NotFound();  
            return Ok(basket); 

        }
    }
}
