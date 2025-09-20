using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
         public async Task DataSeedingAsync()
        {
            try
            {
                var GetPendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (GetPendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                      await  _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes =  await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\products.json");
                    var Product = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Product is not null && Product.Any())
                        await _dbContext.Products.AddRangeAsync(Product);
                }
               await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
               // TODO
            }


        }
    }
}
