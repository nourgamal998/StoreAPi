using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;
using Shared.DTOS;

namespace ServiceApstractionLayer
{
    //public  interface IProductService
    //{
    //    Task<IEnumerable<Product>> GetAllProductsAsync();
    //    Task<ProductDto<Product>> GetProductsByIdAsync();
    //    Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    //    Task<IEnumerable<BrandDto>> GetBrandsAsync();

    //}
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductsByIdAsync(int Id);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task GetProductByIdAsync(int id);
        Task GetAllBrandsAsync();
    }
}
