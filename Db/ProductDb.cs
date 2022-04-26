

namespace WebAPI_ASP.NET6.Db;

public class ProductDb : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public ProductDb(DbContextOptions<ProductDb> options) : base(options)
    {

    }
}