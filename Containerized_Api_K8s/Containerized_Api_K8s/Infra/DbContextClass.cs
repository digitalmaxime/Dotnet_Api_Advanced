using Microsoft.EntityFrameworkCore;

namespace Containerized_Api_K8s;

public class DbContextClass : DbContext
{

    public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
    {

    }

    public DbSet<ProductDetails> Products { get; set; }
}