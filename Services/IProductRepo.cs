namespace WebAPI_ASP.NET6.Services
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync(string? name, string? searchQuery);
        Task<Product?> GetProductAsync(int productId);
        Task<bool> ProductExistAsync(int productId);
        Task<bool> SaveChangesAsync();
        Task DeleteProductAsync(int product);
        Task CreateProduct(Product product);
      
    }
}
//