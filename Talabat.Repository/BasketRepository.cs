using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    { 
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
           _database =  redis.GetDatabase();
        }
        public Task<bool> DeleteBasketAsync(string basketId)
        {
           return _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket = await _database.StringGetAsync(basketId);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var UpdatedOrCreatedBasket = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(1));
            if (UpdatedOrCreatedBasket is false) return null;
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
