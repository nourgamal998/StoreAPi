using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Models.BasketModels;
using Shared.DTOS.BasketDTOS;

namespace ServiceLayer.MappingProfiles
{
    public class BasketMappingProfile:Profile
    {
        public BasketMappingProfile() 
        { 
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
