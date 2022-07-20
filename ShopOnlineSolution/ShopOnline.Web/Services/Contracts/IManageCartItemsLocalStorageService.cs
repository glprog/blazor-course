using ShopOnline.Models;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<IEnumerable<CartItemDto>> GetCollection();
        Task SaveCollection(List<CartItemDto> cartItemsDtos);
        Task RemoveCollection();
    }
}
