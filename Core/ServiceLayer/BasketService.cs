using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exeptions;
using DomanLayer.Models.BasketModels;
using ServiceApstractionLayer;
using Shared.DTOS.BasketDTOS;

namespace ServiceLayer
{
    public class BasketService(IBasketReposatory _basketReposatory, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketReposatory.CreatOrUpdateBasketAsync(CustomerBasket);

            if (CreatedOrUpdatedBasket is not null) return await GetBasketAsync(basket.Id);
            else throw new Exception($"Can Not Create Or Update Basket , Try Again Later ");

        }
  

        public async Task<bool> DeleteBasketAsync(string Key)
        => await _basketReposatory.DeleteBasketAsync(Key);

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket= await _basketReposatory.GetBasketAsync(Key);
            if (Basket is not null)
            return _mapper.Map<BasketDto>(Basket);

            else throw new BasketNotFoundExeption(Key);
        }
    }
}
