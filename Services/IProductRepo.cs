namespace WebAPI_ASP.NET6.Services
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductAsync(int productId);
        Task<bool> ProductExistAsync(int productId);
        Task<bool> SaveChangesAsync();
        void DeleteProductAsync(int productId);
        Task CreateProduct(Product product);
    }
}
