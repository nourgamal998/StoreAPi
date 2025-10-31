using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PersistenceLayer.Data.PersistenceLayer.Data;

namespace PersistenceLayer
{
    public class DataSeeding (StoreDbContext _storeDbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                {
                    _storeDbContext.Database.Migrate();
                }

                if (!_storeDbContext.ProductBrands.Any())
                {
                    var ProductBrandsData = File.ReadAllText(@"../InfraStracture/PersistenceLayer/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
                    if (brands is not null && brands.Any())
                    {
                        _storeDbContext.ProductBrands.AddRange(brands);
                    }
                }

                if (!_storeDbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.ReadAllText(@"../InfraStracture/PersistenceLayer/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                    if (types != null && types.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(types);
                    }
                }
                if (!_storeDbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"../InfraStracture/PersistenceLayer/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (products != null && products.Any())
                    {
                        _storeDbContext.Products.AddRange(products);
                    }
                }

                _storeDbContext.SaveChanges();
            }
            catch (Exception)
            {
                //todo
            }
        }

      
    }
    }

