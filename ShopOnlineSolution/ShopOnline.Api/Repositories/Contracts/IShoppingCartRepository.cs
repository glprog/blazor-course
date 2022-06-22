using ShopOnline.Api.Entities;
using ShopOnline.Models;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItemAsync(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQtyAsync(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItemAsync(int id);
        Task<CartItem> GetItemAsync(int id);
        Task<IEnumerable<CartItem>> GetItemsAsync(int userId);
    }
}
