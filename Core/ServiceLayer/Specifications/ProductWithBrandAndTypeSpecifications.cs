using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;

namespace ServiceLayer.Specifications
{
    public class ProductWithBrandAndTypeSpecifications :BaseSpecifications<Product , int>
    {
        //get all product with brand and type
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        //get product by id with brand and type
        public ProductWithBrandAndTypeSpecifications(int id ) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
