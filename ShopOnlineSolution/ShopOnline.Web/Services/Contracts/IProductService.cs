using ShopOnline.Models;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesAsync();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
