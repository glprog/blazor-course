using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ProductRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
        {
            var categories = await dbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await dbContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<ProductCategory> GetProductCategoryAsync(int id)
        {
            var category = await dbContext.ProductCategories.SingleOrDefaultAsync(x => x.Id == id);
            return category;
        }
    }
}
