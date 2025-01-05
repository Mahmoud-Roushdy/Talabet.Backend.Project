using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Identity.Order_Aggregate;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Specification;
using Talabat.Service.DTOs;

namespace Talabat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto OrderDto)
        {
            if (ModelState.IsValid)
            {
                var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
                var address = _mapper.Map<AddressDto, Address>(OrderDto.ShippingAddress);
                var order = await _orderService.CreateOrderAsync(BuyerEmail, OrderDto.BasketId, OrderDto.DeliveryMethodId, address);
                var OrderToReturn = _mapper.Map<Order, OrderToReturnDto>(order);
                if (order is null) return BadRequest();
                return Ok(OrderToReturn);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(BuyerEmail);
             var OrdersDto = _mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(OrdersDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser (int OrderId)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
             var order = await _orderService.GetOrderByIdForUserAsync(OrderId, BuyerEmail);
            if (order is null) return NotFound();
            return Ok(order);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
        {
            var DeliveryMethods = await _orderService.GetAllDeliveryMethods();
            return Ok(DeliveryMethods);
        }
    }
}
