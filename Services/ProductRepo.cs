namespace WebAPI_ASP.NET6.Services
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDb _context;

        public ProductRepo(ProductDb context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
           return await _context.Products.OrderBy(x=>x.Id).ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int productId, bool includeType)
        {
            if (includeType)
            {
                return await _context.Products.Include(x=> x.TypeOfProduct)
                    .Where(x=>x.Id==productId).FirstOrDefaultAsync();
            }
            return await _context.Products.Where(x=>x.Id==productId).FirstOrDefaultAsync();
        }

        public async Task<TypeOfProduct?> GetTypeOfProduct(int productId, int typeofproductId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TypeOfProduct>> GetTypeOfProductsAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
