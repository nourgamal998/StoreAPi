using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using ServiceApstractionLayer;
using ServiceLayer.Services;

namespace ServiceLayer
{
    public class ServiceManeger(IUnitOfWork _unitOfWork ,IMapper mapper ,IBasketReposatory _basketReposatory) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyproductService =
        new Lazy<IProductService>(() => new ProductService(_unitOfWork, mapper));
        public IProductService ProductService => _lazyproductService.Value;


        private readonly Lazy<IBasketService> _lazyBasketService =
      new Lazy<IBasketService>(() => new BasketService(_basketReposatory, mapper));
        public IBasketService BasketService => _lazyBasketService.Value;

    }
}
