//--------------------------------------------------------------------------------------------------------------------------
//CRUD with Authorization by Policy with search & filter & pagination metadata. Documentation completed for sample methods.|
//--------------------------------------------------------------------------------------------------------------------------
namespace WebAPI_ASP.NET6.Db;

public class ProductDb : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public ProductDb(DbContextOptions<ProductDb> options) : base(options)
    {

    }
}