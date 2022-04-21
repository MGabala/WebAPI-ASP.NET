namespace WebAPI_ASP.NET6.Services
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDb _context;

        public ProductRepo(ProductDb context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProduct(Product product)
        { 
            await _context.Products.AddAsync(product);

        }

        public Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
           return await _context.Products.OrderBy(x=>x.Id).ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
           return await _context.Products.Where(x=>x.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<bool> ProductExistAsync(int productId)
        {
            return await _context.Products.AnyAsync(c => c.Id ==productId);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
