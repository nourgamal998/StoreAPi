
using AutoMapper;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.OrderDTOS;

namespace ServiceLayer.MappingProfiles
{
    public class OrderItemPictureUrlResolver(IConfiguration _configuration)
                 : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                var url = $"{_configuration.GetSection("Urls")["Baseurl"]}{source.Product.PictureUrl}";
                return url;
            }
        }

    }
}
