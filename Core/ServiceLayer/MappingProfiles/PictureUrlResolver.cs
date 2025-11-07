using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Models;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;

namespace ServiceLayer.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver< Product , ProductDto,string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
                return string.Empty;
            else 
            {
                var url = $"{_configuration.GetSection("Urls")["Baseurl"]}{ source.PictureUrl}";
                return url;
            }
        }
    }
}
