using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Models;
using ServiceApstractionLayer;
using ServiceLayer.Specifications;
using Shared;
using Shared.DTOS;
namespace ServiceLayer .Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        #region types and brands
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }
        #endregion
        public interface IProductService;
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync
            (ProductQueryParams queryParams )
        {
            //create object from repository
            var specs=new ProductWithBrandAndTypeSpecifications(queryParams);

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<ProductDto>>(products);

        }

        public async Task<ProductDto> GetProductsByIdAsync(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        //public Task GetProductByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task GetAllBrandsAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

