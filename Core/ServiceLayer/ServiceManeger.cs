using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Models.Identity_models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceApstractionLayer;
using ServiceLayer.Services;

namespace ServiceLayer
{
    public class ServiceManeger
        (IUnitOfWork _unitOfWork ,IMapper mapper ,
        IBasketReposatory _basketReposatory ,
        UserManager<ApplicationUser> userManager,
        IConfiguration _configuration) : IServiceManager

    {
        private readonly Lazy<IProductService> _lazyproductService =
        new Lazy<IProductService>(() => new ProductService(_unitOfWork, mapper));
        public IProductService ProductService => _lazyproductService.Value;


        private readonly Lazy<IBasketService> _lazyBasketService =
        new Lazy<IBasketService>(() => new BasketService(_basketReposatory, mapper));
        public IBasketService BasketService => _lazyBasketService.Value;


        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
       new Lazy<IAuthenticationService>(() => new AuthenticationService( userManager, _configuration));
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;


    }
}
