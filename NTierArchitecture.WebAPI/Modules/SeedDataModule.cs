using Carter;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Models;

namespace NTierArchitecture.WebAPI.Modules
{
    public sealed class SeedDataModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder group)
        {
            var app = group.MapGroup("seed-data").WithTags("SeedData");
            app.MapGet("products", async (ApplicationDbContext dbContext, CancellationToken cancelationToken) =>
            {
                List<Product> products = new();
                var categories = await dbContext.Categories.ToListAsync(cancelationToken);
                for (int i = 0; i < 2000; i++)
                {
                    Product product = new Product()
                    {
                        CategoryId = categories.First().Id,
                        Name = "Product " + i,
                        UnitPrice = new Random().Next(1, 50000)
                    };
                    products.Add(product);
                }
                dbContext.Products.AddRange(products);
                await dbContext.SaveChangesAsync(cancelationToken);
            });
        }
    }
}
