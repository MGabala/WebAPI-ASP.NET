namespace WebAPI_ASP.NET6.Services
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductAsync(int productId);
        Task<bool> ProductExistAsync(int productId);
        Task<IEnumerable<TypeOfProduct>> GetTypeOfProductsAsync(int productId);
        Task<TypeOfProduct?> GetTypeOfProduct(int productId, int typeofproductId);
        Task AddNewProduct(int productId);
    }
}
