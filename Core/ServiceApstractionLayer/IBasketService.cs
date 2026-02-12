using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using Shared.DTOS.BasketDTOS;

namespace ServiceApstractionLayer
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string Key);
        
    }
}
