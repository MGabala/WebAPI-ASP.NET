//----------------------------------------------------------------------------
//CRUD with Authorization Policy with search & filter & pagination metadata. |
//----------------------------------------------------------------------------
namespace WebAPI_ASP.NET6.Services
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<(IEnumerable<Product>, PaginationMetadata)> GetAllProductsAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Product?> GetProductAsync(int productId);
        Task<bool> ProductExistAsync(int productId);
        Task<bool> SaveChangesAsync();
        Task DeleteProductAsync(int product);
        Task CreateProduct(Product product);
      
    }
}