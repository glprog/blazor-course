using Microsoft.AspNetCore.Components;
using ShopOnline.Models;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; private set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }


        protected override async  Task OnInitializedAsync()
        {
            try
            {
                this.ShoppingCartItems = (await this.ShoppingCartService.GetItemsAsync(Constants.UserId)).ToList();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            if (qty > 0)
            {
                var updateItemDto = new CartItemQtyUpdateDto
                {
                    CartItemId = id,
                    Qty = qty,
                };

                var result = await this.ShoppingCartService.UpdateQty(updateItemDto);
            }
            else
            {
                var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    item.Qty = qty;
                    item.TotalPrice = item.Price;
                }
            }
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await this.ShoppingCartService.DeleteItemAsync(id);
            if (cartItemDto != null)
            {
                this.RemoveCartItem(cartItemDto.Id);
            }
        }

        private CartItemDto GetCartItem(int id)
        {
            return this.ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = this.GetCartItem(id);
            this.ShoppingCartItems.Remove(cartItemDto);
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C");
        }
    }
}
