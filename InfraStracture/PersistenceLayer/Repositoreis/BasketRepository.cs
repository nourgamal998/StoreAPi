using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models.BasketModels;
using StackExchange.Redis;

namespace PersistenceLayer.Repositoreis
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketReposatory
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket> CreatOrUpdateBasketAsync(CustomerBasket basket,TimeSpan? TimeToLive = null)
        {
           var JsonBasket = JsonSerializer.Serialize(basket);
           var IsCreatedOrUpdated= await _database.StringSetAsync(basket.Id,JsonBasket, TimeToLive?? TimeSpan.FromDays(30));

            if (IsCreatedOrUpdated) { return basket; }

             else return null!;
            

        }

        public async Task<bool> DeleteBasketAsync(string Key)
         => await _database .KeyDeleteAsync(Key);


        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var basket = await _database.StringGetAsync(Key);
            if (basket.IsNullOrEmpty) return null;
            else return JsonSerializer.Deserialize<CustomerBasket>(basket!);

        }
    }
}
