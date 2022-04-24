﻿namespace WebAPI_ASP.NET6.Services
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

        public async Task DeleteProductAsync(int id)
        {
            var _product = await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            _context.Remove(_product);
            
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
           return await _context.Products.OrderBy(x=>x.Id).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? name, string? searchQuery)
        {
           if(string.IsNullOrEmpty(name)
                && string.IsNullOrEmpty(searchQuery))
            {
                return await GetAllProductsAsync();
            }
           var collection = _context.Products as IQueryable<Product>;
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(x=>x.Name == name);
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(x => x.Name.Contains(searchQuery)
                || (x.Desc != null && x.Desc.Contains(searchQuery)));
            }
            return await collection.OrderBy(x=>x.Name).ToListAsync();
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
            return (await _context.SaveChangesAsync()>=0);
        }
    }
}
//