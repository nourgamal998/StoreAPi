using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModels;
using Shared.DTOS;
using Shared.DTOS.OrderDTOS;

namespace ServiceLayer.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
           CreateMap<AddressDto, AddressDto>();

            CreateMap<Order,OrderToReturnDto>()
                .ForMember(dest=>dest.DeliveryMethod , 
                opt=>opt.MapFrom(src=>src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                //.ForMember(dest => dest.ProductId,
                //opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.productName,
                opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl,
                opt => opt.MapFrom(src => src.Product.PictureUrl));
        }
    }
}
