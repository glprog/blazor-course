using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ShoppingCartRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.dbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }

        public async Task<CartItem> AddItemAsync(CartItemToAddDto cartItemToAddDto)
        {         
            if (await this.CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId))
            {
                return null;
            }

            var item = await this.dbContext.Products.Where(p => p.Id == cartItemToAddDto.ProductId)
                .Select(p => new CartItem
                {
                    CartId = cartItemToAddDto.CartId,
                    ProductId = cartItemToAddDto.ProductId,
                    Qty = cartItemToAddDto.Qty,
                }).SingleOrDefaultAsync();

            if (item != null)
            {
                var result = await this.dbContext.CartItems.AddAsync(item);
                await this.dbContext.SaveChangesAsync();
                return result.Entity;
            }

            return null;
        }

        public async Task<CartItem> DeleteItemAsync(int id)
        {
            var item = await this.dbContext.CartItems.SingleOrDefaultAsync(c => c.Id == id);

            if (item != null)
            {
                this.dbContext.CartItems.Remove(item);
                await this.dbContext.SaveChangesAsync();
            }
            
            return item;
        }

        public async Task<CartItem> GetItemAsync(int id)
        {
            var item = await this.dbContext.Carts
                .Join(this.dbContext.CartItems,
                    cart => cart.Id,
                    items => items.CartId,
                    (cart, items) => new { Cart = cart, Items = items })
                .Where(x => x.Items.Id == id)
                .Select(x => new CartItem
                {
                    Id = x.Items.Id,
                    CartId = x.Items.CartId,
                    ProductId = x.Items.ProductId,
                    Qty = x.Items.Qty,
                }).SingleOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<CartItem>> GetItemsAsync(int userId)
        {
            var items = await this.dbContext.Carts
                .Join(this.dbContext.CartItems, 
                    carts => carts.Id,
                    cartItems => cartItems.CartId, 
                    (carts, cartItems) => new { Cart = carts, Items = cartItems})
                .Where(x => x.Cart.UserId == userId)
                .Select(x => new CartItem
                {
                    Id = x.Items.Id,
                    CartId = x.Cart.Id,
                    ProductId = x.Items.ProductId,
                    Qty = x.Items.Qty,
                }).ToListAsync();

            return items;
        }

        public Task<CartItem> UpdateQtyAsync(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
