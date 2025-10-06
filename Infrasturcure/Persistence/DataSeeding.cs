using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _identityDbContext) : IDataSeeding
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
                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryData = File.OpenRead(@"..\Infrasturcure\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
               // TODO
            }


        }

        public async Task IdentityDataSeedingAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Abdallah@gmail.com",
                        DisplayName = "Abdallah Mostafa",
                        PhoneNumber = "0123456789",
                        UserName = "AbdallahMostafa"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Yousef@gmail.com",
                        DisplayName = "Yousef Mostafa",
                        PhoneNumber = "0123456789",
                        UserName = "YousefMostafa"
                    };
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");
                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {

            }
            
        }
    }
}
