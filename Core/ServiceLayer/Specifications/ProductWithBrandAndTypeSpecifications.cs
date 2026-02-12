using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.ProductModels;
using Shared;

namespace ServiceLayer.Specifications
{
    public class ProductWithBrandAndTypeSpecifications :BaseSpecifications <Product , int>
    {
        //get all product with brand and type
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) :
            base(p => (!queryParams.BrandId.HasValue || p.BrandId== queryParams.BrandId)
                   && (!queryParams.TypeId.HasValue || p.TypeId== queryParams.TypeId)
            &&(string.IsNullOrWhiteSpace(queryParams.SearchValue) ||p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {//where(p=> p.brandid== brandid && typeid == typeid )
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            //sorting
            switch (queryParams.SortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;
            }

            //pagination
            ApplyPagination(queryParams.PageSize , queryParams.PageIndex);
                
        }

        //get product by id with brand and type
        public ProductWithBrandAndTypeSpecifications(int id ) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
