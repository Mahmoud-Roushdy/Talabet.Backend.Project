using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Service.DTOs;

namespace Talabat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        public IBasketRepository _BasketRepository { get; }
        public IMapper _Mapper { get; }

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _BasketRepository = basketRepository;
            _Mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _BasketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdataBasket(CustomerBasketDto basket)
        {
            var customerbasket = _Mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var CreadtedOrUpdatedBasket = await _BasketRepository.UpdateBasketAsync(customerbasket);
            if (CreadtedOrUpdatedBasket is null) return BadRequest();
            return CreadtedOrUpdatedBasket;


        }
        [HttpDelete]
        public async Task DeleteBasket (string basketid)
        {
             await _BasketRepository.DeleteBasketAsync(basketid); 
        }
    }
}
