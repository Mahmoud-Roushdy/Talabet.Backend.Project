using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Order_Aggregate;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public static class DataSeeding
    { 
        public static async Task SeedingAsync (StoreDbcontext dbcontext)
        { 
            if (!dbcontext.ProductBrands.Any())

            {
                var BrnandsData = File.ReadAllText("../Talabat.Repository/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrnandsData);
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(brand);

                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.ProductTypes.Any())

            {
                var TypesData = File.ReadAllText("../Talabat.Repository/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types is not null && Types.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(Type);

                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Products.Any())

            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products is not null && Products.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbcontext.Set<Product>().AddAsync(Product);

                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.deliveryMethods.Any())

            {
                var DeliverysData = File.ReadAllText("../Talabat.Repository/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliverysData);
                if (DeliveryMethods is not null && DeliveryMethods.Count > 0)
                {
                    foreach (var delivery in DeliveryMethods)
                    {
                        await dbcontext.Set<DeliveryMethod>().AddAsync(delivery);

                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

        }
    }
}
