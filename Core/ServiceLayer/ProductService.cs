using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exeptions;
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
        //public interface IProductService;
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync
            (ProductQueryParams queryParams )
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            //create object from repository
            var specs=new ProductWithBrandAndTypeSpecifications(queryParams);
            var products = await repo.GetAsync(specs);
            var mappedproducts= _mapper.Map<IEnumerable<ProductDto>>(products);
            var countSpecs = new ProductCountSpecifications(queryParams);
            var totalCount = await repo.CountAsync(countSpecs);

            return new PaginatedResult<ProductDto>(queryParams.PageIndex,queryParams.PageSize
                                                                        , totalCount, mappedproducts);

        }

        public async Task<ProductDto> GetProductsByIdAsync(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            if ( product is null) throw new ProductNotFoundExeption(id);
            return _mapper.Map<ProductDto>(product);
        }

    }
}

