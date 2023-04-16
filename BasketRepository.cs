using Demo.BLL.Entities;
using Demo.BLL.Inerfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        =>await _database.KeyDeleteAsync(BasketId);

        public async Task<CutomerBasket> GetBasketAsync(string BasketId)
        {
            var Data = await _database.StringGetAsync(BasketId);
            return Data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CutomerBasket>(Data);
        }

        public async Task<CutomerBasket> UpdateBasketAsync(CutomerBasket Basket)
        {
            var creat = await _database.StringSetAsync(Basket.Id, JsonSerializer.Serialize<CutomerBasket>(Basket), TimeSpan.FromDays(30));
            if (!creat)
                return null;
            return await GetBasketAsync(Basket.Id);
        }
    }
}
