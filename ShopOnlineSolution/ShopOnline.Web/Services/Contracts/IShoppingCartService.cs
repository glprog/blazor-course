using ShopOnline.Models;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId);
        Task<CartItemDto> GetItemAsync(int id);
        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItemAsync(int id);
    }
}
