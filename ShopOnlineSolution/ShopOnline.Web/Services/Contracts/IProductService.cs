using ShopOnline.Models;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
