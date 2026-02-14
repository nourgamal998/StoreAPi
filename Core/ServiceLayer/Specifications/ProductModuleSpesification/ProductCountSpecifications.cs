using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.ProductModels;
using DomanLayer.Specifications;
using Shared;

namespace ServiceLayer.Specifications.ProductModuleSpesification
{
    public class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) :
          base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                 && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
          && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        { }
    }
}
