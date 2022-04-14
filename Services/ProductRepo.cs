namespace WebAPI_ASP.NET6.Services
{
    public class ProductRepo : IProductRepo
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<TypeOfProduct?> GetTypeOfProduct(int productId, int typeofproductId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TypeOfProduct>> GetTypeOfProductsAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
