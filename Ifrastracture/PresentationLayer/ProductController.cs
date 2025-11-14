using Microsoft.AspNetCore.Mvc;
using ServiceApstractionLayer;
using Shared;
using Shared.DTOS;


namespace PresentationLayer
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]//Get All Products
        public async Task<ActionResult<PaginatedResult<ProductDto>>>GetAllProducts
                                       ([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
                return Ok(products);
            
        }

        [HttpGet("{Id}")] //Get Product By Id
                         //get :: baseurl/api/Products/4
        public async Task<ActionResult<ProductDto>> GetProducstByIdAsync(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductsByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

    }

}
