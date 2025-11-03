using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceApstractionLayer;
using Shared.DTOS;


namespace PresentationLayer
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManeger _serviceManeger) : ControllerBase
    {
        [HttpGet]//Get All Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _serviceManeger.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")] //Get Product By Id
        public async Task<ActionResult<ProductDto>> GetProducstByIdAsync(int id)
        {
            var product = await _serviceManeger.ProductService.GetProductsByIdAsync(id);

            return Ok(product);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var brands = await _serviceManeger.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var types = await _serviceManeger.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

    }

}
