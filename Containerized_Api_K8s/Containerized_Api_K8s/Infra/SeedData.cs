using Microsoft.EntityFrameworkCore;

namespace Containerized_Api_K8s;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DbContextClass(
                   serviceProvider.GetRequiredService<DbContextOptions<DbContextClass>>()))
        {
            if (context.Products.Any())
            {
                return;
            }

            context.Products.AddRange(
                new ProductDetails
                {
                    Id = 1,
                    ProductName = "IPhone",
                    ProductDescription = "IPhone 14",
                    ProductPrice = 120000,
                    ProductStock = 100
                },
                new ProductDetails
                {
                    Id = 2,
                    ProductName = "Samsung TV",
                    ProductDescription = "Smart TV",
                    ProductPrice = 400000,
                    ProductStock = 120
                });
            context.SaveChanges();
        }
    }
}