using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.BasketModels;

namespace DomanLayer.Contracts
{
    public interface IBasketReposatory
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);
        Task<CustomerBasket> CreatOrUpdateBasketAsync(CustomerBasket basket , TimeSpan? TimeToLive=null );
        Task<bool> DeleteBasketAsync(string Key );
    }
}
