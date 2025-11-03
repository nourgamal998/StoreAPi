using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Models;
using Shared.DTOS;

namespace ServiceLayer.MappingProfiles
{
    public class ProductProfile :Profile 
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
           .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
           .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name));
          

            CreateMap<ProductType,TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
        }
    }
}   
