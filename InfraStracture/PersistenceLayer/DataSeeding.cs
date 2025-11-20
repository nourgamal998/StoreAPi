using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PersistenceLayer.Data.PersistenceLayer.Data;

namespace PersistenceLayer
{
    public class DataSeeding (StoreDbContext _storeDbContext) : IDataSeed
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    _storeDbContext.Database.Migrate();
                }

                if (!_storeDbContext.ProductBrands.Any())
                {
                    var ProductBrandsData =File.OpenRead(@"../InfraStracture/PersistenceLayer/Data/DataSeed/brands.json");
                    var brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);
                    if (brands is not null && brands.Any())
                    {
                        await _storeDbContext.ProductBrands.AddRangeAsync(brands);
                    }
                }

                if (!_storeDbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead(@"../InfraStracture/PersistenceLayer/Data/DataSeed/types.json");
                    var types =await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                    if (types != null && types.Any())
                    {
                       await _storeDbContext.ProductTypes.AddRangeAsync(types);
                    }
                }
                if (!_storeDbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead(@"../InfraStracture/PersistenceLayer/Data/DataSeed/products.json");
                    var products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (products != null && products.Any())
                    {
                       await _storeDbContext.Products.AddRangeAsync(products);
                    }
                }

               await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                //todo
            }
        }

    }
}

