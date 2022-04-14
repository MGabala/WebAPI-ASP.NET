namespace WebAPI_ASP.NET6.Services
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDb context;

        public ProductRepo(ProductDb context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
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
