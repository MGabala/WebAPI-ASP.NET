namespace WebAPI_ASP.NET6.Services
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDb context;

        public ProductRepo(ProductDb context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
           return await context.Products.OrderBy(x=>x.Id).ToListAsync();
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
