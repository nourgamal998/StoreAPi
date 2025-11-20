using Shared;
using Shared.DTOS.ProductDTOS;

namespace ServiceApstractionLayer
{

    public interface IProductService
   
          {
        //get all products
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams); 
        //get product by id 
        Task<ProductDto> GetProductsByIdAsync(int Id);
        //get all types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        //get all brands
        Task<IEnumerable<BrandDto>> GetBrandsAsync();

        //Task GetProductByIdAsync(int id);
        //Task GetAllBrandsAsync();
          }
}
