namespace WebAPI_ASP.NET6.Services
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductAsync(int productId, bool includeType);
        Task<IEnumerable<TypeOfProduct>> GetTypeOfProductsAsync(int productId);
        Task<TypeOfProduct?> GetTypeOfProduct(int productId, int typeofproductId);
    }
}
